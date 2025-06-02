package place.run.mep.controller;

import io.swagger.v3.oas.annotations.Parameter;
import jakarta.validation.Valid;
import org.springframework.security.core.annotation.AuthenticationPrincipal;
import org.springframework.security.core.userdetails.UserDetails;
import place.run.mep.dto.LoginRequestDto;
import place.run.mep.dto.RegisterRequestDto;
import place.run.mep.dto.TokenResponseDto;
import place.run.mep.dto.TokenRefreshRequestDto;
import place.run.mep.service.AuthService;
import place.run.mep.service.UserService;
import lombok.RequiredArgsConstructor;
import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;



@RestController
@RequestMapping("/api/users")
@RequiredArgsConstructor
public class AuthController {

    private final AuthService authService;

    @PostMapping("/register")
    public ResponseEntity<?> registerUser(@Valid @RequestBody RegisterRequestDto registerRequestDto) {
        authService.registerUser(registerRequestDto);
        return ResponseEntity.status(HttpStatus.CREATED).body("{\"message\": \"회원 가입이 완료되었습니다.\"}");
    }

    @PostMapping("/login")
    public ResponseEntity<TokenResponseDto> login(@Valid @RequestBody LoginRequestDto loginRequestDto) {
        TokenResponseDto tokenResponseDto = authService.login(loginRequestDto);
        return ResponseEntity.ok(tokenResponseDto);
    }

    @PostMapping("/logout")
    public ResponseEntity<?> logout(@Parameter(hidden = true) @AuthenticationPrincipal UserDetails userDetails) {
        authService.logout(userDetails.getUsername());
        return ResponseEntity.ok("{\"message\": \"로그아웃 되었습니다.\"}");
    }

    @PostMapping("/token/refresh")
    public ResponseEntity<TokenResponseDto> refreshToken(@Valid @RequestBody TokenRefreshRequestDto tokenRefreshRequestDto) { // Added @Valid and DTO
        TokenResponseDto tokenResponseDto = authService.refreshToken(tokenRefreshRequestDto);
        return ResponseEntity.ok(tokenResponseDto);
    }
}
