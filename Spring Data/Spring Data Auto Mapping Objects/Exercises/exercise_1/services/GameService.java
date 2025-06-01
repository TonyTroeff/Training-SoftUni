package exercise_1.services;

import exercise_1.dtos.GameDto;
import exercise_1.dtos.GameInputDto;
import jakarta.validation.Valid;
import jakarta.validation.constraints.NotNull;
import org.springframework.validation.annotation.Validated;

import java.util.List;

@Validated
public interface GameService {
    List<GameDto> all();
    GameDto findById(@NotNull Long id);
    GameDto createGame(@Valid GameInputDto dto);
    void deleteGame(@NotNull Long id);
}
