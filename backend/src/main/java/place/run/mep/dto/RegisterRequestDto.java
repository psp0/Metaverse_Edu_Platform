package place.run.mep.dto;

import lombok.Getter;
import lombok.Setter;

import java.time.LocalDate;

@Getter
@Setter
public class RegisterRequestDto {
    private String userId;
    private String password;
    private String confirmPassword;
    private String email;
    private String phone;
    private String name;
    private String nickname;
    private LocalDate birthDate;
    private String gender;
}
