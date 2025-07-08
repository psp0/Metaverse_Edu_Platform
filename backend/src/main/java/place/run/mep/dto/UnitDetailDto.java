package place.run.mep.dto;

import com.fasterxml.jackson.annotation.JsonPropertyOrder;
import lombok.Getter;
import place.run.mep.entity.Unit;
import java.util.List;

@Getter
@JsonPropertyOrder({ "unitId", "chapterNo", "title", "description", "subUnits" })
public class UnitDetailDto {
    private final int unitId;
    private final int chapterNo;
    private final String title;
    private final String description;
    private final List<SubUnitDetailDto> subUnits;

    public UnitDetailDto(Unit unit, List<SubUnitDetailDto> subUnits) {
        this.unitId = unit.getUnitId();
        this.chapterNo = unit.getChapterNo();
        this.title = unit.getTitle();
        this.description = unit.getDescription();
        this.subUnits = subUnits;
    }
}