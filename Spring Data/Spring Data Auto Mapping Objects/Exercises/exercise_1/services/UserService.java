package exercise_1.services;

import exercise_1.dtos.UserDto;
import exercise_1.dtos.UserLoginDto;
import exercise_1.dtos.UserRegisterDto;
import exercise_1.models.User;
import jakarta.validation.Valid;
import jakarta.validation.constraints.NotNull;
import org.springframework.validation.annotation.Validated;

@Validated
public interface UserService {
    void registerUser(@Valid UserRegisterDto dto);
    UserDto login(@NotNull UserLoginDto dto);
    User getById(@NotNull Long id);
}
