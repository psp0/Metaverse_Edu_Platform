package place.run.mep.repository;

import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.data.jpa.repository.Query;
import org.springframework.data.repository.query.Param;
import place.run.mep.entity.Subject;

import java.util.Optional;

public interface SubjectRepository extends JpaRepository<Subject, Integer> {

}