package place.run.mep.dto;

import lombok.Getter;
import lombok.NoArgsConstructor;
import lombok.Setter;

@Getter
@Setter
@NoArgsConstructor
public class ProblemSolutionDto {
    private Long problemId;
    private String correctAnswer;
    private String explanation;
}