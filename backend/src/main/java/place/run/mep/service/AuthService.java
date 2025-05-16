package place.run.mep.service;

import place.run.mep.dto.LoginRequestDto;
import place.run.mep.dto.TokenResponseDto; // Changed from LoginResponseDto
import place.run.mep.dto.TokenRefreshRequestDto;

public interface AuthService {
    TokenResponseDto login(LoginRequestDto loginRequestDto); // Changed return type
    TokenResponseDto refreshToken(TokenRefreshRequestDto tokenRefreshRequestDto); // Changed return type
    // void register(RegisterRequestDto registerRequestDto); // This is currently in UserService
}
