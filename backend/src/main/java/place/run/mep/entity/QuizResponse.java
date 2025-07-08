package place.run.mep.entity;

import jakarta.persistence.*;
import java.time.LocalDateTime;
import lombok.Getter;
import lombok.NoArgsConstructor;
import lombok.Setter;

@Entity
@Table(name = "quiz_response")
@Getter
@Setter
@NoArgsConstructor
public class QuizResponse {
    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    @Column(name = "response_id")
    private Integer responseId;

    @ManyToOne(fetch = FetchType.LAZY)
    @JoinColumn(name = "user_no")
    private User user;

    @ManyToOne(fetch = FetchType.LAZY)
    @JoinColumn(name = "quiz_id")
    private Quiz quiz;

    @ManyToOne(fetch = FetchType.LAZY)
    @JoinColumn(name = "option_id")
    private QuizOption quizOption;

    @Column(name = "is_correct")
    private Boolean isCorrect;

    @Column(name = "answered_at", nullable = false)
    private LocalDateTime answeredAt;

    public QuizResponse(Quiz quiz, User user, QuizOption quizOption, Boolean isCorrect, LocalDateTime answeredAt) {
        this.quiz = quiz;
        this.user = user;
        this.quizOption = quizOption;
        this.isCorrect = isCorrect;
        this.answeredAt = answeredAt;
    }
}