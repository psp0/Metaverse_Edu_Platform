package place.run.mep.dto;

import com.fasterxml.jackson.annotation.JsonInclude;
import com.fasterxml.jackson.annotation.JsonPropertyOrder;
import lombok.Getter;
import place.run.mep.entity.SubUnit;
import place.run.mep.entity.UserUnitProgress;
import java.math.BigDecimal;
import java.time.LocalDateTime;

@Getter
@JsonInclude(JsonInclude.Include.NON_NULL)
@JsonPropertyOrder({ "subUnitId", "subChapterNo", "subTitle", "description", "completed", "progressPercentage", "lectureProgress", "quizProgress", "lastStudiedAt", "lectureLastTimestampSec" })
public class SubUnitDetailDto {
    private final int subUnitId;
    private final int subChapterNo;
    private final String subTitle;
    private final String description;
    private final boolean completed;
    private final BigDecimal progressPercentage;
    private final BigDecimal lectureProgress;
    private final BigDecimal quizProgress;
    private final LocalDateTime lastStudiedAt;
    private final Integer lectureLastTimestampSec;
    private final String contentUrl;

    // 학습 기록이 있는 경우
    public SubUnitDetailDto(SubUnit subUnit, UserUnitProgress progress) {
        this.subUnitId = subUnit.getSubUnitId();
        this.subChapterNo = subUnit.getSubChapterNo();
        this.subTitle = subUnit.getSubTitle();
        this.description = subUnit.getDescription();
        this.completed = progress.getProgressPercentage() != null && progress.getProgressPercentage().compareTo(new BigDecimal("100.00")) >= 0;
        this.progressPercentage = progress.getProgressPercentage();
        this.lectureProgress = progress.getLectureProgress();
        this.quizProgress = progress.getQuizProgress();
        this.lastStudiedAt = progress.getUpdatedAt();
        this.lectureLastTimestampSec = progress.getLectureLastTimestampSec();
        this.contentUrl = (subUnit.getLecture() != null) ? subUnit.getLecture().getContentUrl() : null;
    }

    // 학습 기록이 없는 경우
    public SubUnitDetailDto(SubUnit subUnit) {
        this.subUnitId = subUnit.getSubUnitId();
        this.subChapterNo = subUnit.getSubChapterNo();
        this.subTitle = subUnit.getSubTitle();
        this.description = subUnit.getDescription();
        this.completed = false;
        this.progressPercentage = BigDecimal.ZERO;
        this.lectureProgress = BigDecimal.ZERO;
        this.quizProgress = BigDecimal.ZERO;
        this.lastStudiedAt = null;
        this.lectureLastTimestampSec = 0;
        this.contentUrl = (subUnit.getLecture() != null) ? subUnit.getLecture().getContentUrl() : null;
    }
}