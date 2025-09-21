package place.run.mep.repository;
import org.springframework.data.jpa.repository.JpaRepository;
import place.run.mep.entity.Lecture;
import place.run.mep.entity.SubUnit;
import java.util.Optional;
public interface LectureRepository extends JpaRepository<Lecture, Integer> {
    Optional<Lecture> findBySubUnit(SubUnit subUnit);
}