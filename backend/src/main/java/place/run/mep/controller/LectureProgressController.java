package place.run.mep.controller;

import io.swagger.v3.oas.annotations.Operation;
import io.swagger.v3.oas.annotations.Parameter;
import io.swagger.v3.oas.annotations.security.SecurityRequirement;
import io.swagger.v3.oas.annotations.tags.Tag;
import lombok.RequiredArgsConstructor;
import org.springframework.http.ResponseEntity;
import org.springframework.security.core.annotation.AuthenticationPrincipal;
import org.springframework.security.core.userdetails.UserDetails;
import org.springframework.web.bind.annotation.PostMapping;
import org.springframework.web.bind.annotation.RequestBody;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RestController;
import place.run.mep.dto.LectureProgressRequestDto;
import place.run.mep.service.ProgressService;

@Tag(name = "학습 진행도 (강의)", description = "사용자의 강의 학습 진행 상황을 업데이트하는 API")
@RestController
@RequestMapping("/api/progress")
@RequiredArgsConstructor
public class LectureProgressController {

    private final ProgressService progressService;

    @Operation(summary = "강의 학습 진행도 업데이트 (이어보기 포함)",
               description = "사용자가 시청한 마지막 시간(초)을 받아, 진행률 계산과 이어보기 시간 저장을 한번에 처리합니다.")

    @PostMapping("/lecture")
    public ResponseEntity<Void> updateLectureProgress(
            @Parameter(hidden = true) @AuthenticationPrincipal UserDetails userDetails,
            @RequestBody LectureProgressRequestDto requestDto) {

        progressService.updateLectureProgress(
            userDetails.getUsername(),
            requestDto.getSubUnitId(),
            requestDto.getLastTimestamp()
        );
        return ResponseEntity.ok().build();
    }
}