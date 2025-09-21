package place.run.mep.dto;

import com.fasterxml.jackson.annotation.JsonInclude;
import lombok.Getter;
import place.run.mep.entity.Quiz;
import place.run.mep.entity.QuizResponse;
import java.util.List;
import java.util.stream.Collectors;
import place.run.mep.entity.QuizOption;

@Getter
@JsonInclude(JsonInclude.Include.NON_NULL)
public class QuizStateDto {
    private final Integer quizId;
    private final String content;
    private final List<QuizOptionDto> options;

    private final Integer userSelectedOptionId;
    private final Boolean isCorrect;
    private final String commentary;
    private final Integer correctOptionId; // 정답 선택지 ID 필드 추가

    public QuizStateDto(Quiz quiz, QuizResponse userResponse, String commentary) {
        this.quizId = quiz.getQuizId();
        this.content = quiz.getContent();
        this.options = quiz.getOptions().stream()
                .map(QuizOptionDto::new)
                .collect(Collectors.toList());

        // 사용자의 응답 기록이 있을 때만 채점 결과, 해설, 정답 ID를 DTO에 담습니다.
        if (userResponse != null) {
            this.userSelectedOptionId = userResponse.getQuizOption().getOptionId();
            this.isCorrect = userResponse.getIsCorrect();
            this.commentary = commentary;
            // 푼 문제에 대해서만 정답 ID를 찾아서 포함
            this.correctOptionId = quiz.getOptions().stream()
                    .filter(QuizOption::getIsAnswer)
                    .findFirst()
                    .map(QuizOption::getOptionId)
                    .orElse(null);
        } else {
            // 풀지 않은 문제에는 개인화된 정보를 포함하지 않음
            this.userSelectedOptionId = null;
            this.isCorrect = null;
            this.commentary = null;
            this.correctOptionId = null;
        }
    }
}