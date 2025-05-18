package place.run.mep.dto;

import lombok.Builder;
import lombok.Getter;
import lombok.Setter;

import java.time.LocalDateTime;

@Getter
@Setter
@Builder
public class UserListInfoResponseDto {
    private Long userNo;
    private String userId;
    private String email;
    private String nickname;
    private LocalDateTime createdAt;
}
