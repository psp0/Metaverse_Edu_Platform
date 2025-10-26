package place.run.mep.dto;

import lombok.AllArgsConstructor;
import lombok.Getter;

@Getter
@AllArgsConstructor
public class CustomProblemResultDto {
    private Long problemId;
    private String userAnswer;
    private boolean isCorrect;
    private String explanation;
}