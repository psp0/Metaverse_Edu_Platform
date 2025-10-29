package place.run.mep.dto;

import lombok.AllArgsConstructor;
import lombok.Getter;

@Getter
@AllArgsConstructor
public class CustomProblemResultDto {
    private String problemId;
    private String userAnswer;
    private boolean isCorrect;
}