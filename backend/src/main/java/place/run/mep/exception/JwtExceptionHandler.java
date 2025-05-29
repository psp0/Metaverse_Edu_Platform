package place.run.mep.exception;

import io.jsonwebtoken.*;
import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.ExceptionHandler;
import org.springframework.web.bind.annotation.RestControllerAdvice;


@RestControllerAdvice
public class JwtExceptionHandler {

    // 토큰 만료
    @ExceptionHandler(ExpiredJwtException.class)
    public ResponseEntity<ErrorResult> handleExpiredJwtException(ExpiredJwtException ex) {
        return ResponseEntity.status(HttpStatus.UNAUTHORIZED)
                .body(new ErrorResult("JWT_EXPIRED", "Access token has expired"));
    }


    // Base64 디코딩이 불가능한 값
    @ExceptionHandler(MalformedJwtException.class)
    public ResponseEntity<ErrorResult> handleMalformedJwtException(MalformedJwtException ex) {
        return ResponseEntity.status(HttpStatus.BAD_REQUEST)
                .body(new ErrorResult("JWT_MALFORMED", "JWT is malformed and cannot be parsed"));
    }


    // 클라이언트가 위조한 토큰 전달 or 서버가 잘못된 secret키로 파싱
    @ExceptionHandler(SignatureException.class)
    public ResponseEntity<ErrorResult> handleSignatureException(SignatureException ex) {
        return ResponseEntity.status(HttpStatus.UNAUTHORIZED)
                .body(new ErrorResult("JWT_INVALID_SIGNATURE", "Invalid JWT signature"));
    }


    // 기타 jwt 오류
    @ExceptionHandler(JwtException.class)
    public ResponseEntity<ErrorResult> handleJwtException(JwtException ex) {
        return ResponseEntity.status(HttpStatus.UNAUTHORIZED)
                .body(new ErrorResult("JWT_ERROR", "Invalid JWT token"));
    }
}
