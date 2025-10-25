package place.run.mep.entity;

import jakarta.persistence.*;
import lombok.Getter;
import lombok.NoArgsConstructor;
import java.util.List;

@Entity
@Table(name = "sub_unit")
@Getter
@NoArgsConstructor
public class SubUnit {
    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    @Column(name = "sub_unit_id")
    private Integer subUnitId;

    @ManyToOne(fetch = FetchType.LAZY)
    @JoinColumn(name = "sub_unit_group_id", nullable = false)
    private SubUnitGroup subUnitGroup;

    @Column(name = "detail")
    private String detail;

    @OneToOne(mappedBy = "subUnit", cascade = CascadeType.ALL, fetch = FetchType.LAZY)
    private Lecture lecture;

    @OneToMany(mappedBy = "subUnit", cascade = CascadeType.ALL, fetch = FetchType.LAZY)
    private List<Quiz> quizzes;
}