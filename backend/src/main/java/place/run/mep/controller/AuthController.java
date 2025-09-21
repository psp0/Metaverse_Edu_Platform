package place.run.mep.controller;

import io.swagger.v3.oas.annotations.Operation;
import io.swagger.v3.oas.annotations.Parameter;
import io.swagger.v3.oas.annotations.tags.Tag;
import jakarta.validation.Valid;
import org.springframework.security.core.annotation.AuthenticationPrincipal;
import org.springframework.security.core.userdetails.UserDetails;
import place.run.mep.dto.LoginRequestDto;
import place.run.mep.dto.RegisterRequestDto;
import place.run.mep.dto.TokenResponseDto;
import place.run.mep.dto.TokenRefreshRequestDto;
import place.run.mep.service.AuthService;
import lombok.RequiredArgsConstructor;
import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;


@Tag(name = "인증 (Authentication)", description = "사용자 회원가입, 로그인, 로그아웃 및 토큰 관련 API")
@RestController
@RequestMapping("/api/users")
@RequiredArgsConstructor
public class AuthController {

    private final AuthService authService;

    @Operation(summary = "회원 가입", description = "새로운 사용자를 등록합니다.")
    @PostMapping("/register")
    public ResponseEntity<?> registerUser(@Valid @RequestBody RegisterRequestDto registerRequestDto) {
        authService.registerUser(registerRequestDto);
        return ResponseEntity.status(HttpStatus.CREATED).body("{\"message\": \"회원 가입이 완료되었습니다.\"}");
    }

    @Operation(summary = "로그인", description = "아이디와 비밀번호로 로그인하고 JWT 토큰을 발급받습니다.")
    @PostMapping("/login")
    public ResponseEntity<TokenResponseDto> login(@Valid @RequestBody LoginRequestDto loginRequestDto) {
        TokenResponseDto tokenResponseDto = authService.login(loginRequestDto);
        return ResponseEntity.ok(tokenResponseDto);
    }

    @Operation(summary = "로그아웃", description = "서버에 저장된 사용자의 리프레시 토큰을 무효화합니다.")
    @PostMapping("/logout")
    public ResponseEntity<?> logout(@Parameter(hidden = true) @AuthenticationPrincipal UserDetails userDetails) {
        authService.logout(userDetails.getUsername());
        return ResponseEntity.ok("{\"message\": \"로그아웃 되었습니다.\"}");
    }

    @Operation(summary = "액세스 토큰 재발급", description = "유효한 리프레시 토큰으로 새로운 액세스 토큰과 리프레시 토큰을 발급받습니다.")
    @PostMapping("/token/refresh")
    public ResponseEntity<TokenResponseDto> refreshToken(@Valid @RequestBody TokenRefreshRequestDto tokenRefreshRequestDto) { // Added @Valid and DTO
        TokenResponseDto tokenResponseDto = authService.refreshToken(tokenRefreshRequestDto);
        return ResponseEntity.ok(tokenResponseDto);
    }
}
