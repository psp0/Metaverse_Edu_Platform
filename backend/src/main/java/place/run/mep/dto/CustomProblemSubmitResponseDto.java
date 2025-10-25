package place.run.mep.dto;

import lombok.AllArgsConstructor;
import lombok.Getter;

import java.math.BigDecimal;
import java.util.List;

@Getter
@AllArgsConstructor
public class CustomProblemSubmitResponseDto {
    private List<CustomProblemResultDto> results;
    private BigDecimal newProficiencyPercentage;
}