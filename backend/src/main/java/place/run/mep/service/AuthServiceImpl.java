package place.run.mep.service;

import place.run.mep.config.JwtTokenProvider;
import place.run.mep.dto.LoginRequestDto;
import place.run.mep.dto.TokenResponseDto;
import place.run.mep.dto.TokenRefreshRequestDto;
import place.run.mep.entity.User;
import place.run.mep.entity.UserRefreshToken;
import place.run.mep.repository.UserRefreshTokenRepository;
import place.run.mep.repository.UserRepository;
import place.run.mep.service.UserDetailsServiceImpl;
import lombok.RequiredArgsConstructor;
import org.springframework.security.authentication.AuthenticationManager;
import org.springframework.security.authentication.UsernamePasswordAuthenticationToken;
import org.springframework.security.core.Authentication;
import org.springframework.security.core.userdetails.UserDetails;
import org.springframework.stereotype.Service;
import org.springframework.transaction.annotation.Transactional;

import java.time.LocalDateTime;

@Service
@RequiredArgsConstructor
public class AuthServiceImpl implements AuthService {

    private final AuthenticationManager authenticationManager;
    private final JwtTokenProvider jwtTokenProvider;
    private final UserRefreshTokenRepository userRefreshTokenRepository;
    private final UserRepository userRepository;
    private final UserDetailsServiceImpl userDetailsServiceImpl;

    @Override
    @Transactional
    public TokenResponseDto login(LoginRequestDto loginRequestDto) {
        Authentication authentication = authenticationManager.authenticate(
                new UsernamePasswordAuthenticationToken(loginRequestDto.getUserId(), loginRequestDto.getPassword())        
        );
        
        String accessToken = jwtTokenProvider.createAccessToken(authentication); 
        String refreshTokenString = jwtTokenProvider.createRefreshToken(authentication); 

        UserDetails userDetails = (UserDetails) authentication.getPrincipal();
        User user = userRepository.findByUserId(userDetails.getUsername())
            .orElseThrow(() -> new RuntimeException("User not found for refresh token persistence"));

        UserRefreshToken userRefreshToken = userRefreshTokenRepository.findByUser_UserId(user.getUserId())
                .orElse(new UserRefreshToken()); 
        
        userRefreshToken.setUser(user);
        userRefreshToken.setRefreshToken(refreshTokenString);
        userRefreshToken.setIssuedAt(LocalDateTime.now()); 
        userRefreshToken.setExpiresAt(LocalDateTime.now().plusSeconds(jwtTokenProvider.getRefreshTokenValidityInSeconds())); 
        userRefreshToken.setRevoked(false);
        userRefreshTokenRepository.save(userRefreshToken);

        return new TokenResponseDto(accessToken, refreshTokenString); 
    }

    @Override
    @Transactional
    public TokenResponseDto refreshToken(TokenRefreshRequestDto tokenRefreshRequestDto) {
        String requestRefreshToken = tokenRefreshRequestDto.getRefreshToken();
        
        if (!jwtTokenProvider.validateToken(requestRefreshToken)) {
            throw new RuntimeException("Invalid Refresh Token");
        }

        String userId = jwtTokenProvider.getUsernameFromToken(requestRefreshToken);
        UserRefreshToken storedRefreshToken = userRefreshTokenRepository.findByUser_UserId(userId) 
                .orElseThrow(() -> new RuntimeException("Refresh token not found in DB"));

        if (storedRefreshToken.getRevoked() || !storedRefreshToken.getRefreshToken().equals(requestRefreshToken) || storedRefreshToken.getExpiresAt().isBefore(LocalDateTime.now())) {
            throw new RuntimeException("Refresh token is revoked, mismatched, or expired");
        }

        UserDetails userDetails = userDetailsServiceImpl.loadUserByUsername(userId);
        Authentication newAuthentication = new UsernamePasswordAuthenticationToken(userDetails, null, userDetails.getAuthorities());
        String newAccessToken = jwtTokenProvider.createAccessToken(newAuthentication); 

        // Optionally, re-issue a new refresh token and update it in the database
        // String newRefreshToken = jwtTokenProvider.createRefreshToken(newAuthentication);
        // storedRefreshToken.setRefreshToken(newRefreshToken);
        // userRefreshTokenRepository.save(storedRefreshToken);

        return new TokenResponseDto(newAccessToken, requestRefreshToken); // or newRefreshToken if re-issued
    }
}
