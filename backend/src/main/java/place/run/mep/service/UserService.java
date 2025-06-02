package place.run.mep.service;

import place.run.mep.dto.RegisterRequestDto;
import place.run.mep.dto.UserInfoDto;
import place.run.mep.dto.PagedUserResponseDto; // Added
import org.springframework.data.domain.Pageable; // Added

public interface UserService {
    UserInfoDto getUserInfo(String userId);
    PagedUserResponseDto getAllUsers(Pageable pageable); // Added
}
