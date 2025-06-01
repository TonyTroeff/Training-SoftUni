package exercise_1.dtos;

import jakarta.validation.constraints.DecimalMin;
import jakarta.validation.constraints.NotNull;
import jakarta.validation.constraints.Pattern;
import jakarta.validation.constraints.Size;

import java.math.BigDecimal;

public class GameInputDto {
    @NotNull
    @Size(min = 3, max = 100)
    @Pattern(regexp = "^[A-Z].+$")
    private final String title;

    @NotNull
    @DecimalMin(value = "0.01", inclusive = false)
    private final BigDecimal price;

    @NotNull
    @Pattern(regexp = "^\\w{11}$")
    private final String trailer;

    @NotNull
    @Pattern(regexp = "^https?://.+$")
    private final String thumbnail;

    @NotNull
    @Size(min = 20)
    private final String description;

    public GameInputDto(String title, BigDecimal price, String trailer, String thumbnail, String description) {
        this.title = title;
        this.price = price;
        this.trailer = trailer;
        this.thumbnail = thumbnail;
        this.description = description;
    }

    public String getTitle() {
        return this.title;
    }

    public BigDecimal getPrice() {
        return this.price;
    }

    public String getTrailer() {
        return this.trailer;
    }

    public String getThumbnail() {
        return this.thumbnail;
    }

    public String getDescription() {
        return this.description;
    }
}
