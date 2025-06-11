package place.run.mep.service;

import place.run.mep.dto.*;
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

import java.time.LocalDate;
import java.time.LocalDateTime;
import java.util.List;
import java.util.stream.Collectors;

@Service
@RequiredArgsConstructor
public class UserServiceImpl implements UserService {

    private final UserRepository userRepository;
    private final UserProfileRepository userProfileRepository;
    private final UserAuthRepository userAuthRepository;
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
    @Override
    @Transactional
    public void updateUserInfo(String userId, UpdateUserDto dto) {
        User user = userRepository.findByUserId(userId)
                .orElseThrow(() -> new RuntimeException("User not found"));
        UserProfile profile = userProfileRepository.findById(user.getUserNo())
                .orElseThrow(() -> new RuntimeException("User profile not found"));

        if (dto.getNickname() != null && !dto.getNickname().equals(profile.getNickname())) {
            boolean exists = userProfileRepository.existsByNickname(dto.getNickname());
            if (exists) {
                throw new IllegalArgumentException("이미 사용 중인 닉네임입니다.");
            }
            profile.setNickname(dto.getNickname());
        }

        if (dto.getName() != null) {
            profile.setName(dto.getName());
        }

        if (dto.getPhone() != null) {
            profile.setPhone(dto.getPhone());
        }

        if (dto.getBirthdate() != null) {
            profile.setBirthDate(LocalDate.parse(dto.getBirthdate()));
        }

        if (dto.getGender() != null) {
            if (!dto.getGender().equals("M") && !dto.getGender().equals("F")) {
                throw new IllegalArgumentException("성별은 'M' 또는 'F'이어야 합니다.");
            }
            profile.setGender(dto.getGender());
        }
    }
    @Override
    @Transactional
    public void changePassword(String userId, ChangePasswordDto dto) {
        User user = userRepository.findByUserId(userId)
                .orElseThrow(() -> new RuntimeException("User not found"));

        UserAuth auth = userAuthRepository.findById(user.getUserNo())
                .orElseThrow(() -> new RuntimeException("UserAuth not found"));

        if (!passwordEncoder.matches(dto.getCurrentPassword(), auth.getPasswordHash())) {
            throw new IllegalArgumentException("현재 비밀번호가 일치하지 않습니다.");
        }


        auth.setPasswordHash(passwordEncoder.encode(dto.getNewPassword()));
        auth.setPwChanged(LocalDateTime.now());
    }



}
