package exercise_1.services;

import exercise_1.dtos.GameDto;
import exercise_1.dtos.GameInputDto;
import exercise_1.models.Game;
import jakarta.validation.Valid;
import jakarta.validation.constraints.NotNull;
import org.springframework.validation.annotation.Validated;

import java.util.List;

@Validated
public interface GameService {
    List<Game> getByIds(@NotNull List<Long> ids);
    Game getById(@NotNull Long id);
    Game getByTitle(@NotNull String title);

    List<GameDto> all();
    List<GameDto> findByIds(@NotNull List<Long> ids);
    GameDto findById(@NotNull Long id);

    GameDto createGame(@Valid GameInputDto dto);
    void deleteGame(@NotNull Long id);
}
