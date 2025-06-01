package exercise_1.dtos;

import com.google.gson.annotations.Expose;
import jakarta.validation.constraints.NotNull;
import jakarta.validation.constraints.Size;

public class CategoryInputDto {
    @Expose
    @NotNull
    @Size(min = 3, max = 15)
    private String name;

    public String getName() {
        return this.name;
    }

    public void setName(String name) {
        this.name = name;
    }
}
