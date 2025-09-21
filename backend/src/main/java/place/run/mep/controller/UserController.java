package place.run.mep.controller;

import io.swagger.v3.oas.annotations.Operation;
import io.swagger.v3.oas.annotations.Parameter;
import io.swagger.v3.oas.annotations.tags.Tag;
import jakarta.validation.Valid;
import org.springframework.data.domain.PageRequest;
import org.springframework.data.domain.Sort;
import place.run.mep.dto.ChangePasswordDto;
import place.run.mep.dto.UpdateUserDto;
import place.run.mep.dto.UserInfoDto;
import place.run.mep.dto.PagedUserResponseDto;
import place.run.mep.service.UserService;
import lombok.RequiredArgsConstructor;
import org.springframework.data.domain.Pageable;
import org.springframework.http.ResponseEntity;
import org.springframework.security.core.annotation.AuthenticationPrincipal;
import org.springframework.security.core.userdetails.UserDetails;
import org.springframework.web.bind.annotation.*;

@Tag(name = "사용자 정보 (User Info)", description = "사용자 정보 조회, 수정 및 관리 API")
@RestController
@RequestMapping("/api/users")
@RequiredArgsConstructor
public class UserController {

    private final UserService userService;

    @Operation(summary = "현재 로그인된 사용자 정보 조회", description = "인증된 사용자의 상세 정보를 반환합니다.")
    @GetMapping("/me")
    public ResponseEntity<UserInfoDto> getCurrentUser(@Parameter(hidden = true) @AuthenticationPrincipal UserDetails userDetails) {
        // Assuming UserDetails.getUsername() returns the userId
        // You might need a custom UserDetails implementation to store more user info
        // or fetch it from the database using the username (userId)
        UserInfoDto userInfoDto = userService.getUserInfo(userDetails.getUsername());
        return ResponseEntity.ok(userInfoDto);
    }

    @Operation(summary = "전체 사용자 목록 조회", description = "페이지네이션을 통해 모든 사용자의 목록을 조회합니다.")
    @GetMapping
    public ResponseEntity<PagedUserResponseDto> getAllUsers(
            @Parameter(description = "페이지 번호") @RequestParam(defaultValue = "0") int page,
            @Parameter(description = "페이지 크기") @RequestParam(defaultValue = "10") int size,
            @Parameter(description = "정렬 필드: userId, email, createdAt 중 하나") @RequestParam(defaultValue = "userId") String sortField,
            @Parameter(description = "정렬 방향: asc 또는 desc") @RequestParam(defaultValue = "desc") String sortDir) {

        Sort.Direction direction = sortDir.equalsIgnoreCase("asc") ? Sort.Direction.ASC : Sort.Direction.DESC;
        Pageable pageable = PageRequest.of(page, size, Sort.by(direction, sortField));
        return ResponseEntity.ok(userService.getAllUsers(pageable));
    }

    @Operation(summary = "현재 사용자 정보 수정", description = "로그인된 사용자의 프로필 정보(이메일, 닉네임 등)를 수정합니다.")
    @PatchMapping("/me")
    public ResponseEntity<?> updateCurrentUser(
            @AuthenticationPrincipal UserDetails userDetails,
            @Valid @RequestBody UpdateUserDto updateUserDto) {
        userService.updateUserInfo(userDetails.getUsername(), updateUserDto);
        return ResponseEntity.ok("{\"message\": \"회원 정보가 수정되었습니다.\"}");
    }

    @Operation(summary = "현재 사용자 비밀번호 변경", description = "로그인된 사용자의 비밀번호를 변경합니다.")
    @PutMapping("/me/password")
    public ResponseEntity<?> changePassword(
            @AuthenticationPrincipal UserDetails userDetails,
            @Valid @RequestBody ChangePasswordDto dto) {
        userService.changePassword(userDetails.getUsername(), dto);
        return ResponseEntity.ok("{\"message\": \"비밀번호가 성공적으로 변경되었습니다.\"}");
    }

    @Operation(summary = "회원 탈퇴 (현재 사용자)", description = "로그인된 사용자의 계정을 삭제합니다.")
    @DeleteMapping("/me")
    public ResponseEntity<?> deleteUser(@AuthenticationPrincipal UserDetails userDetails) {
        userService.deleteUser(userDetails.getUsername());
        return ResponseEntity.ok("{\"message\": \"회원 탈퇴가 완료되었습니다.\"}");
    }


}
