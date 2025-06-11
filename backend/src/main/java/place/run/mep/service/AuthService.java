package place.run.mep.service;

import place.run.mep.dto.LoginRequestDto;
import place.run.mep.dto.RegisterRequestDto;
import place.run.mep.dto.TokenResponseDto; // Changed from LoginResponseDto
import place.run.mep.dto.TokenRefreshRequestDto;

public interface AuthService {
    void registerUser(RegisterRequestDto registerRequestDto);
    TokenResponseDto login(LoginRequestDto loginRequestDto); // Changed return type
    TokenResponseDto refreshToken(TokenRefreshRequestDto tokenRefreshRequestDto); // Changed return type
    void logout(String userId);
}
