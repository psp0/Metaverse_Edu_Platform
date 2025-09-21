package place.run.mep.entity;

import jakarta.persistence.*;
import lombok.Getter;
import lombok.NoArgsConstructor;
import lombok.Setter;

@Entity
@Table(name = "problem_response")
@Getter
@Setter
@NoArgsConstructor
public class ProblemResponse {
    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    @Column(name = "response_id")
    private Long responseId;

    @ManyToOne(fetch = FetchType.LAZY)
    @JoinColumn(name = "user_no")
    private User user;

    @Column(name = "problem_id", length = 50)
    private String problemId;

    @Column(columnDefinition = "TEXT")
    private String answer;

    @Column(name = "is_correct")
    private Boolean isCorrect;
}