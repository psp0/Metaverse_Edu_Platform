package place.run.mep.dto;

import com.fasterxml.jackson.annotation.JsonInclude;
import lombok.Getter;
import place.run.mep.entity.Explanation;
import place.run.mep.entity.Quiz;
import place.run.mep.entity.QuizOption;
import place.run.mep.entity.QuizResponse;

import java.util.List;
import java.util.stream.Collectors;

@Getter
@JsonInclude(JsonInclude.Include.NON_NULL) // 필드가 null이면 JSON에서 제외
public class QuizDetailDto {
    private final Integer quizId;
    private final String content;
    private final List<QuizOptionDto> options;

    // --- 아래 필드들은 사용자가 푼 문제에만 값이 채워짐 ---
    private final Integer userSelectedOptionId;
    private final Boolean isCorrect;
    private final Integer correctOptionId;
    private final String commentary;

    // 1: 아직 풀지 않은 퀴즈용
    public QuizDetailDto(Quiz quiz) {
        this.quizId = quiz.getQuizId();
        this.content = quiz.getContent();
        this.options = quiz.getOptions().stream().map(QuizOptionDto::new).collect(Collectors.toList());
        this.userSelectedOptionId = null;
        this.isCorrect = null;
        this.correctOptionId = null;
        this.commentary = null;
    }

    // 2: 이미 푼 퀴즈용
    public QuizDetailDto(Quiz quiz, QuizResponse userResponse) {
        this.quizId = quiz.getQuizId();
        this.content = quiz.getContent();
        this.options = quiz.getOptions().stream().map(QuizOptionDto::new).collect(Collectors.toList());
        
        this.userSelectedOptionId = userResponse.getQuizOption().getOptionId();
        this.isCorrect = userResponse.getIsCorrect();

        this.correctOptionId = quiz.getOptions().stream()
                .filter(QuizOption::getIsAnswer)
                .findFirst()
                .map(QuizOption::getOptionId)
                .orElse(null);

        Explanation explanation = quiz.getExplanation();
        this.commentary = (explanation != null) ? explanation.getCommentary() : "해설이 제공되지 않는 문제입니다.";
    }
}