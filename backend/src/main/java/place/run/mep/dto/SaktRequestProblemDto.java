package place.run.mep.dto;

import lombok.AllArgsConstructor;
import lombok.Getter;

@Getter
@AllArgsConstructor
public class SaktRequestProblemDto {
    private String problemId;
    private int isCorrect; // 정답 1, 오답 0
}