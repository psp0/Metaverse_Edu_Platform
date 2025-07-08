package place.run.mep.repository;

import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.data.jpa.repository.Query;
import org.springframework.data.repository.query.Param;
import place.run.mep.entity.Unit;
import java.util.List;

public interface UnitRepository extends JpaRepository<Unit, Integer> {
    /**
     * 특정 과목 ID에 해당하는 모든 단원과, 그에 속한 소단원 및 강의 정보까지 함께 조회합니다.
     * chapterNo 순서로 정렬합니다.
     */
    @Query("SELECT DISTINCT u FROM Unit u " +
            "LEFT JOIN FETCH u.subUnits su " +
            "LEFT JOIN FETCH su.lecture " +
            "WHERE u.subject.subjectId = :subjectId " +
            "ORDER BY u.chapterNo ASC, su.subChapterNo ASC")
    List<Unit> findAllBySubjectIdWithSubUnitsAndLectures(@Param("subjectId") Integer subjectId);
}