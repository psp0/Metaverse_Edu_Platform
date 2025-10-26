package place.run.mep.entity;

import jakarta.persistence.*;
import lombok.Getter;
import lombok.NoArgsConstructor;
import lombok.Setter;
import java.math.BigDecimal;
import java.time.LocalDateTime;

@Entity
@Table(name = "user_skill_proficiency")
@Getter
@Setter
@NoArgsConstructor
public class UserSkillProficiency {
    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    @Column(name = "proficiency_id")
    private Long proficiencyId;

    @ManyToOne(fetch = FetchType.LAZY)
    @JoinColumn(name = "user_no", nullable = false)
    private User user;

    @ManyToOne(fetch = FetchType.LAZY)
    @JoinColumn(name = "sub_unit_group_id", nullable = false)
    private SubUnitGroup subUnitGroup;

    @Column(name = "proficiency_percentage", nullable = false, precision = 5, scale = 2)
    private BigDecimal proficiencyPercentage;

    @Column(name = "updated_at", nullable = false)
    private LocalDateTime updatedAt;
}