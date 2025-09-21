package place.run.mep.repository;

import org.springframework.data.jpa.repository.JpaRepository;
import place.run.mep.entity.Quiz;
import place.run.mep.entity.QuizResponse;
import place.run.mep.entity.SubUnit;
import place.run.mep.entity.User;
import java.util.List;
import java.util.Optional;

public interface QuizResponseRepository extends JpaRepository<QuizResponse, Integer> {
    List<QuizResponse> findByUserAndQuiz_SubUnit(User user, SubUnit subUnit);
    long countByUserAndQuiz_SubUnit(User user, SubUnit subUnit);
    Optional<QuizResponse> findByUserAndQuiz(User user, Quiz quiz);
}