package place.run.mep.service;

import org.springframework.security.crypto.password.PasswordEncoder;
import place.run.mep.dto.RegisterRequestDto;
import place.run.mep.entity.UserAuth;
import place.run.mep.entity.UserProfile;
import place.run.mep.jwt.JwtTokenProvider;
import place.run.mep.dto.LoginRequestDto;
import place.run.mep.dto.TokenResponseDto;
import place.run.mep.dto.TokenRefreshRequestDto;
import place.run.mep.entity.User;
import place.run.mep.entity.UserRefreshToken;
import place.run.mep.repository.UserAuthRepository;
import place.run.mep.repository.UserProfileRepository;
import place.run.mep.repository.UserRefreshTokenRepository;
import place.run.mep.repository.UserRepository;
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

    private final UserAuthRepository userAuthRepository;
    private final PasswordEncoder passwordEncoder;
    private final AuthenticationManager authenticationManager;
    private final JwtTokenProvider jwtTokenProvider;
    private final UserRefreshTokenRepository userRefreshTokenRepository;
    private final UserRepository userRepository;
    private final UserDetailsServiceImpl userDetailsServiceImpl;
    private final UserProfileRepository userProfileRepository;

    @Transactional
    @Override
    public void registerUser(RegisterRequestDto registerRequestDto) {
        if (!registerRequestDto.getPassword().equals(registerRequestDto.getConfirmPassword())) {
            throw new IllegalArgumentException("Passwords do not match.");
        }
        if (userRepository.existsByUserId(registerRequestDto.getUserId())) {
            throw new IllegalArgumentException("User ID already exists.");
        }
        if (userProfileRepository.existsByEmail(registerRequestDto.getEmail())) {
            throw new IllegalArgumentException("Email already exists.");
        }
        if (userProfileRepository.existsByNickname(registerRequestDto.getNickname())){
            throw new IllegalArgumentException("Nickname already exists.");
        }

        User user = new User();
        user.setUserId(registerRequestDto.getUserId());
        userRepository.save(user);

        UserAuth userAuth = new UserAuth();
        userAuth.setUser(user);
        userAuth.setPasswordHash(passwordEncoder.encode(registerRequestDto.getPassword()));
        userAuthRepository.save(userAuth);

        UserProfile userProfile = new UserProfile();
        userProfile.setUser(user);
        userProfile.setEmail(registerRequestDto.getEmail());
        userProfile.setPhone(registerRequestDto.getPhone());
        userProfile.setName(registerRequestDto.getName());
        userProfile.setNickname(registerRequestDto.getNickname());
        userProfile.setBirthDate(registerRequestDto.getBirthDate());
        userProfile.setGender(registerRequestDto.getGender());
        userProfileRepository.save(userProfile);
    }

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
    public void logout(String userId) {
        UserRefreshToken token = userRefreshTokenRepository.findByUser_UserId(userId)
                .orElseThrow(() -> new RuntimeException("The refresh token for that user does not exist."));
        if (Boolean.TRUE.equals(token.getRevoked())) {
            throw new IllegalArgumentException("이미 로그아웃된 사용자입니다.");
        }
        token.setRevoked(true);
        userRefreshTokenRepository.save(token);
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
