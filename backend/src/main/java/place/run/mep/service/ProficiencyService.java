package place.run.mep.service;

import lombok.RequiredArgsConstructor;
import org.springframework.stereotype.Service;
import org.springframework.transaction.annotation.Transactional;
import place.run.mep.entity.*;
import place.run.mep.repository.QuizResponseRepository;
import place.run.mep.repository.UserSkillProficiencyRepository;

import java.math.BigDecimal;
import java.time.LocalDateTime;
import java.util.List;

@Service
@Transactional
@RequiredArgsConstructor
public class ProficiencyService {

    private final QuizResponseRepository quizResponseRepository;
    private final UserSkillProficiencyRepository userSkillProficiencyRepository;

    public void calculateAndSaveInitialGroupProficiency(User user, SubUnitGroup group) {
        List<SubUnit> subUnitsInGroup = group.getSubUnits();
        if (subUnitsInGroup.isEmpty()) {
            return;
        }

        List<Integer> quizIds = subUnitsInGroup.stream()
                .flatMap(subUnit -> subUnit.getQuizzes().stream())
                .map(Quiz::getQuizId)
                .toList();

        long totalCorrectAnswers = quizResponseRepository.countByUserAndQuiz_QuizIdInAndIsCorrect(user, quizIds, true);
        long totalQuizzesInGroup = quizIds.size();

        double averageCorrectRate = (totalQuizzesInGroup > 0) ? ((double) totalCorrectAnswers / totalQuizzesInGroup) * 100.0 : 0.0;

        BigDecimal initialProficiency = convertRateToProficiency(averageCorrectRate);

        UserSkillProficiency proficiency = userSkillProficiencyRepository.findByUserAndSubUnitGroup(user, group)
                .orElseGet(UserSkillProficiency::new);

        proficiency.setUser(user);
        proficiency.setSubUnitGroup(group);
        proficiency.setProficiencyPercentage(initialProficiency);
        proficiency.setUpdatedAt(LocalDateTime.now());

        userSkillProficiencyRepository.save(proficiency);
    }

    private BigDecimal convertRateToProficiency(double rate) {
        if (rate >= 80) return new BigDecimal("40.00");
        if (rate >= 60) return new BigDecimal("35.00");
        if (rate >= 40) return new BigDecimal("30.00");
        if (rate >= 20) return new BigDecimal("15.00");
        return new BigDecimal("5.00");
    }
}