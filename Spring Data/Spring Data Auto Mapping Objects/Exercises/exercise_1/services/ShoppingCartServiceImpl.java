package exercise_1.services;

import exercise_1.dtos.ShoppingCartItemDto;
import exercise_1.models.Game;
import exercise_1.models.ShoppingCartItem;
import exercise_1.models.User;
import exercise_1.repositories.ShoppingCartItemRepository;
import jakarta.transaction.Transactional;
import org.springframework.stereotype.Service;

import java.util.List;

@Service
public class ShoppingCartServiceImpl implements ShoppingCartService {
    private final ShoppingCartItemRepository shoppingCartItemRepository;
    private final GameService gameService;
    private final UserService userService;

    public ShoppingCartServiceImpl(ShoppingCartItemRepository shoppingCartItemRepository, GameService gameService, UserService userService) {
        this.shoppingCartItemRepository = shoppingCartItemRepository;
        this.gameService = gameService;
        this.userService = userService;
    }

    @Override
    public ShoppingCartItem getShoppingCartItem(Long userId, String gameTitle) {
        return this.shoppingCartItemRepository.findByUserAndGame(userId, gameTitle);
    }

    @Override
    public List<ShoppingCartItemDto> getShoppingCartItemsByUser(Long userId) {
        return this.shoppingCartItemRepository.findAllByUser(userId);
    }

    @Override
    public void createShoppingCartItem(Long userId, String gameTitle) {
        User user = this.userService.getById(userId);
        if (user == null) throw new IllegalStateException("User was not found.");

        Game game = this.gameService.getByTitle(gameTitle);
        if (game == null) throw new IllegalStateException("Game was not found.");

        ShoppingCartItem item = new ShoppingCartItem();
        item.setUser(user);
        item.setGame(game);

        this.shoppingCartItemRepository.save(item);
    }

    @Override
    @Transactional
    public void deleteShoppingCartItem(Long id) {
        this.shoppingCartItemRepository.removeById(id);
    }

    @Override
    @Transactional
    public void deleteShoppingCartItems(List<Long> ids) {
        this.shoppingCartItemRepository.removeAllByIdIn(ids);
    }
}
