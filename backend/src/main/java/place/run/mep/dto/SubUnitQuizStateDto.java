package place.run.mep.dto;

import lombok.AllArgsConstructor;
import lombok.Getter;
import java.util.List;

@Getter
@AllArgsConstructor
public class SubUnitQuizStateDto {
    private int subUnitId;
    private int totalQuizCount;
    private int solvedQuizCount;
    private int correctQuizCount;
    private List<QuizDetailDto> quizzes;
}