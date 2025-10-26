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
import place.run.mep.dto.CustomProblemSubmitRequestDto;
import place.run.mep.dto.CustomProblemSubmitResponseDto;
import place.run.mep.dto.ProblemDto;
import place.run.mep.service.CustomProblemService;
import java.util.List;

@Tag(name = "학습 (맞춤 문제)", description = "숙달도 기반 맞춤 문제 풀이 관련 API")
@RestController
@RequestMapping("/api/custom-problems")
@RequiredArgsConstructor
public class CustomProblemController {

    private final CustomProblemService customProblemService;

    @Operation(summary = "맞춤 문제 목록 조회", description = "사용자의 현재 숙달도에 맞는 레벨별 문제 조합을 반환합니다.")
    @SecurityRequirement(name = "bearerAuth")
    @GetMapping("/group/{subUnitGroupId}")
    public ResponseEntity<List<ProblemDto>> getCustomProblems(
            @Parameter(hidden = true) @AuthenticationPrincipal UserDetails userDetails,
            @PathVariable Integer subUnitGroupId) {

        List<ProblemDto> problems = customProblemService.getCustomProblems(userDetails.getUsername(), subUnitGroupId);
        return ResponseEntity.ok(problems);
    }

    @Operation(summary = "맞춤 문제 답안 제출 및 채점/숙달도 재평가",
            description = "사용자가 푼 맞춤 문제 답안들을 제출하면, 채점 결과와 해설, SAKT 모델을 통해 재계산된 숙달도를 반환합니다.")
    @SecurityRequirement(name = "bearerAuth")
    @PostMapping("/submit")
    public ResponseEntity<CustomProblemSubmitResponseDto> submitCustomProblems(
            @Parameter(hidden = true) @AuthenticationPrincipal UserDetails userDetails,
            @RequestBody CustomProblemSubmitRequestDto requestDto) {

        CustomProblemSubmitResponseDto response = customProblemService.submitCustomProblems(userDetails.getUsername(), requestDto);
        return ResponseEntity.ok(response);
    }
}