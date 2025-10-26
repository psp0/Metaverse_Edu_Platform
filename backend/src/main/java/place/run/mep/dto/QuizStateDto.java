package place.run.mep.dto;

import com.fasterxml.jackson.annotation.JsonInclude;
import lombok.AllArgsConstructor;
import lombok.Getter;
import place.run.mep.entity.Quiz;
import java.util.List;
import java.util.stream.Collectors;

@Getter
@AllArgsConstructor
@JsonInclude(JsonInclude.Include.NON_NULL)
public class QuizStateDto {

    private int subUnitId;
    private int totalQuizCount;
    private int solvedQuizCount;
    private int correctQuizCount;

    private Integer quizId;
    private String content;
    private List<QuizOptionDto> options;

    public QuizStateDto(int subUnitId, int totalQuizCount, int solvedQuizCount, int correctQuizCount, Quiz nextQuiz) {
        this.subUnitId = subUnitId;
        this.totalQuizCount = totalQuizCount;
        this.solvedQuizCount = solvedQuizCount;
        this.correctQuizCount = correctQuizCount;

        if (nextQuiz != null) {
            this.quizId = nextQuiz.getQuizId();
            this.content = nextQuiz.getContent();
            this.options = nextQuiz.getOptions().stream()
                    .map(QuizOptionDto::new)
                    .collect(Collectors.toList());
        } else {
            this.quizId = null; // 이름 변경
            this.content = null;
            this.options = null;
        }
    }
}