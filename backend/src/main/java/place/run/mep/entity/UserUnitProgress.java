package place.run.mep.entity;

import jakarta.persistence.*;
import lombok.Getter;
import lombok.NoArgsConstructor;
import lombok.Setter;
import java.math.BigDecimal;
import java.time.LocalDateTime;

@Entity
@Table(name = "user_unit_progress",
        uniqueConstraints = @UniqueConstraint(columnNames = {"user_no", "sub_unit_id"}))
@Getter
@Setter
@NoArgsConstructor
public class UserUnitProgress {
    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    @Column(name = "uup_id")
    private Long uupId;

    @Column(name = "progress_percentage", precision = 5, scale = 2)
    private BigDecimal progressPercentage;

    @Column(name = "lecture_progress", precision = 5, scale = 2)
    private BigDecimal lectureProgress;

    @Column(name = "quiz_progress", precision = 5, scale = 2)
    private BigDecimal quizProgress;

    @Column(name = "lecture_last_timestamp_sec")
    private Integer lectureLastTimestampSec;

    @Column(name = "updated_at")
    private LocalDateTime updatedAt;

    @ManyToOne(fetch = FetchType.LAZY)
    @JoinColumn(name = "user_no")
    private User user;

    @ManyToOne(fetch = FetchType.LAZY)
    @JoinColumn(name = "sub_unit_id")
    private SubUnit subUnit;

    public UserUnitProgress(User user, SubUnit subUnit) {
        this.user = user;
        this.subUnit = subUnit;
        this.progressPercentage = BigDecimal.ZERO;
        this.lectureProgress = BigDecimal.ZERO;
        this.quizProgress = BigDecimal.ZERO;
        this.lectureLastTimestampSec = 0;
        this.updatedAt = LocalDateTime.now();
    }
}