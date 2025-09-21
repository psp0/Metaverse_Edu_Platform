package place.run.mep.entity;

import jakarta.persistence.*;
import lombok.Getter;
import lombok.NoArgsConstructor;

@Entity
@Table(name = "explanation")
@Getter
@NoArgsConstructor
public class Explanation {
    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    @Column(name = "explanation_id")
    private Integer explanationId;

    @OneToOne(fetch = FetchType.LAZY)
    @JoinColumn(name = "quiz_id", unique = true)
    private Quiz quiz;

    @Column(nullable = false, columnDefinition = "TEXT")
    private String commentary;
}