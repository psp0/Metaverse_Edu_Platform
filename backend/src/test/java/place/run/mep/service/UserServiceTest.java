package place.run.mep.service;

import org.junit.jupiter.api.DisplayName;
import org.junit.jupiter.api.Test;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.boot.test.context.SpringBootTest;
import org.springframework.data.domain.PageRequest;
import org.springframework.data.domain.Pageable;
import org.springframework.test.context.ActiveProfiles;
import org.springframework.transaction.annotation.Transactional;
import place.run.mep.dto.PagedUserResponseDto;
import place.run.mep.dto.RegisterRequestDto;
import place.run.mep.dto.UserInfoDto;
import place.run.mep.entity.User;
import place.run.mep.repository.UserAuthRepository;
import place.run.mep.repository.UserProfileRepository;
import place.run.mep.repository.UserRepository;

import java.time.LocalDate;

import static org.assertj.core.api.Assertions.*;

@SpringBootTest
@ActiveProfiles("test")
@Transactional
class UserServiceTest {
    @Autowired
    private UserService userService;

    @Autowired
    private UserRepository userRepository;
    @Autowired
    private UserAuthRepository userAuthRepository;
    @Autowired
    private UserProfileRepository userProfileRepository;


    @Test
    @DisplayName("유저 등록")
    void registerUser() {
        userService.registerUser(createRegisterDtoUser1());


        User user = userRepository.findByUserId("test1111").orElse(null);


        assertThat(user).isNotNull();
        assertThat(user.getUserId()).isEqualTo("test1111");
        assertThat(userProfileRepository.findById(user.getUserNo())).isPresent();
        assertThat(userAuthRepository.findById(user.getUserNo())).isPresent();
    }

    @Test
    @DisplayName("비밀번호 불일치 시 예외 발생")
    void registerUser_passwordMismatch() {
        assertThatThrownBy(() -> userService.registerUser(createMismatchPasswordRegisterDtoUser()))
                .isInstanceOf(IllegalArgumentException.class)
                .hasMessageContaining("Passwords do not match");
    }


    @Test
    @DisplayName("중복 userId 등록 시 예외 발생")
    void registerUser_duplicateUserId() {
        userService.registerUser(createRegisterDtoUser1());

        RegisterRequestDto duplicate = createRegisterDtoUser1();

        assertThatThrownBy(() -> userService.registerUser(duplicate))
                .isInstanceOf(IllegalArgumentException.class)
                .hasMessageContaining("User ID already exists");
    }

    @Test
    @DisplayName("중복 email 등록 시 예외 발생")
    void registerUser_duplicateEmail() {
        userService.registerUser(createRegisterDtoUser1());

        RegisterRequestDto duplicate = createRegisterDtoUser2();
        duplicate.setEmail("test1@naver.com"); // email 중복

        assertThatThrownBy(() -> userService.registerUser(duplicate))
                .isInstanceOf(IllegalArgumentException.class)
                .hasMessageContaining("Email already exists");
    }


    @Test
    @DisplayName("중복 nickname 등록 시 예외 발생")
    void registerUser_duplicateNickname() {
        userService.registerUser(createRegisterDtoUser1());

        RegisterRequestDto duplicate = createRegisterDtoUser2();
        duplicate.setNickname("dragonTest1"); // nickname 중복

        assertThatThrownBy(() -> userService.registerUser(duplicate))
                .isInstanceOf(IllegalArgumentException.class)
                .hasMessageContaining("Nickname already exists");
    }



    @Test
    @DisplayName("유저 1명 정보 가져오기")
    void getUserInfo() {
        userService.registerUser(createRegisterDtoUser1());


        UserInfoDto userInfo = userService.getUserInfo("test1111");


        assertThat(userInfo.getUserId()).isEqualTo("test1111");
        assertThat(userInfo.getEmail()).isEqualTo("test1@naver.com");
        assertThat(userInfo.getPhone()).isEqualTo("010-1111-1111");
        assertThat(userInfo.getName()).isEqualTo("test1");
        assertThat(userInfo.getNickname()).isEqualTo("dragonTest1");
        assertThat(userInfo.getBirthDate()).isEqualTo(LocalDate.of(2001, 1, 11));
        assertThat(userInfo.getGender()).isEqualTo("M");
    }

    @Test
    @DisplayName("없는 userId로 유저 조회 시 예외 발생")
    void getUserInfo_userNotFound() {
        assertThatThrownBy(() -> userService.getUserInfo("nonexistent"))
                .isInstanceOf(RuntimeException.class)
                .hasMessageContaining("User not found");
    }


    @Test
    @DisplayName("전체 유저 정보 가져오기")
    void getAllUsers() {
        userService.registerUser(createRegisterDtoUser1());
        userService.registerUser(createRegisterDtoUser2());
        userService.registerUser(createRegisterDtoUser3());


        Pageable pageable = PageRequest.of(0, 10);
        PagedUserResponseDto response = userService.getAllUsers(pageable);


        assertThat(response.getTotalElements()).isEqualTo(3);
        assertThat(response.getTotalPages()).isEqualTo(1);
        assertThat(response.getContent()).hasSize(3);

        assertThat(response.getContent()).extracting("userId")
                .containsExactlyInAnyOrder("test1111", "test2222", "test3333");
    }

    public RegisterRequestDto createMismatchPasswordRegisterDtoUser() {
        return RegisterRequestDto.builder()
                .userId("test1111")
                .password("password1111")
                .confirmPassword("password2222")
                .email("test1@naver.com")
                .phone("010-1111-1111")
                .name("test1")
                .nickname("dragonTest1")
                .birthDate(LocalDate.of(2001, 1, 11))
                .gender("M")
                .build();
    }

    public RegisterRequestDto createRegisterDtoUser1(){
        return RegisterRequestDto.builder()
                .userId("test1111")
                .password("password1111")
                .confirmPassword("password1111")
                .email("test1@naver.com")
                .phone("010-1111-1111")
                .name("test1")
                .nickname("dragonTest1")
                .birthDate(LocalDate.of(2001, 1, 11))
                .gender("M")
                .build();
    }

    public RegisterRequestDto createRegisterDtoUser2() {
        return RegisterRequestDto.builder()
                .userId("test2222")
                .password("password2222")
                .confirmPassword("password2222")
                .email("test2@naver.com")
                .phone("010-2222-2222")
                .name("test2")
                .nickname("dragonTest2")
                .birthDate(LocalDate.of(2002, 2, 22))
                .gender("F")
                .build();
    }

    public RegisterRequestDto createRegisterDtoUser3() {
        return RegisterRequestDto.builder()
                .userId("test3333")
                .password("password3333")
                .confirmPassword("password3333")
                .email("test3@naver.com")
                .phone("010-3333-3333")
                .name("test3")
                .nickname("dragonTest3")
                .birthDate(LocalDate.of(2003, 3, 3))
                .gender("M")
                .build();
    }

}