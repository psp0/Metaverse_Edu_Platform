package place.run.mep.entity;

import jakarta.persistence.*;
import lombok.Getter;
import lombok.NoArgsConstructor;
import java.util.ArrayList;
import java.util.List;

@Entity
@Table(name = "sub_unit_group")
@Getter
@NoArgsConstructor
public class SubUnitGroup {
    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    @Column(name = "sub_unit_group_id")
    private Integer subUnitGroupId;

    @ManyToOne(fetch = FetchType.LAZY)
    @JoinColumn(name = "unit_id", nullable = false)
    private Unit unit;

    @Column(name = "subunit_title", nullable = false)
    private String subunitTitle;

    @OneToMany(mappedBy = "subUnitGroup", fetch = FetchType.LAZY, cascade = CascadeType.ALL)
    @OrderBy("subUnitId ASC")
    private List<SubUnit> subUnits = new ArrayList<>();
}