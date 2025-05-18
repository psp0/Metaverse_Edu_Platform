package place.run.mep.dto;

import lombok.AllArgsConstructor;
import lombok.Getter;
import lombok.Setter;

import java.util.List;

@Getter
@Setter
@AllArgsConstructor
public class PagedUserResponseDto {
    private long totalElements;
    private int totalPages;
    private List<UserListInfoResponseDto> content;
}
