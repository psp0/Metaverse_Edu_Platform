package place.run.mep.repository;

import org.springframework.data.jpa.repository.JpaRepository;
import place.run.mep.entity.Quiz;
import place.run.mep.entity.SubUnit;
import java.util.List;

public interface QuizRepository extends JpaRepository<Quiz, Integer> {
    int countBySubUnit(SubUnit subUnit);
    List<Quiz> findBySubUnit(SubUnit subUnit);
}