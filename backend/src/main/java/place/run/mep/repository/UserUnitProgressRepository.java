package place.run.mep.repository;

import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.data.jpa.repository.Query;
import org.springframework.data.repository.query.Param;
import place.run.mep.entity.SubUnit;
import place.run.mep.entity.User;
import place.run.mep.entity.UserUnitProgress;

import java.util.List;
import java.util.Optional;

public interface UserUnitProgressRepository extends JpaRepository<UserUnitProgress, Long> {

    Optional<UserUnitProgress> findByUserAndSubUnit(User user, SubUnit subUnit);

    @Query("SELECT p FROM UserUnitProgress p " +
            "JOIN FETCH p.subUnit su " +
            "JOIN FETCH su.subUnitGroup sg " +
            "JOIN FETCH sg.unit u " +
            "WHERE p.user = :user AND u.subject.subjectId = :subjectId")
    List<UserUnitProgress> findAllByUserAndSubjectWithDetails(@Param("user") User user, @Param("subjectId") int subjectId);


    @Query("SELECT p FROM UserUnitProgress p " +
            "JOIN p.subUnit su " +
            "WHERE p.user = :user AND su.subUnitGroup.id = :groupId")
    List<UserUnitProgress> findAllByUserAndSubUnitGroup(@Param("user") User user, @Param("groupId") Integer groupId);
}