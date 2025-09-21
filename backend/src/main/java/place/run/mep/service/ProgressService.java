package place.run.mep.service;

import lombok.RequiredArgsConstructor;
import org.springframework.stereotype.Service;
import org.springframework.transaction.annotation.Transactional;
import place.run.mep.entity.Lecture;
import place.run.mep.entity.SubUnit;
import place.run.mep.entity.User;
import place.run.mep.entity.UserUnitProgress;
import place.run.mep.repository.LectureRepository;
import place.run.mep.repository.SubUnitRepository;
import place.run.mep.repository.UserRepository;
import place.run.mep.repository.UserUnitProgressRepository;
import java.math.BigDecimal;
import java.time.LocalDateTime;

@Service
@RequiredArgsConstructor
public class ProgressService {

    private final UserRepository userRepository;
    private final UserUnitProgressRepository userUnitProgressRepository;
    private final SubUnitRepository subUnitRepository;
    private final LectureRepository lectureRepository;

    /**
     * User와 SubUnit에 해당하는 UserUnitProgress를 찾거나, 없으면 새로 생성하여 반환합니다.
     */
    private UserUnitProgress getOrCreateProgress(User user, SubUnit subUnit) {
        return userUnitProgressRepository.findByUserAndSubUnit(user, subUnit)
                .orElse(new UserUnitProgress(user, subUnit));
    }

    /**
     * 강의와 퀴즈 진행도를 기반으로 최종 진행도를 계산하고 DB에 저장합니다.
     */
    private void updateTotalProgress(UserUnitProgress progress) {
        BigDecimal lectureScore = progress.getLectureProgress() != null ? progress.getLectureProgress() : BigDecimal.ZERO;
        BigDecimal quizScore = progress.getQuizProgress() != null ? progress.getQuizProgress() : BigDecimal.ZERO;

        // 강의 50%, 퀴즈 50% 가중치 적용
        BigDecimal lectureContribution = lectureScore.multiply(new BigDecimal("0.5"));
        BigDecimal quizContribution = quizScore.multiply(new BigDecimal("0.5"));
        BigDecimal total = lectureContribution.add(quizContribution);

        progress.setProgressPercentage(total);
        progress.setUpdatedAt(LocalDateTime.now());
        userUnitProgressRepository.save(progress);
    }
    
    /**
     * 강의 시청 진행도를 업데이트합니다.
     */
    @Transactional
    public void updateLectureProgress(String userId, Integer subUnitId, Integer lastTimestamp) {
        User user = userRepository.findByUserId(userId).orElseThrow(() -> new RuntimeException("사용자를 찾을 수 없습니다."));
        SubUnit subUnit = subUnitRepository.findById(subUnitId).orElseThrow(() -> new RuntimeException("소단원을 찾을 수 없습니다."));
        Lecture lecture = lectureRepository.findBySubUnit(subUnit)
                .orElseThrow(() -> new RuntimeException("해당 소단원의 강의 정보가 없습니다."));
        
        UserUnitProgress progress = getOrCreateProgress(user, subUnit);

        double watchedPercentage = ((double) lastTimestamp / lecture.getDurationSec()) * 100.0;

        if (watchedPercentage >= 95.0) {
            watchedPercentage = 100.0;
        }

        BigDecimal newLectureProgress = BigDecimal.valueOf(watchedPercentage);

        // 진행도는 후퇴하지 않도록, 기존 값보다 클 경우에만 갱신
        if (progress.getLectureProgress() == null || newLectureProgress.compareTo(progress.getLectureProgress()) > 0) {
            progress.setLectureProgress(newLectureProgress);
        }
        
        progress.setLectureLastTimestampSec(lastTimestamp);
        updateTotalProgress(progress);
    }

    /**
     * 퀴즈 완료율을 기반으로 퀴즈 진행도를 업데이트합니다. (QuizService에서 호출)
     */
    @Transactional
    public void updateQuizProgressByCompletion(User user, SubUnit subUnit, BigDecimal completionRatio) {
        UserUnitProgress progress = getOrCreateProgress(user, subUnit);
        
        BigDecimal quizProgress = completionRatio.multiply(new BigDecimal("100.0"));
        progress.setQuizProgress(quizProgress);

        updateTotalProgress(progress);
    }
}