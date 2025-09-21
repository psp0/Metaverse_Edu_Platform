package place.run.mep.controller;

import io.swagger.v3.oas.annotations.Operation;
import io.swagger.v3.oas.annotations.Parameter;
import io.swagger.v3.oas.annotations.security.SecurityRequirement;
import io.swagger.v3.oas.annotations.tags.Tag;
import lombok.RequiredArgsConstructor;
import org.springframework.http.ResponseEntity;
import org.springframework.security.core.annotation.AuthenticationPrincipal;
import org.springframework.security.core.userdetails.UserDetails;
import org.springframework.web.bind.annotation.*;
import place.run.mep.dto.UnitDetailDto;
import place.run.mep.service.ClassroomService;

import java.util.List;

@Tag(name = "학습 (강의실)", description = "학습 단원 조회 관련 API")
@RestController
@RequestMapping("/api/classroom")
@RequiredArgsConstructor
public class ClassroomController {

    private final ClassroomService classroomService;

    @Operation(summary = "특정 과목의 전체 단원 목록 및 학습 현황 조회",
            description = "사용자의 모든 학습 진행 상황을 포함한 단원 목록을 반환합니다.")

    @GetMapping("/subjects/{subjectId}/units")
    public ResponseEntity<List<UnitDetailDto>> getUnitsWithProgress(
            @Parameter(hidden = true) @AuthenticationPrincipal UserDetails userDetails,
            @PathVariable Integer subjectId) {
        List<UnitDetailDto> units = classroomService.getUnitsWithProgress(userDetails.getUsername(), subjectId);
        return ResponseEntity.ok(units);
    }
}