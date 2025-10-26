package place.run.mep.dto;

import lombok.AllArgsConstructor;
import lombok.Getter;
import java.math.BigDecimal;

@Getter
@AllArgsConstructor
public class ProficiencyUpdateResponseDto {
    private Integer subUnitId;
    private BigDecimal newProficiencyPercentage; // SAKT 모델이 계산한 새로운 숙달도
}