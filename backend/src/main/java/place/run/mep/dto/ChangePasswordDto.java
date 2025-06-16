package place.run.mep.dto;

import jakarta.validation.constraints.NotBlank;
import lombok.Getter;
import lombok.Setter;

@Getter
@Setter
public class ChangePasswordDto {

    @NotBlank(message = "현재 비밀번호는 필수입니다.")
    private String currentPassword;

    @NotBlank(message = "새 비밀번호는 필수입니다.")
    private String newPassword;
}
