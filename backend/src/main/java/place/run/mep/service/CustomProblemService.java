package place.run.mep.service;

import lombok.RequiredArgsConstructor;
import org.springframework.beans.factory.annotation.Value;
import org.springframework.core.ParameterizedTypeReference;
import org.springframework.http.HttpMethod;
import org.springframework.http.ResponseEntity;
import org.springframework.stereotype.Service;
import org.springframework.transaction.annotation.Transactional;
import org.springframework.web.client.RestTemplate;
import place.run.mep.dto.*;
import place.run.mep.entity.*;
import place.run.mep.repository.*;

import java.math.BigDecimal;
import java.time.LocalDateTime;
import java.util.*;
import java.util.stream.Collectors;

@Service
@Transactional
@RequiredArgsConstructor
public class CustomProblemService {

    private final UserRepository userRepository;
    private final SubUnitGroupRepository subUnitGroupRepository;
    private final UserSkillProficiencyRepository proficiencyRepository;
    private final ProblemResponseRepository problemResponseRepository;
    private final RestTemplate restTemplate;

    @Value("${ai.problem.server.url}")
    private String AI_PROBLEM_SERVER_URL;

    @Value("${sakt.server.url}")
    private String SAKT_MODEL_SERVER_URL;

    @Transactional(readOnly = true)
    public List<ProblemDto> getCustomProblems(String userId, Integer subUnitGroupId) {
        User user = userRepository.findByUserId(userId)
                .orElseThrow(() -> new RuntimeException("사용자를 찾을 수 없습니다."));
        SubUnitGroup group = subUnitGroupRepository.findById(subUnitGroupId)
                .orElseThrow(() -> new RuntimeException("소단원 그룹을 찾을 수 없습니다."));

        UserSkillProficiency proficiency = proficiencyRepository.findByUserAndSubUnitGroup(user, group)
                .orElseThrow(() -> new IllegalStateException("사용자의 숙달도 정보가 없습니다."));

        Map<Integer, Integer> problemMix = determineProblemMix(proficiency.getProficiencyPercentage().doubleValue(), subUnitGroupId);

        List<ProblemDto> customProblems = new ArrayList<>();
        problemMix.forEach((level, count) -> {
            if (count > 0) {
                String requestUrl = String.format("%s/problems?subUnitGroupId=%d&level=%d&count=%d",
                        AI_PROBLEM_SERVER_URL, subUnitGroupId, level, count);
                ProblemDto[] problems = restTemplate.getForObject(requestUrl, ProblemDto[].class);
                if (problems != null) {
                    customProblems.addAll(List.of(problems));
                }
            }
        });
        return customProblems;
    }

    public CustomProblemSubmitResponseDto submitCustomProblems(String userId, CustomProblemSubmitRequestDto requestDto) {
        User user = userRepository.findByUserId(userId).orElseThrow(() -> new RuntimeException("User not found"));
        SubUnitGroup group = subUnitGroupRepository.findById(requestDto.getSubUnitGroupId())
                .orElseThrow(() -> new RuntimeException("SubUnitGroup not found"));

        List<ProblemAnswerDto> userAnswers = requestDto.getAnswers();
        List<Long> problemIds = userAnswers.stream().map(ProblemAnswerDto::getProblemId).collect(Collectors.toList());

        String solutionsUrl = AI_PROBLEM_SERVER_URL + "/problems/solutions";
        ProblemSolutionDto[] solutions = restTemplate.postForObject(solutionsUrl, problemIds, ProblemSolutionDto[].class);
        Map<Long, ProblemSolutionDto> solutionMap = new HashMap<>();
        if (solutions != null) {
            for (ProblemSolutionDto solution : solutions) {
                solutionMap.put(solution.getProblemId(), solution);
            }
        }

        List<CustomProblemResultDto> gradingResults = new ArrayList<>();
        List<SaktRequestProblemDto> saktHistory = new ArrayList<>();

        for (ProblemAnswerDto userAnswer : userAnswers) {
            ProblemSolutionDto solution = solutionMap.get(userAnswer.getProblemId());
            boolean isCorrectBool = false;
            String explanation = "해당 문제의 해설 정보가 없습니다.";

            if (solution != null) {
                isCorrectBool = solution.getCorrectAnswer().equalsIgnoreCase(userAnswer.getUserAnswer());
                explanation = solution.getExplanation();
            }

            ProblemResponse response = new ProblemResponse(user, String.valueOf(userAnswer.getProblemId()), userAnswer.getUserAnswer(), isCorrectBool);
            problemResponseRepository.save(response);

            gradingResults.add(new CustomProblemResultDto(userAnswer.getProblemId(), userAnswer.getUserAnswer(), isCorrectBool, explanation));
            saktHistory.add(new SaktRequestProblemDto(String.valueOf(userAnswer.getProblemId()), isCorrectBool ? 1 : 0));
        }

        String saktRequestUrl = SAKT_MODEL_SERVER_URL + "/sakt/predict";
        Map<String, Object> saktRequestBody = new HashMap<>();
        saktRequestBody.put("userId", userId);
        saktRequestBody.put("problemHistory", saktHistory);

        Map<String, Object> responseMap = restTemplate.postForObject(saktRequestUrl, saktRequestBody, Map.class);
        if (responseMap == null || !responseMap.containsKey("newProficiencyPercentage")) {
            throw new RuntimeException("SAKT 모델 서버에서 숙달도 결과를 받지 못했습니다.");
        }

        Object proficiencyValue = responseMap.get("newProficiencyPercentage");
        BigDecimal newProficiency = new BigDecimal(proficiencyValue.toString());

        UserSkillProficiency proficiency = proficiencyRepository.findByUserAndSubUnitGroup(user, group)
                .orElseThrow(() -> new IllegalStateException("숙달도 정보가 없습니다."));

        proficiency.setProficiencyPercentage(newProficiency);
        proficiency.setUpdatedAt(LocalDateTime.now());
        proficiencyRepository.save(proficiency);

        return new CustomProblemSubmitResponseDto(gradingResults, newProficiency);
    }

    private Map<Integer, Integer> determineProblemMix(double proficiency, int subUnitGroupId) {
        final int TOTAL_PROBLEMS = 30;

        Map<Integer, Double> targetRatio = getTargetRatio(proficiency);

        String inventoryUrl = String.format("%s/problems/inventory?subUnitGroupId=%d", AI_PROBLEM_SERVER_URL, subUnitGroupId);
        ResponseEntity<Map<Integer, Integer>> response = restTemplate.exchange(inventoryUrl, HttpMethod.GET, null, new ParameterizedTypeReference<>() {});
        Map<Integer, Integer> inventory = response.getBody();
        if (inventory == null) inventory = new HashMap<>();

        Map<Integer, Integer> finalMix = new HashMap<>();
        int securedProblems = 0;

        for (Map.Entry<Integer, Double> entry : targetRatio.entrySet()) {
            int level = entry.getKey();
            double ratio = entry.getValue();
            int idealCount = (int) Math.round(TOTAL_PROBLEMS * ratio);
            int availableCount = inventory.getOrDefault(level, 0);

            int countToSecure = Math.min(idealCount, availableCount);
            finalMix.put(level, countToSecure);
            securedProblems += countToSecure;
            inventory.put(level, availableCount - countToSecure);
        }

        int deficit = TOTAL_PROBLEMS - securedProblems;
        if (deficit > 0) {
            List<Integer> levels = Arrays.asList(2, 3, 4);
            for (int level : levels) {
                if (deficit == 0) break;
                int availableCount = inventory.getOrDefault(level, 0);
                int countToTake = Math.min(deficit, availableCount);

                finalMix.put(level, finalMix.getOrDefault(level, 0) + countToTake);
                deficit -= countToTake;
            }
        }

        return finalMix;
    }

    private Map<Integer, Double> getTargetRatio(double proficiency) {
        Map<Integer, Double> ratio = new HashMap<>();
        if (proficiency <= 30) {
            ratio.put(2, 0.70); ratio.put(3, 0.30); ratio.put(4, 0.0);
        } else if (proficiency <= 60) {
            ratio.put(2, 0.40); ratio.put(3, 0.50); ratio.put(4, 0.10);
        } else if (proficiency <= 90) {
            ratio.put(2, 0.10); ratio.put(3, 0.50); ratio.put(4, 0.40);
        } else {
            ratio.put(2, 0.0); ratio.put(3, 0.30); ratio.put(4, 0.70);
        }
        return ratio;
    }
}