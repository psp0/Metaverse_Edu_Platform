package place.run.mep.service;

import lombok.RequiredArgsConstructor;
import org.springframework.stereotype.Service;
import org.springframework.transaction.annotation.Transactional;
import place.run.mep.dto.QuizAnswerResponseDto;
import place.run.mep.dto.QuizDetailDto;
import place.run.mep.dto.SubUnitQuizStateDto;
import place.run.mep.entity.*;
import place.run.mep.repository.*;

import java.math.BigDecimal;
import java.math.RoundingMode;
import java.time.LocalDateTime;
import java.util.List;
import java.util.Map;
import java.util.stream.Collectors;

@Service
@Transactional
@RequiredArgsConstructor
public class QuizService {

    private final UserRepository userRepository;
    private final SubUnitRepository subUnitRepository;
    private final QuizRepository quizRepository;
    private final QuizOptionRepository quizOptionRepository;
    private final QuizResponseRepository quizResponseRepository;
    private final UserUnitProgressRepository userUnitProgressRepository;
    private final ProficiencyService proficiencyService;


    @Transactional(readOnly = true)
    public SubUnitQuizStateDto getQuizState(String userId, Integer subUnitId) {
        // 1. 기본 정보(사용자, 소단원) 조회
        User user = userRepository.findByUserId(userId)
                .orElseThrow(() -> new RuntimeException("User not found"));
        SubUnit subUnit = subUnitRepository.findById(subUnitId)
                .orElseThrow(() -> new RuntimeException("SubUnit not found"));

        // 2. 해당 소단원의 모든 퀴즈와 선택지를 한 번에 조회
        List<Quiz> allQuizzes = quizRepository.findBySubUnitWithOptions(subUnit);

        // 3. 사용자의 모든 응답 기록을 Map으로 변환하여 조회 성능 향상
        Map<Integer, QuizResponse> responseMap = quizResponseRepository.findByUserAndQuiz_SubUnit(user, subUnit)
                .stream()
                .collect(Collectors.toMap(response -> response.getQuiz().getQuizId(), response -> response));

        // 4. 전체 퀴즈 목록을 순회하며 DTO 생성w
        List<QuizDetailDto> quizDetails = allQuizzes.stream()
                .map(quiz -> {
                    QuizResponse userResponse = responseMap.get(quiz.getQuizId());
                    if (userResponse != null) {
                        // 이미 푼 문제일 경우: 응답 기록을 포함하여 DTO 생성
                        return new QuizDetailDto(quiz, userResponse);
                    } else {
                        // 아직 안 푼 문제일 경우: 퀴즈 정보만으로 DTO 생성
                        return new QuizDetailDto(quiz);
                    }
                })
                .collect(Collectors.toList());

        // 5. 최종 결과(카운트) 계산
        int totalQuizCount = allQuizzes.size();
        int solvedQuizCount = responseMap.size();
        int correctQuizCount = (int) responseMap.values().stream().filter(QuizResponse::getIsCorrect).count();

        // 6. 최종 DTO를 생성하여 반환
        return new SubUnitQuizStateDto(subUnitId, totalQuizCount, solvedQuizCount, correctQuizCount, quizDetails);
    }

    public QuizAnswerResponseDto submitAnswer(String userId, Integer quizId, Integer optionId) {
        User user = userRepository.findByUserId(userId).orElseThrow(() -> new RuntimeException("User not found"));
        Quiz quiz = quizRepository.findById(quizId).orElseThrow(() -> new RuntimeException("Quiz not found"));
        QuizOption selectedOption = quizOptionRepository.findById(optionId).orElseThrow(() -> new RuntimeException("Option not found"));

        if (!selectedOption.getQuiz().equals(quiz)) {
            throw new IllegalArgumentException("선택한 옵션이 해당 퀴즈에 속하지 않습니다.");
        }

        if (quizResponseRepository.findByUserAndQuiz(user, quiz).isPresent()) {
            throw new IllegalStateException("이미 제출한 퀴즈입니다.");
        }

        boolean isCorrect = selectedOption.getIsAnswer();

        QuizResponse response = new QuizResponse(quiz, user, selectedOption, isCorrect, LocalDateTime.now());
        quizResponseRepository.save(response);

        updateQuizProgress(user, quiz.getSubUnit());

        checkAndCalculateInitialProficiency(user, quiz.getSubUnit().getSubUnitGroup());

        Explanation explanation = quiz.getExplanation();
        String commentary = (explanation != null) ? explanation.getCommentary() : "해설이 제공되지 않는 문제입니다.";

        Integer correctOptionId = quiz.getOptions().stream()
                .filter(QuizOption::getIsAnswer)
                .findFirst()
                .map(QuizOption::getOptionId)
                .orElse(null);

        return new QuizAnswerResponseDto(isCorrect, commentary, correctOptionId);
    }

    // [삭제] 이 메소드는 getQuizState에 기능이 통합되었으므로 삭제합니다.
    // public SolvedQuizDetailDto getSolvedQuizDetail(String userId, Integer quizId) { ... }

    private void updateQuizProgress(User user, SubUnit subUnit) {
        UserUnitProgress progress = userUnitProgressRepository.findByUserAndSubUnit(user, subUnit)
                .orElse(new UserUnitProgress(user, subUnit));

        long totalQuizzes = quizRepository.countBySubUnit(subUnit);
        long answeredQuizzes = quizResponseRepository.countByUserAndQuiz_SubUnit(user, subUnit);

        BigDecimal quizProgress = BigDecimal.ZERO;
        if (totalQuizzes > 0) {
            quizProgress = new BigDecimal(answeredQuizzes * 100.0 / totalQuizzes)
                    .setScale(2, RoundingMode.HALF_UP);
        }
        progress.setQuizProgress(quizProgress);

        BigDecimal lectureProgress = progress.getLectureProgress() != null ? progress.getLectureProgress() : BigDecimal.ZERO;

        // 강의 50%, 퀴즈 50% 가중치 적용
        progress.setProgressPercentage(lectureProgress.multiply(new BigDecimal("0.5")).add(quizProgress.multiply(new BigDecimal("0.5"))));
        progress.setUpdatedAt(LocalDateTime.now());
        userUnitProgressRepository.save(progress);
    }

    private void checkAndCalculateInitialProficiency(User user, SubUnitGroup group) {
        List<SubUnit> subUnitsInGroup = group.getSubUnits();
        if (subUnitsInGroup.isEmpty()) {
            return;
        }

        List<UserUnitProgress> progresses = userUnitProgressRepository.findAllByUserAndSubUnitGroup(user, group.getSubUnitGroupId());
        long totalSubUnitsInGroup = subUnitsInGroup.size();

        long completedQuizSubUnits = progresses.stream()
                .filter(p -> p.getQuizProgress() != null && p.getQuizProgress().compareTo(new BigDecimal("100.00")) == 0)
                .count();

        if (totalSubUnitsInGroup > 0 && totalSubUnitsInGroup == completedQuizSubUnits) {
            proficiencyService.calculateAndSaveInitialGroupProficiency(user, group);
        }
    }
}