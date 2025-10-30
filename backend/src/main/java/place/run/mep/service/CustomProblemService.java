package place.run.mep.service;

import com.fasterxml.jackson.core.type.TypeReference;
import com.fasterxml.jackson.databind.ObjectMapper;
import lombok.RequiredArgsConstructor;
import lombok.extern.slf4j.Slf4j; // Lombok @Slf4j import
import org.springframework.beans.factory.annotation.Value;
import org.springframework.core.ParameterizedTypeReference;
import org.springframework.http.HttpMethod;
import org.springframework.http.ResponseEntity;
import org.springframework.stereotype.Service;
import org.springframework.transaction.annotation.Transactional;
import org.springframework.web.client.RestClientException;
import org.springframework.web.client.RestTemplate;
import place.run.mep.dto.*;
import place.run.mep.entity.*;
import place.run.mep.repository.*;

import java.math.BigDecimal;
import java.math.RoundingMode;
import java.time.LocalDateTime;
import java.util.*;
import java.util.concurrent.ConcurrentHashMap;

@Service
@Transactional
@RequiredArgsConstructor
@Slf4j
public class CustomProblemService {


    private final UserRepository userRepository;
    private final SubUnitGroupRepository subUnitGroupRepository;
    private final UserSkillProficiencyRepository proficiencyRepository;
    private final ProblemResponseRepository problemResponseRepository;
    private final RestTemplate restTemplate;
    private final ObjectMapper objectMapper;

    @Value("${ai.problem.server.url}")
    private String AI_PROBLEM_SERVER_URL;

    @Value("${sakt.server.url}")
    private String SAKT_MODEL_SERVER_URL;

    private final Map<String, Map<String, String>> temporaryAnswerStore = new ConcurrentHashMap<>();

    @Transactional(readOnly = true)
    public List<ProblemDto> getCustomProblems(String userId, Integer subUnitGroupId) {
        User user = userRepository.findByUserId(userId)
                .orElseThrow(() -> new RuntimeException("사용자를 찾을 수 없습니다."));
        SubUnitGroup group = subUnitGroupRepository.findById(subUnitGroupId)
                .orElseThrow(() -> new RuntimeException("소단원 그룹을 찾을 수 없습니다."));

        BigDecimal currentProficiency = proficiencyRepository.findByUserAndSubUnitGroup(user, group)
                .map(UserSkillProficiency::getProficiencyPercentage)
                .orElse(BigDecimal.ZERO);

        log.info("Fetching problems for user: {}, group: {}, proficiency: {}", userId, subUnitGroupId, currentProficiency);

        Map<Integer, Integer> problemMix = determineProblemMix(currentProficiency.doubleValue(), subUnitGroupId);
        log.debug("Determined problem mix: {}", problemMix);

        List<ProblemDto> customProblems = new ArrayList<>();
        Map<String, String> currentAnswers = new ConcurrentHashMap<>();

        problemMix.forEach((level, count) -> {
            if (count > 0) {
                String requestUrl = String.format("%s/problems?subUnitGroupId=%d&level=%d&count=%d",
                        AI_PROBLEM_SERVER_URL, subUnitGroupId, level, count);
                log.debug("Requesting problems from URL: {}", requestUrl);
                try {
                    String jsonResponse = restTemplate.getForObject(requestUrl, String.class);
                    if (jsonResponse != null && !jsonResponse.isEmpty()) {
                        List<ProblemDto> problems = objectMapper.readValue(jsonResponse, new TypeReference<List<ProblemDto>>() {});
                        customProblems.addAll(problems);
                        log.info("Received {} problems of level {} for group {}", problems.size(), level, subUnitGroupId);

                        problems.forEach(p -> {
                            if (p.getProblemId() != null && p.getAnswer() != null) {
                                currentAnswers.put(p.getProblemId(), p.getAnswer());
                            } else {
                                log.warn("Problem received from AI server is missing ID or Answer: {}", p);
                            }
                        });
                    } else {
                        log.warn("Received empty response from AI server for URL: {}", requestUrl);
                    }
                } catch (RestClientException e) {
                    log.error("Error connecting to AI problem server at {}: {}", requestUrl, e.getMessage());
                    throw new RuntimeException("문제 은행 서버에 연결할 수 없습니다.", e);
                } catch (Exception e) {
                    log.error("Error fetching or parsing problems from AI server for URL {}: {}", requestUrl, e.getMessage(), e);
                    throw new RuntimeException("문제 데이터를 처리하는 중 오류가 발생했습니다.", e);
                }
            }
        });

        String storeKey = generateStoreKey(userId, subUnitGroupId);
        temporaryAnswerStore.put(storeKey, currentAnswers);
        log.info("Stored answers temporarily for key: {}", storeKey);

        return customProblems;
    }

    public CustomProblemSubmitResponseDto submitCustomProblems(String userId, CustomProblemSubmitRequestDto requestDto) {
        User user = userRepository.findByUserId(userId).orElseThrow(() -> new RuntimeException("User not found"));
        SubUnitGroup group = subUnitGroupRepository.findById(requestDto.getSubUnitGroupId())
                .orElseThrow(() -> new RuntimeException("SubUnitGroup not found"));

        List<ProblemAnswerDto> userAnswers = requestDto.getAnswers();
        List<CustomProblemResultDto> gradingResults = new ArrayList<>();
        List<SaktRequestProblemDto> saktHistory = new ArrayList<>();

        String storeKey = generateStoreKey(userId, requestDto.getSubUnitGroupId());
        Map<String, String> correctAnswers = temporaryAnswerStore.get(storeKey);

        if (correctAnswers == null) {
            log.warn("Answer key {} not found in temporary store for user {}", storeKey, userId);
            throw new IllegalStateException("제출 시간이 초과되었거나 문제가 조회되지 않았습니다. 문제를 다시 조회 후 제출해주세요.");
        }

        log.info("Processing submission for user: {}, group: {}, number of answers: {}", userId, group.getSubUnitGroupId(), userAnswers.size());

        // --- 채점 및 풀이 기록 저장 ---
        for (ProblemAnswerDto userAnswer : userAnswers) {
            String problemId = userAnswer.getProblemId();
            String correctAnswer = correctAnswers.get(problemId);
            boolean isCorrectBool = false;

            if (correctAnswer != null) {
                isCorrectBool = correctAnswer.equalsIgnoreCase(userAnswer.getUserAnswer());
                log.debug("Grading problemId: {}, userAnswer: {}, correctAnswer: {}, result: {}",
                        problemId, userAnswer.getUserAnswer(), correctAnswer, isCorrectBool);
            } else {
                log.warn("Correct answer not found for problemId: {} in key: {}. Marking as incorrect.", problemId, storeKey);
            }

            ProblemResponse response = new ProblemResponse(user, problemId, userAnswer.getUserAnswer(), isCorrectBool);
            problemResponseRepository.save(response);

            gradingResults.add(new CustomProblemResultDto(problemId, userAnswer.getUserAnswer(), isCorrectBool));
            saktHistory.add(new SaktRequestProblemDto(problemId, isCorrectBool ? 1 : 0));
        }

        // --- SAKT 모델 호출 및 숙련도 업데이트 ---
        BigDecimal newProficiency = callSaktAndUpdateProficiency(userId, user, group, saktHistory);
        log.info("Updated proficiency for user: {}, group: {} to {}", userId, group.getSubUnitGroupId(), newProficiency);

        // --- 처리 완료 후 임시 저장소 정리 ---
        temporaryAnswerStore.remove(storeKey);
        log.info("Removed temporary answers for key: {}", storeKey);

        return new CustomProblemSubmitResponseDto(gradingResults, newProficiency);
    }

    // SAKT 호출 및 숙련도 업데이트
    private BigDecimal callSaktAndUpdateProficiency(String userId, User user, SubUnitGroup group, List<SaktRequestProblemDto> saktHistory) {
        BigDecimal newProficiency = BigDecimal.ZERO;
        try {
            String saktRequestUrl = SAKT_MODEL_SERVER_URL + "/sakt/predict";
            Map<String, Object> saktRequestBody = new HashMap<>();
            saktRequestBody.put("userId", userId);
            saktRequestBody.put("problemHistory", saktHistory);

            log.debug("Calling SAKT server at {} with history size: {}", saktRequestUrl, saktHistory.size());
            Map<String, Object> responseMap = restTemplate.postForObject(saktRequestUrl, saktRequestBody, Map.class);

            if (responseMap != null && responseMap.containsKey("newProficiencyPercentage")) {
                Object proficiencyValue = responseMap.get("newProficiencyPercentage");
                try {
                    newProficiency = new BigDecimal(proficiencyValue.toString()).setScale(2, RoundingMode.HALF_UP);
                    log.info("Received new proficiency from SAKT: {}", newProficiency);

                    // --- 숙련도 DB 업데이트 ---
                    UserSkillProficiency proficiency = proficiencyRepository.findByUserAndSubUnitGroup(user, group)
                            .orElseGet(() -> {
                                log.info("Creating new proficiency record for user: {}, group: {}", userId, group.getSubUnitGroupId());
                                UserSkillProficiency newProf = new UserSkillProficiency();
                                newProf.setUser(user);
                                newProf.setSubUnitGroup(group);
                                return newProf;
                            });
                    proficiency.setProficiencyPercentage(newProficiency);
                    proficiency.setUpdatedAt(LocalDateTime.now());
                    proficiencyRepository.save(proficiency);
                    log.info("Successfully updated proficiency in DB for user: {}, group: {}", userId, group.getSubUnitGroupId());

                } catch (NumberFormatException e) {
                    log.error("SAKT server returned non-numeric proficiency value: {}", proficiencyValue, e);
                }
            } else {
                log.error("Failed to get 'newProficiencyPercentage' from SAKT server. Response: {}", responseMap);
            }
        } catch (RestClientException e) {
            log.error("Error connecting to SAKT server at {}: {}", SAKT_MODEL_SERVER_URL, e.getMessage());
        } catch (Exception e) {
            log.error("Error calling SAKT server or updating proficiency: {}", e.getMessage(), e);
        }
        return newProficiency;
    }


    private String generateStoreKey(String userId, Integer subUnitGroupId) {
        return userId + "_" + subUnitGroupId;
    }


    private Map<Integer, Integer> determineProblemMix(double proficiency, int subUnitGroupId) {
        final int TOTAL_PROBLEMS = 30;
        log.debug("Determining problem mix for proficiency: {}, target total: {}", proficiency, TOTAL_PROBLEMS);
        Map<Integer, Double> targetRatio = getTargetRatio(proficiency);
        log.debug("Target ratio: {}", targetRatio);

        String inventoryUrl = String.format("%s/problems/inventory?subUnitGroupId=%d", AI_PROBLEM_SERVER_URL, subUnitGroupId);
        Map<Integer, Integer> inventory = new HashMap<>();
        try {
            ResponseEntity<Map<Integer, Integer>> response = restTemplate.exchange(inventoryUrl, HttpMethod.GET, null, new ParameterizedTypeReference<>() {});
            inventory = response.getBody();
            if (inventory == null) inventory = new HashMap<>();
            log.debug("Received inventory: {}", inventory);
        } catch (RestClientException e) {
            log.error("Error connecting to AI inventory server at {}: {}", inventoryUrl, e.getMessage());
        } catch (Exception e) {
            log.error("Error fetching or parsing inventory from AI server for URL {}: {}", inventoryUrl, e.getMessage(), e);
        }

        Map<Integer, Integer> finalMix = new HashMap<>();
        int securedProblems = 0;

        for (Map.Entry<Integer, Double> entry : targetRatio.entrySet()) {
            int level = entry.getKey();
            double ratio = entry.getValue();
            int idealCount = (int) Math.round(TOTAL_PROBLEMS * ratio);
            int availableCount = inventory.getOrDefault(level, 0);
            int countToSecure = Math.min(idealCount, availableCount);

            if (countToSecure > 0) {
                finalMix.put(level, countToSecure);
                securedProblems += countToSecure;
                inventory.put(level, availableCount - countToSecure);
            }
        }
        log.debug("Secured {} problems based on target ratio. Mix: {}", securedProblems, finalMix);

        int deficit = TOTAL_PROBLEMS - securedProblems;
        if (deficit > 0) {
            log.warn("Deficit of {} problems. Trying to fill from remaining inventory.", deficit);
            List<Integer> fillOrder = Arrays.asList(2, 3, 4);
            for (int level : fillOrder) {
                if (deficit == 0) break;
                int availableCount = inventory.getOrDefault(level, 0);
                int countToTake = Math.min(deficit, availableCount);

                if (countToTake > 0) {
                    finalMix.put(level, finalMix.getOrDefault(level, 0) + countToTake);
                    deficit -= countToTake;
                    inventory.put(level, availableCount - countToTake);
                    log.debug("Filled {} problems from level {}. Deficit remaining: {}", countToTake, level, deficit);
                }
            }
            if (deficit > 0) {
                log.error("Could not fill deficit. Not enough problems in inventory to meet TOTAL_PROBLEMS({}). Final secured: {}", TOTAL_PROBLEMS, (TOTAL_PROBLEMS-deficit));
            }
        }
        log.info("Final problem mix determined: {}", finalMix);
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