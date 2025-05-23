package place.run.mep.service;

import org.junit.jupiter.api.DisplayName;
import org.junit.jupiter.api.Test;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.boot.test.context.SpringBootTest;
import org.springframework.security.authentication.BadCredentialsException;
import org.springframework.test.context.ActiveProfiles;
import org.springframework.transaction.annotation.Transactional;
import place.run.mep.dto.LoginRequestDto;
import place.run.mep.dto.RegisterRequestDto;
import place.run.mep.dto.TokenRefreshRequestDto;
import place.run.mep.dto.TokenResponseDto;
import place.run.mep.entity.UserRefreshToken;
import place.run.mep.repository.UserRefreshTokenRepository;

import java.time.LocalDate;

import static org.assertj.core.api.Assertions.*;

@SpringBootTest
@ActiveProfiles("test")
@Transactional
class AuthServiceTest {

    @Autowired
    private AuthService authService;

    @Autowired
    private UserService userService;

    @Autowired
    private UserRefreshTokenRepository userRefreshTokenRepository;

    public RegisterRequestDto createRegisterDtoUser(){
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

    private LoginRequestDto createLoginDto() {
        LoginRequestDto loginRequestDto = new LoginRequestDto();
        loginRequestDto.setUserId("test1111");
        loginRequestDto.setPassword("password1111");
        return loginRequestDto;
    }

    private LoginRequestDto createWrongLoginDto(){
        LoginRequestDto loginRequestDto = new LoginRequestDto();
        loginRequestDto.setUserId("test1111");
        loginRequestDto.setPassword("wrongPassword1111");
        return loginRequestDto;
    }

    @Test
    @DisplayName("로그인 성공 시 AccessToken과 RefreshToken 반환")
    void login_success() {
        userService.registerUser(createRegisterDtoUser());


        TokenResponseDto tokenResponse = authService.login(createLoginDto());


        assertThat(tokenResponse.getAccessToken()).isNotBlank();
        assertThat(tokenResponse.getRefreshToken()).isNotBlank();

        UserRefreshToken token = userRefreshTokenRepository.findByUser_UserId("test1111").orElse(null);
        assertThat(token).isNotNull();
        assertThat(token.getRefreshToken()).isEqualTo(tokenResponse.getRefreshToken());
    }

    @Test
    @DisplayName("잘못된 비밀번호로 로그인 시 실패")
    void login_wrong_password() {
        userService.registerUser(createRegisterDtoUser());


        assertThatThrownBy(() -> authService.login(createWrongLoginDto()))
                .isInstanceOf(BadCredentialsException.class);
    }


    @Test
    @DisplayName("유효한 RefreshToken으로 AccessToken 재발급 성공")
    void refreshToken_success() {
        userService.registerUser(createRegisterDtoUser());
        TokenResponseDto loginResponse = authService.login(createLoginDto());

        TokenRefreshRequestDto request = new TokenRefreshRequestDto();
        request.setRefreshToken(loginResponse.getRefreshToken());


        TokenResponseDto newToken = authService.refreshToken(request);


        assertThat(newToken.getAccessToken()).isNotBlank();
        assertThat(newToken.getRefreshToken()).isEqualTo(loginResponse.getRefreshToken()); // 동일 refreshToken 재사용
    }

    @Test
    @DisplayName("만료된 RefreshToken으로 재발급 시 실패")
    void refreshToken_expired() {
        userService.registerUser(createRegisterDtoUser());
        TokenResponseDto loginResponse = authService.login(createLoginDto());

        UserRefreshToken stored = userRefreshTokenRepository.findByUser_UserId("test1111").orElseThrow();
        stored.setExpiresAt(LocalDate.now().minusDays(1).atStartOfDay()); // 강제 만료

        TokenRefreshRequestDto request = new TokenRefreshRequestDto();
        request.setRefreshToken(loginResponse.getRefreshToken());


        assertThatThrownBy(() -> authService.refreshToken(request))
                .isInstanceOf(RuntimeException.class)
                .hasMessageContaining("expired");
    }

    @Test
    @DisplayName("DB에 없는 RefreshToken으로 재발급 시 실패")
    void refreshToken_notFound() {
        TokenRefreshRequestDto fake = new TokenRefreshRequestDto();
        fake.setRefreshToken("fakeRefreshToken");


        assertThatThrownBy(() -> authService.refreshToken(fake))
                .isInstanceOf(RuntimeException.class)
                .hasMessageContaining("Invalid");
    }
}
