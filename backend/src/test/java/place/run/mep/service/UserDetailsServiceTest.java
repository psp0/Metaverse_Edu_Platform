package place.run.mep.service;

import org.junit.jupiter.api.DisplayName;
import org.junit.jupiter.api.Test;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.boot.test.context.SpringBootTest;
import org.springframework.security.core.userdetails.UserDetails;
import org.springframework.security.core.userdetails.UsernameNotFoundException;
import org.springframework.test.context.ActiveProfiles;
import org.springframework.transaction.annotation.Transactional;
import place.run.mep.dto.RegisterRequestDto;
import place.run.mep.repository.UserAuthRepository;
import place.run.mep.repository.UserProfileRepository;
import place.run.mep.repository.UserRepository;

import java.time.LocalDate;

import static org.assertj.core.api.Assertions.*;

@SpringBootTest
@ActiveProfiles("test")
@Transactional
class UserDetailsServiceTest {

    @Autowired
    private UserDetailsServiceImpl userDetailsService;

    @Autowired
    private UserService userService;


    public RegisterRequestDto createTestUserDto(){
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

    @Test
    @DisplayName("존재하는 사용자 조회 성공")
    void loadUserByUsername_success() {
        userService.registerUser(createTestUserDto());

        UserDetails userDetails = userDetailsService.loadUserByUsername("test1111");

        assertThat(userDetails).isNotNull();
        assertThat(userDetails.getUsername()).isEqualTo("test1111");
        assertThat(userDetails.getAuthorities()).extracting("authority")
                .contains("ROLE_USER");
    }

    @Test
    @DisplayName("존재하지 않는 사용자 조회 시 예외 발생")
    void loadUserByUsername_notFound() {
        assertThatThrownBy(() -> userDetailsService.loadUserByUsername("nonexistent"))
                .isInstanceOf(UsernameNotFoundException.class)
                .hasMessageContaining("User not found with userId");
    }
}
