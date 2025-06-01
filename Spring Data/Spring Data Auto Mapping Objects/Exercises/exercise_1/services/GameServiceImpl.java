package exercise_1.services;

import exercise_1.dtos.GameDto;
import exercise_1.dtos.GameInputDto;
import exercise_1.models.Game;
import exercise_1.repositories.GameRepository;
import jakarta.transaction.Transactional;
import org.modelmapper.ModelMapper;
import org.springframework.stereotype.Service;

import java.time.LocalDate;
import java.util.List;

@Service
public class GameServiceImpl implements GameService {
    private final GameRepository gameRepository;
    private final ModelMapper modelMapper;

    public GameServiceImpl(GameRepository gameRepository, ModelMapper modelMapper) {
        this.gameRepository = gameRepository;
        this.modelMapper = modelMapper;
    }

    @Override
    public List<GameDto> all() {
        List<Game> games = this.gameRepository.findAll();
        return games.stream()
                .map(x -> this.modelMapper.map(x, GameDto.class))
                .toList();
    }

    @Override
    public GameDto findById(Long id) {
        Game game = this.gameRepository.findById(id).orElse(null);
        if (game == null) return null;

        return this.modelMapper.map(game, GameDto.class);
    }

    @Override
    public GameDto createGame(GameInputDto dto) {
        Game game = this.modelMapper.map(dto, Game.class);
        game.setReleaseDate(LocalDate.now());

        this.gameRepository.save(game);
        return this.modelMapper.map(game, GameDto.class);
    }

    @Override
    @Transactional
    public void deleteGame(Long id) {
        this.gameRepository.removeById(id);
    }
}
