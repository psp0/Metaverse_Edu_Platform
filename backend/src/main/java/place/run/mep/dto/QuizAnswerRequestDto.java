package place.run.mep.dto;

import lombok.Getter;
import lombok.NoArgsConstructor;

@Getter
@NoArgsConstructor
public class QuizAnswerRequestDto {
    private Integer quizId;
    private Integer selectedOptionId;
}