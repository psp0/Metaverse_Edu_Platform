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
import place.run.mep.dto.QuizStateDto;
import place.run.mep.service.QuizService;

import java.util.List;

@Tag(name = "학습 (퀴즈)", description = "개념 퀴즈 문제 조회 및 풀이 관련 API")
@RestController
@RequestMapping("/api/quizzes")
@RequiredArgsConstructor
public class QuizController {

    private final QuizService quizService;

    @Operation(summary = "소단원별 퀴즈 상태 조회 (이어풀기용)",
               description = "특정 소단원의 모든 문제와 함께, 사용자가 이전에 푼 기록(선택 답안, 정답 여부)을 반환합니다.")
    @GetMapping("/sub-unit/{subUnitId}/state")
    public ResponseEntity<List<QuizStateDto>> getQuizState(
            @Parameter(hidden = true) @AuthenticationPrincipal UserDetails userDetails,
            @PathVariable Integer subUnitId) {
        
        List<QuizStateDto> quizStates = quizService.getQuizStateForUser(userDetails.getUsername(), subUnitId);
        return ResponseEntity.ok(quizStates);
    }

    @Operation(summary = "퀴즈 단일 문항 제출 및 채점",
               description = "사용자가 푼 한 문제의 답안을 받아 즉시 채점하고, 정답 여부 및 해설을 반환하며, 진행도를 업데이트합니다.")
    @PostMapping("/answer")
    public ResponseEntity<QuizAnswerResponseDto> submitSingleAnswer(
            @Parameter(hidden = true) @AuthenticationPrincipal UserDetails userDetails,
            @RequestBody QuizAnswerRequestDto requestDto) {

        QuizAnswerResponseDto response = quizService.submitAndGradeSingleAnswer(userDetails.getUsername(), requestDto);
        return ResponseEntity.ok(response);
    }
}