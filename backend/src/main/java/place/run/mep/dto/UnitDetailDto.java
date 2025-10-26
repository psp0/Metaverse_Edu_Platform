package place.run.mep.dto;

import lombok.Getter;
import place.run.mep.entity.Unit;
import java.util.List;

@Getter
public class UnitDetailDto {
    private final int unitId;
    private final int chapterNo;
    private final String title;


    private final List<SubUnitGroupDto> subUnitGroups;

    public UnitDetailDto(Unit unit, List<SubUnitGroupDto> subUnitGroups) {
        this.unitId = unit.getUnitId();
        this.chapterNo = unit.getChapterNo();
        this.title = unit.getTitle();
        this.subUnitGroups = subUnitGroups;
    }
}