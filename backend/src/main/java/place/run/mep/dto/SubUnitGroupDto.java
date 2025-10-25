package place.run.mep.dto;

import lombok.Getter;
import java.math.BigDecimal;
import java.util.List;

@Getter
public class SubUnitGroupDto {
    private final int subUnitGroupId;
    private final String subunitTitle;
    private final boolean customProblemAvailable;
    private final BigDecimal proficiencyPercentage;
    private final List<SubUnitDetailDto> subUnits;

    public SubUnitGroupDto(int subUnitGroupId, String subunitTitle, boolean customProblemAvailable, List<SubUnitDetailDto> subUnits, BigDecimal proficiencyPercentage) {
        this.subUnitGroupId = subUnitGroupId;
        this.subunitTitle = subunitTitle;
        this.customProblemAvailable = customProblemAvailable;
        this.subUnits = subUnits;
        this.proficiencyPercentage = proficiencyPercentage;
    }
}