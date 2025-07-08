package place.run.mep.repository;

import org.springframework.data.jpa.repository.JpaRepository;
import place.run.mep.entity.SubUnit;
import place.run.mep.entity.User;
import place.run.mep.entity.UserUnitProgress;

import java.util.List;
import java.util.Optional;

public interface UserUnitProgressRepository extends JpaRepository<UserUnitProgress, Long> {
    // 특정 사용자의 모든 학습 진행 상황을 조회합니다.
    List<UserUnitProgress> findAllByUser(User user);

    // 특정 사용자의 특정 소단원에 대한 학습 진행 상황을 조회합니다.
    Optional<UserUnitProgress> findByUserAndSubUnit(User user, SubUnit subUnit);
}