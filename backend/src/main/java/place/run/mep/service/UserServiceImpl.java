package place.run.mep.service;

import place.run.mep.dto.RegisterRequestDto;
import place.run.mep.dto.UserInfoDto;
import place.run.mep.dto.PagedUserResponseDto;
import place.run.mep.dto.UserListInfoResponseDto;
import place.run.mep.entity.User;
import place.run.mep.entity.UserAuth;
import place.run.mep.entity.UserProfile;
import place.run.mep.repository.UserAuthRepository;
import place.run.mep.repository.UserProfileRepository;
import place.run.mep.repository.UserRepository;
import lombok.RequiredArgsConstructor;
import org.springframework.data.domain.Page;
import org.springframework.data.domain.Pageable;
import org.springframework.security.crypto.password.PasswordEncoder;
import org.springframework.stereotype.Service;
import org.springframework.transaction.annotation.Transactional;

import java.time.LocalDateTime;
import java.util.List;
import java.util.stream.Collectors;

@Service
@RequiredArgsConstructor
public class UserServiceImpl implements UserService {

    private final UserRepository userRepository;
    private final UserAuthRepository userAuthRepository;
    private final UserProfileRepository userProfileRepository;
    private final PasswordEncoder passwordEncoder;


    @Override
    public UserInfoDto getUserInfo(String userId) {
        User user = userRepository.findByUserId(userId)
                .orElseThrow(() -> new RuntimeException("User not found")); // Consider custom exception
        UserProfile userProfile = userProfileRepository.findById(user.getUserNo())
                .orElseThrow(() -> new RuntimeException("User profile not found")); // Consider custom exception

        return UserInfoDto.builder()
                .userId(user.getUserId())
                .email(userProfile.getEmail())
                .phone(userProfile.getPhone())
                .name(userProfile.getName())
                .nickname(userProfile.getNickname())
                .birthDate(userProfile.getBirthDate())
                .gender(userProfile.getGender())
                .build();
    }

    @Override
    @Transactional(readOnly = true)
    public PagedUserResponseDto getAllUsers(Pageable pageable) {
        Page<User> userPage = userRepository.findAll(pageable);
        List<UserListInfoResponseDto> userList = userPage.getContent().stream()
                .map(user -> {
                    // Fetching UserProfile separately. This could be optimized if needed.
                    UserProfile userProfile = userProfileRepository.findById(user.getUserNo())
                            .orElse(new UserProfile()); // Or throw exception / handle as appropriate
                    return UserListInfoResponseDto.builder()
                            .userNo(user.getUserNo())
                            .userId(user.getUserId())
                            .email(userProfile.getEmail()) // Assuming UserProfile is always present or handled
                            .nickname(userProfile.getNickname())
                            .createdAt(user.getCreatedAt())
                            .build();
                })
                .collect(Collectors.toList());

        return new PagedUserResponseDto(userPage.getTotalElements(), userPage.getTotalPages(), userList);
    }
}
