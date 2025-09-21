package place.run.mep.service;

import lombok.RequiredArgsConstructor;
import org.springframework.stereotype.Service;
import org.springframework.transaction.annotation.Transactional;
import place.run.mep.dto.QuizAnswerRequestDto;
import place.run.mep.dto.QuizAnswerResponseDto;
import place.run.mep.dto.QuizStateDto;
import place.run.mep.entity.*;
import place.run.mep.repository.*;

import java.math.BigDecimal;
import java.math.RoundingMode;
import java.time.LocalDateTime;
import java.util.List;
import java.util.Map;
import java.util.Optional;
import java.util.stream.Collectors;

@Service
@RequiredArgsConstructor
public class QuizService {

    private final UserRepository userRepository;
    private final SubUnitRepository subUnitRepository;
    private final QuizRepository quizRepository;
    private final QuizResponseRepository quizResponseRepository;
    private final QuizOptionRepository quizOptionRepository;
    private final ExplanationRepository explanationRepository;
    private final ProgressService progressService;

    @Transactional(readOnly = true)
    public List<QuizStateDto> getQuizStateForUser(String userId, Integer subUnitId) {
        User user = userRepository.findByUserId(userId)
                .orElseThrow(() -> new RuntimeException("사용자를 찾을 수 없습니다."));
        SubUnit subUnit = subUnitRepository.findById(subUnitId)
                .orElseThrow(() -> new RuntimeException("소단원을 찾을 수 없습니다."));

        List<Quiz> allQuizzes = quizRepository.findBySubUnit(subUnit);
        Map<Integer, QuizResponse> userResponsesMap = quizResponseRepository.findByUserAndQuiz_SubUnit(user, subUnit)
                .stream()
                .collect(Collectors.toMap(response -> response.getQuiz().getQuizId(), response -> response));

        return allQuizzes.stream()
                .map(quiz -> {
                    QuizResponse userResponse = userResponsesMap.get(quiz.getQuizId());
                    String commentary = explanationRepository.findByQuiz(quiz)
                                        .map(Explanation::getCommentary)
                                        .orElse(null);
                    return new QuizStateDto(quiz, userResponse, commentary);
                })
                .collect(Collectors.toList());
    }

    @Transactional
    public QuizAnswerResponseDto submitAndGradeSingleAnswer(String userId, QuizAnswerRequestDto req) {
        // 1. 필요한 정보 조회
        User user = userRepository.findByUserId(userId).orElseThrow(() -> new RuntimeException("사용자를 찾을 수 없습니다."));
        Quiz quiz = quizRepository.findById(req.getQuizId()).orElseThrow(() -> new RuntimeException("퀴즈를 찾을 수 없습니다."));
        SubUnit subUnit = quiz.getSubUnit();
        QuizOption selectedOption = quizOptionRepository.findById(req.getSelectedOptionId()).orElseThrow(() -> new RuntimeException("선택지를 찾을 수 없습니다."));

        // 2. 정답 여부 채점 및 정답 옵션 ID 확인
        boolean isCorrect = selectedOption.getIsAnswer();
        QuizOption correctOption = quiz.getOptions().stream()
                .filter(QuizOption::getIsAnswer)
                .findFirst()
                .orElseThrow(() -> new RuntimeException("정답이 없는 퀴즈입니다: " + quiz.getQuizId()));

        // 3. 사용자의 응답 기록을 DB에 저장 또는 갱신
        QuizResponse response = quizResponseRepository.findByUserAndQuiz(user, quiz)
                .orElse(new QuizResponse(quiz, user, selectedOption, isCorrect, LocalDateTime.now()));

        response.setQuizOption(selectedOption);
        response.setIsCorrect(isCorrect);
        response.setAnsweredAt(LocalDateTime.now());
        quizResponseRepository.save(response);

        // 4. 퀴즈 완료율 기반으로 진행도 업데이트
        long answeredCount = quizResponseRepository.countByUserAndQuiz_SubUnit(user, subUnit);
        long totalCount = quizRepository.countBySubUnit(subUnit);
        BigDecimal completionRatio = BigDecimal.valueOf(answeredCount)
                .divide(BigDecimal.valueOf(totalCount), 4, RoundingMode.HALF_UP);

        progressService.updateQuizProgressByCompletion(user, subUnit, completionRatio);

        // 5. 해설 조회 및 최종 결과 반환
        String commentary = explanationRepository.findByQuiz(quiz)
                .map(Explanation::getCommentary)
                .orElse("해설 정보가 없습니다.");

        return new QuizAnswerResponseDto(isCorrect, commentary, correctOption.getOptionId());
    }
}