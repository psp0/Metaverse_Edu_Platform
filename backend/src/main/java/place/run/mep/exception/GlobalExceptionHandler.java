package place.run.mep.exception;


import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;
import org.springframework.security.core.userdetails.UsernameNotFoundException;
import org.springframework.web.bind.MethodArgumentNotValidException;
import org.springframework.web.bind.annotation.ExceptionHandler;
import org.springframework.web.bind.annotation.RestControllerAdvice;

@RestControllerAdvice
public class GlobalExceptionHandler {

    @ExceptionHandler(IllegalArgumentException.class)
    public ResponseEntity<ErrorResult> handleIllegalArgument(IllegalArgumentException e) {
        return ResponseEntity.badRequest().body(new ErrorResult("BAD_REQUEST", e.getMessage()));
    }

    @ExceptionHandler(RuntimeException.class)
    public ResponseEntity<ErrorResult> handleRuntime(RuntimeException e) {
        return ResponseEntity.status(HttpStatus.NOT_FOUND).body(new ErrorResult("NOT_FOUND", e.getMessage()));
    }

    @ExceptionHandler(UsernameNotFoundException.class)
    public ResponseEntity<ErrorResult> handleUsernameNotFound(UsernameNotFoundException e) {
        return ResponseEntity
                .status(HttpStatus.UNAUTHORIZED)
                .body(new ErrorResult("USER_NOT_FOUND", e.getMessage()));
    }

    @ExceptionHandler(MethodArgumentNotValidException.class)
    public ResponseEntity<ErrorResult> handleValidation(MethodArgumentNotValidException e) {
        String errorMessage = e.getBindingResult().getFieldErrors().stream()
                .map(error -> error.getField() + ": " + error.getDefaultMessage())
                .findFirst()
                .orElse("Validation error");
        return ResponseEntity.badRequest().body(new ErrorResult("VALIDATION_ERROR", errorMessage));
    }

    @ExceptionHandler(Exception.class)
    public ResponseEntity<ErrorResult> handleException(Exception e) {
        return ResponseEntity.status(HttpStatus.INTERNAL_SERVER_ERROR)
                .body(new ErrorResult("INTERNAL_ERROR", "서버 내부 오류가 발생했습니다."));
    }
}
