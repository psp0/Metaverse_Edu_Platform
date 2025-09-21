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
    @JoinColumn(name = "unit_id")
    private Unit unit;

    @Column(name = "sub_chapter_no")
    private Integer subChapterNo;

    @Column(name = "sub_title", nullable = false)
    private String subTitle;

    @Column(columnDefinition = "TEXT")
    private String description;

    @OneToOne(mappedBy = "subUnit", cascade = CascadeType.ALL, fetch = FetchType.LAZY)
    private Lecture lecture;

    @OneToMany(mappedBy = "subUnit", cascade = CascadeType.ALL, fetch = FetchType.LAZY)
    private List<Quiz> quizzes;
}