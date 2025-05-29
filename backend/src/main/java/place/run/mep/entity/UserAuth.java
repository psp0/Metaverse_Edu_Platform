package place.run.mep.entity;

import lombok.Getter;
import lombok.Setter;
import jakarta.persistence.*;
import java.time.LocalDateTime;

@Entity
@Getter
@Setter
@Table(name = "user_auth")
public class UserAuth {
    @Id
    @Column(name = "user_no")
    private Long userNo;

    @OneToOne
    @MapsId // This ensures that user_no is taken from the User entity
    @JoinColumn(name = "user_no")
    private User user;

    @Column(name = "password_hash", nullable = false, length = 255)
    private String passwordHash;

    @Column(name = "pw_changed")
    private LocalDateTime pwChanged;
}
