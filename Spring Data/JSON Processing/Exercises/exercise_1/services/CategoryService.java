package exercise_1.services;

import exercise_1.dtos.CategoryInputDto;
import exercise_1.models.Category;
import jakarta.validation.Valid;
import org.springframework.validation.annotation.Validated;

@Validated
public interface CategoryService {
    Category createCategory(@Valid CategoryInputDto dto);
}
