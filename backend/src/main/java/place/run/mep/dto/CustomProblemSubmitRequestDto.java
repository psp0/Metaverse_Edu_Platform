package place.run.mep.dto;

import lombok.Getter;
import java.util.List;

@Getter
public class CustomProblemSubmitRequestDto {
    private int subUnitGroupId;
    private List<ProblemAnswerDto> answers;
}