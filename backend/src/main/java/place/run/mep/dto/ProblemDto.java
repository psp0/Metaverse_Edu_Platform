package place.run.mep.dto;

import com.fasterxml.jackson.annotation.JsonProperty;
import lombok.Getter;
import lombok.NoArgsConstructor;
import lombok.Setter;
import lombok.extern.slf4j.Slf4j;

import java.util.Collections;
import java.util.List;
import java.util.regex.Matcher;
import java.util.regex.Pattern;
import java.util.stream.Collectors;

@Getter
@Setter
@NoArgsConstructor
@Slf4j
public class ProblemDto {

    @JsonProperty("id")
    private String problemId;

    @JsonProperty("textbody")
    private String content;

    private List<String> optionsList;

    private String answer;

    private Integer level;

    // 'options' 문자열을 받아서 List<String>으로 변환하는 Setter
    @JsonProperty("options")
    public void setOptions(String optionsString) {
        if (optionsString == null || optionsString.trim().isEmpty() || optionsString.equals("[]")) {
            this.optionsList = Collections.emptyList(); // 빈 리스트
        } else {
            Pattern pattern = Pattern.compile("'([^']*)'");
            Matcher matcher = pattern.matcher(optionsString);
            this.optionsList = matcher.results()
                    .map(match -> match.group(1))
                    .collect(Collectors.toList());
        }
    }

    // 'level' 문자열을 받아서 Integer로 변환하는 Setter
    @JsonProperty("level")
    public void setLevel(String levelString) {
        try {
            this.level = Integer.parseInt(levelString);
        } catch (NumberFormatException e) {
            log.error("Warning: Could not parse level '{}' for problemId: {}. Defaulting to 0.", levelString, this.problemId);
            this.level = 0; // 파싱 실패 시 기본값
        }
    }

}