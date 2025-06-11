package place.run.mep.service;

import place.run.mep.dto.ChangePasswordDto;
import place.run.mep.dto.UpdateUserDto;
import place.run.mep.dto.UserInfoDto;
import place.run.mep.dto.PagedUserResponseDto; // Added
import org.springframework.data.domain.Pageable; // Added

public interface UserService {
    UserInfoDto getUserInfo(String userId);
    PagedUserResponseDto getAllUsers(Pageable pageable);
    void updateUserInfo(String userId, UpdateUserDto dto);
    void changePassword(String userId, ChangePasswordDto dto);

}
