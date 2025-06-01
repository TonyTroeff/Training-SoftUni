package exercise_1.services;

import exercise_1.dtos.UserInputDto;
import exercise_1.models.User;
import jakarta.validation.Valid;
import org.springframework.validation.annotation.Validated;

@Validated
public interface UserService {
    User createUser(@Valid UserInputDto dto);
}
