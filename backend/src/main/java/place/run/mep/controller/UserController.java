package place.run.mep.controller;

import io.swagger.v3.oas.annotations.Parameter;
import jakarta.validation.Valid;
import org.springframework.data.domain.PageRequest;
import org.springframework.data.domain.Sort;
import place.run.mep.dto.UpdateUserDto;
import place.run.mep.dto.UserInfoDto;
import place.run.mep.dto.PagedUserResponseDto;
import place.run.mep.service.UserService;
import lombok.RequiredArgsConstructor;
import org.springframework.data.domain.Pageable;
import org.springframework.data.web.PageableDefault;
import org.springframework.http.ResponseEntity;
import org.springframework.security.core.annotation.AuthenticationPrincipal;
import org.springframework.security.core.userdetails.UserDetails;
import org.springframework.web.bind.annotation.*;

@RestController
@RequestMapping("/api/users")
@RequiredArgsConstructor
public class UserController {

    private final UserService userService;

    @GetMapping("/me")
    public ResponseEntity<UserInfoDto> getCurrentUser(@Parameter(hidden = true) @AuthenticationPrincipal UserDetails userDetails) {
        // Assuming UserDetails.getUsername() returns the userId
        // You might need a custom UserDetails implementation to store more user info
        // or fetch it from the database using the username (userId)
        UserInfoDto userInfoDto = userService.getUserInfo(userDetails.getUsername());
        return ResponseEntity.ok(userInfoDto);
    }

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

    @PatchMapping("/me")
    public ResponseEntity<?> updateCurrentUser(
            @AuthenticationPrincipal UserDetails userDetails,
            @Valid @RequestBody UpdateUserDto updateUserDto) {
        userService.updateUserInfo(userDetails.getUsername(), updateUserDto);
        return ResponseEntity.ok("{\"message\": \"회원 정보가 수정되었습니다.\"}");
    }

}
