package place.run.mep.service;

import lombok.RequiredArgsConstructor;
import org.springframework.stereotype.Service;
import org.springframework.transaction.annotation.Transactional;
import place.run.mep.dto.SubUnitDetailDto;
import place.run.mep.dto.SubUnitGroupDto;
import place.run.mep.dto.UnitDetailDto;
import place.run.mep.entity.*;
import place.run.mep.repository.SubjectRepository;
import place.run.mep.repository.UserRepository;
import place.run.mep.repository.UserSkillProficiencyRepository;
import place.run.mep.repository.UserUnitProgressRepository;

import java.math.BigDecimal;
import java.util.List;
import java.util.Map;
import java.util.stream.Collectors;

@Service
@Transactional(readOnly = true)
@RequiredArgsConstructor
public class ClassroomService {

    private final UserRepository userRepository;
    private final SubjectRepository subjectRepository;
    private final UserUnitProgressRepository userUnitProgressRepository;
    private final UserSkillProficiencyRepository userSkillProficiencyRepository;

    public List<UnitDetailDto> getSubjectDetails(String userId, int subjectId) {
        User user = userRepository.findByUserId(userId)
                .orElseThrow(() -> new RuntimeException("사용자를 찾을 수 없습니다."));

        Subject subject = subjectRepository.findById(subjectId)
                .orElseThrow(() -> new RuntimeException("과목을 찾을 수 없습니다."));

        // 사용자의 모든 진행도와 숙달도 정보를 미리 한번에 조회하여 Map으로 변환
        Map<Integer, UserUnitProgress> progressMap = userUnitProgressRepository.findAllByUserAndSubjectWithDetails(user, subjectId)
                .stream().collect(Collectors.toMap(p -> p.getSubUnit().getSubUnitId(), p -> p));

        Map<Integer, UserSkillProficiency> proficiencyMap = userSkillProficiencyRepository.findAllByUserAndSubjectWithDetails(user, subjectId)
                .stream().collect(Collectors.toMap(p -> p.getSubUnitGroup().getSubUnitGroupId(), p -> p));


        return subject.getUnits().stream()
                .map(unit -> {
                    List<SubUnitGroupDto> subUnitGroupDtos = unit.getSubUnitGroups().stream()
                            .map(group -> {
                                // 1. 각 세부 개념(SubUnit)에 대한 DTO 리스트 생성
                                List<SubUnitDetailDto> subUnitDetailDtos = group.getSubUnits().stream()
                                        .map(subUnit -> new SubUnitDetailDto(subUnit, progressMap.get(subUnit.getSubUnitId())))
                                        .collect(Collectors.toList());

                                // 2. 맞춤 문제 활성화 여부(customProblemAvailable) 계산
                                // 그룹 내 모든 세부 개념의 퀴즈 진행도가 100인지 확인
                                boolean allQuizzesCompleted = subUnitDetailDtos.stream()
                                        .allMatch(dto -> dto.getQuizProgress().compareTo(new BigDecimal("100.00")) == 0);

                                // 3. 그룹의 숙달도 정보 조회
                                UserSkillProficiency proficiency = proficiencyMap.get(group.getSubUnitGroupId());
                                BigDecimal proficiencyPercentage = (proficiency != null) ? proficiency.getProficiencyPercentage() : BigDecimal.ZERO;

                                // SubUnitGroupDto 생성
                                return new SubUnitGroupDto(group.getSubUnitGroupId(), group.getSubunitTitle(), allQuizzesCompleted, subUnitDetailDtos, proficiencyPercentage);
                            })
                            .collect(Collectors.toList());

                    // UnitDetailDto 생성
                    return new UnitDetailDto(unit, subUnitGroupDtos);
                })
                .collect(Collectors.toList());
    }
}