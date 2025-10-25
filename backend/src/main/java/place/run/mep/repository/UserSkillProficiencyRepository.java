package place.run.mep.repository;

import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.data.jpa.repository.Query;
import org.springframework.data.repository.query.Param;
import place.run.mep.entity.SubUnitGroup;
import place.run.mep.entity.User;
import place.run.mep.entity.UserSkillProficiency;

import java.util.List;
import java.util.Optional;

public interface UserSkillProficiencyRepository extends JpaRepository<UserSkillProficiency, Long> {

    Optional<UserSkillProficiency> findByUserAndSubUnitGroup(User user, SubUnitGroup subUnitGroup);


    @Query("SELECT p FROM UserSkillProficiency p " +
            "JOIN FETCH p.subUnitGroup sg " +
            "JOIN FETCH sg.unit u " +
            "WHERE p.user = :user AND u.subject.subjectId = :subjectId")
    List<UserSkillProficiency> findAllByUserAndSubjectWithDetails(@Param("user") User user, @Param("subjectId") int subjectId);
}