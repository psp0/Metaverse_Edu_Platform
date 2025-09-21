package place.run.mep.service;

import lombok.RequiredArgsConstructor;
import org.springframework.stereotype.Service;
import org.springframework.transaction.annotation.Transactional;
import place.run.mep.dto.SubUnitDetailDto;
import place.run.mep.dto.UnitDetailDto;
import place.run.mep.entity.Unit;
import place.run.mep.entity.User;
import place.run.mep.entity.UserUnitProgress;
import place.run.mep.repository.UnitRepository;
import place.run.mep.repository.UserRepository;
import place.run.mep.repository.UserUnitProgressRepository;

import java.util.List;
import java.util.Map;
import java.util.stream.Collectors;

@Service
@RequiredArgsConstructor
public class ClassroomService {

    private final UnitRepository unitRepository;
    private final UserRepository userRepository;
    private final UserUnitProgressRepository userUnitProgressRepository;

    @Transactional(readOnly = true)
    public List<UnitDetailDto> getUnitsWithProgress(String userId, Integer subjectId) {
        User currentUser = userRepository.findByUserId(userId)
                .orElseThrow(() -> new RuntimeException("사용자를 찾을 수 없습니다."));

        Map<Integer, UserUnitProgress> progressMap = userUnitProgressRepository.findAllByUser(currentUser)
                .stream()
                .collect(Collectors.toMap(p -> p.getSubUnit().getSubUnitId(), p -> p));

        List<Unit> unitsOfSubject = unitRepository.findAllBySubjectIdWithSubUnitsAndLectures(subjectId);

        return unitsOfSubject.stream().map(unit -> {
            List<SubUnitDetailDto> subUnitDtos = unit.getSubUnits().stream()
                    .map(subUnit -> {
                        UserUnitProgress progress = progressMap.get(subUnit.getSubUnitId());
                        return (progress != null)
                                ? new SubUnitDetailDto(subUnit, progress)
                                : new SubUnitDetailDto(subUnit);
                    })
                    .collect(Collectors.toList());

            return new UnitDetailDto(unit, subUnitDtos);
        }).collect(Collectors.toList());
    }
}