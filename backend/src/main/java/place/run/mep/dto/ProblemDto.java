package place.run.mep.dto;

import lombok.Getter;
import lombok.NoArgsConstructor;
import lombok.Setter;

@Getter
@Setter
@NoArgsConstructor
public class ProblemDto {
    private Long problemId; // AI 문제의 고유 ID
    private Integer level;
    private String content;
}