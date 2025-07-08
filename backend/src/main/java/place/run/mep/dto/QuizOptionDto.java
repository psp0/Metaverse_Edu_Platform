package place.run.mep.dto;

import lombok.Getter;
import place.run.mep.entity.QuizOption;

@Getter
public class QuizOptionDto {
    private final Integer optionId;
    private final String label;
    private final String optionText;

    public QuizOptionDto(QuizOption quizOption) {
        this.optionId = quizOption.getOptionId();
        this.label = quizOption.getLabel();
        this.optionText = quizOption.getOptionText();
    }
}