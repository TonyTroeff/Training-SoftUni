package softuni.exam.models.dto;

import com.google.gson.annotations.Expose;
import jakarta.validation.constraints.NotNull;
import jakarta.validation.constraints.PositiveOrZero;
import org.hibernate.validator.constraints.Length;

public class AttractionInputDto {
    @Expose
    @NotNull
    @Length(min = 10, max = 100)
    private String description;

    @Expose
    @NotNull
    @PositiveOrZero
    private Integer elevation;

    @Expose
    @NotNull
    @Length(min = 5, max = 40)
    private String name;

    @Expose
    @NotNull
    @Length(min = 3, max = 30)
    private String type;

    @Expose
    @NotNull
    private Long country;

    public String getDescription() {
        return description;
    }

    public void setDescription(String description) {
        this.description = description;
    }

    public Integer getElevation() {
        return elevation;
    }

    public void setElevation(Integer elevation) {
        this.elevation = elevation;
    }

    public String getName() {
        return name;
    }

    public void setName(String name) {
        this.name = name;
    }

    public String getType() {
        return type;
    }

    public void setType(String type) {
        this.type = type;
    }

    public Long getCountry() {
        return country;
    }

    public void setCountry(Long country) {
        this.country = country;
    }
}
