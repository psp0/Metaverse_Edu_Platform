package place.run.mep.repository;

import org.springframework.data.jpa.repository.JpaRepository;
import place.run.mep.entity.Explanation;
import place.run.mep.entity.Quiz;
import java.util.Optional;

public interface ExplanationRepository extends JpaRepository<Explanation, Integer> {
    Optional<Explanation> findByQuiz(Quiz quiz);
}