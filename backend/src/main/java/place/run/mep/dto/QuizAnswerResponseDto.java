package place.run.mep.dto;

import lombok.AllArgsConstructor;
import lombok.Getter;

@Getter
@AllArgsConstructor
public class QuizAnswerResponseDto {
    private boolean isCorrect;
    private String commentary;
    private Integer correctOptionId;
}