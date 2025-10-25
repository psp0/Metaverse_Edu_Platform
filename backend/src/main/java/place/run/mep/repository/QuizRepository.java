package place.run.mep.repository;

import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.data.jpa.repository.Query;
import org.springframework.data.repository.query.Param;
import place.run.mep.entity.Quiz;
import place.run.mep.entity.SubUnit;
import java.util.List;

public interface QuizRepository extends JpaRepository<Quiz, Integer> {
    int countBySubUnit(SubUnit subUnit);
    List<Quiz> findBySubUnit(SubUnit subUnit);

    @Query("SELECT q FROM Quiz q LEFT JOIN FETCH q.options WHERE q.subUnit = :subUnit ORDER BY q.quizId ASC")
    List<Quiz> findBySubUnitWithOptions(@Param("subUnit") SubUnit subUnit);
}