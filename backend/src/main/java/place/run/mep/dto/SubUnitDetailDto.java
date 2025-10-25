package place.run.mep.dto;

import com.fasterxml.jackson.annotation.JsonPropertyOrder;
import lombok.Getter;
import place.run.mep.entity.SubUnit;
import place.run.mep.entity.UserUnitProgress;
import java.math.BigDecimal;
import java.time.LocalDateTime;

@Getter
@JsonPropertyOrder({ "subUnitId", "detail", "completed", "progressPercentage", "lectureProgress", "quizProgress", "lastStudiedAt", "lectureLastTimestampSec", "contentUrl" })
public class SubUnitDetailDto {
    private final int subUnitId;
    private final String detail;
    private final boolean completed;
    private final BigDecimal progressPercentage;
    private final BigDecimal lectureProgress;
    private final BigDecimal quizProgress;
    private final LocalDateTime lastStudiedAt;
    private final Integer lectureLastTimestampSec;
    private final String contentUrl;

    public SubUnitDetailDto(SubUnit subUnit, UserUnitProgress progress) {
        this.subUnitId = subUnit.getSubUnitId();
        this.detail = subUnit.getDetail();

        // 진행도 정보가 있는경우
        if (progress != null) {
            this.progressPercentage = progress.getProgressPercentage();
            this.lectureProgress = progress.getLectureProgress();
            this.quizProgress = progress.getQuizProgress();
            this.lastStudiedAt = progress.getUpdatedAt();
            this.lectureLastTimestampSec = progress.getLectureLastTimestampSec();
            this.completed = progress.getProgressPercentage() != null && progress.getProgressPercentage().compareTo(new BigDecimal("100.00")) >= 0;
        } else {
            this.progressPercentage = BigDecimal.ZERO;
            this.lectureProgress = BigDecimal.ZERO;
            this.quizProgress = BigDecimal.ZERO;
            this.lastStudiedAt = null;
            this.lectureLastTimestampSec = 0;
            this.completed = false;
        }

        this.contentUrl = (subUnit.getLecture() != null) ? subUnit.getLecture().getContentUrl() : null;
    }
}