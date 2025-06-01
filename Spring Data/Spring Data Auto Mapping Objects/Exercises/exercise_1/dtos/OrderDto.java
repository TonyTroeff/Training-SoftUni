package exercise_1.dtos;

import java.time.LocalDateTime;
import java.util.List;

public class OrderDto {
    private Long id;
    private LocalDateTime purchaseTime;
    private UserDto user;
    private List<GameDto> games;

    public Long getId() {
        return this.id;
    }

    public void setId(Long id) {
        this.id = id;
    }

    public LocalDateTime getPurchaseTime() {
        return this.purchaseTime;
    }

    public void setPurchaseTime(LocalDateTime purchaseTime) {
        this.purchaseTime = purchaseTime;
    }

    public UserDto getUser() {
        return this.user;
    }

    public void setUser(UserDto user) {
        this.user = user;
    }

    public List<GameDto> getGames() {
        return this.games;
    }

    public void setGames(List<GameDto> games) {
        this.games = games;
    }
}
