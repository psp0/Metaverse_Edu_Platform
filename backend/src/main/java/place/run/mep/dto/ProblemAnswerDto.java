package place.run.mep.dto;

import lombok.Getter;
import lombok.NoArgsConstructor;

@Getter
@NoArgsConstructor
public class ProblemAnswerDto {
    private Long problemId; // AI 문제 은행의 고유 ID
    private String userAnswer;
}