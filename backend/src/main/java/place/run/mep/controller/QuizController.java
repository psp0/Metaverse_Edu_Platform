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
import place.run.mep.dto.QuizAnswerRequestDto;
import place.run.mep.dto.QuizAnswerResponseDto;
import place.run.mep.dto.SubUnitQuizStateDto; // [수정] DTO import 변경
import place.run.mep.service.QuizService;

@Tag(name = "학습 (퀴즈)", description = "개념 퀴즈 문제 조회 및 풀이 관련 API")
@RestController
@RequestMapping("/api/quizzes")
@RequiredArgsConstructor
public class QuizController {

    private final QuizService quizService;

    @Operation(summary = "소단원별 퀴즈 상태 및 전체 목록 조회",
            description = "특정 소단원의 전체 문제 수, 푼 문제 수 등 현황과 함께, 풀이 여부가 포함된 전체 문제 목록을 반환합니다.")
    @SecurityRequirement(name = "bearerAuth")
    @GetMapping("/sub-unit/{subUnitId}/state")
    public ResponseEntity<SubUnitQuizStateDto> getQuizState(
                                                             @Parameter(hidden = true) @AuthenticationPrincipal UserDetails userDetails,
                                                             @PathVariable Integer subUnitId) {
        SubUnitQuizStateDto subUnitQuizState = quizService.getQuizState(userDetails.getUsername(), subUnitId);
        return ResponseEntity.ok(subUnitQuizState);
    }

    @Operation(summary = "퀴즈 단일 문항 제출 및 채점",
            description = "한 문제의 답안을 제출하면 즉시 채점/해설을 반환하고 진행도를 업데이트합니다. 마지막 문제일 경우, 자동으로 숙달도 판정까지 수행됩니다.")
    @SecurityRequirement(name = "bearerAuth")
    @PostMapping("/answer")
    public ResponseEntity<QuizAnswerResponseDto> submitAnswer(
            @Parameter(hidden = true) @AuthenticationPrincipal UserDetails userDetails,
            @RequestBody QuizAnswerRequestDto requestDto) {
        QuizAnswerResponseDto response = quizService.submitAnswer(
                userDetails.getUsername(),
                requestDto.getQuizId(),
                requestDto.getSelectedOptionId()
        );
        return ResponseEntity.ok(response);
    }

}