package place.run.mep.dto;

import lombok.AllArgsConstructor;
import lombok.Getter;
import java.math.BigDecimal;

@Getter
@AllArgsConstructor
public class QuizFinalSubmitResponseDto {
    private int totalQuestions;
    private int correctAnswers;
    private BigDecimal proficiencyPercentage;
}