package place.run.mep.entity;

import jakarta.persistence.*;
import lombok.Getter;
import lombok.NoArgsConstructor;

@Entity
@Table(name = "lecture")
@Getter
@NoArgsConstructor
public class Lecture {
    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    @Column(name = "lecture_id")
    private Integer lectureId;

    @OneToOne(fetch = FetchType.LAZY)
    @JoinColumn(name = "sub_unit_id")
    private SubUnit subUnit;

    @Column(nullable = false)
    private String title;

    @Column(name = "content_url", columnDefinition = "TEXT")
    private String contentUrl;

    @Column(name = "duration_sec", nullable = false)
    private Integer durationSec;
}