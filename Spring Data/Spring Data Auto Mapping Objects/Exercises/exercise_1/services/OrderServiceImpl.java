package exercise_1.services;

import exercise_1.dtos.OrderDto;
import exercise_1.dtos.ShoppingCartItemDto;
import exercise_1.models.Game;
import exercise_1.models.Order;
import exercise_1.models.User;
import exercise_1.repositories.OrderRepository;
import jakarta.transaction.Transactional;
import org.modelmapper.ModelMapper;
import org.springframework.stereotype.Service;

import java.time.LocalDateTime;
import java.util.List;

@Service
public class OrderServiceImpl implements OrderService {
    private final OrderRepository orderRepository;
    private final GameService gameService;
    private final ShoppingCartService shoppingCartService;
    private final UserService userService;
    private final ModelMapper modelMapper;

    public OrderServiceImpl(OrderRepository orderRepository, GameService gameService, ShoppingCartService shoppingCartService, UserService userService, ModelMapper modelMapper) {
        this.orderRepository = orderRepository;
        this.gameService = gameService;
        this.shoppingCartService = shoppingCartService;
        this.userService = userService;
        this.modelMapper = modelMapper;
    }

    @Override
    @Transactional
    public void finishPurchase(Long userId) {
        Order order = new Order();

        User user = this.userService.getById(userId);
        if (user == null) throw new IllegalStateException("User was not found.");

        List<ShoppingCartItemDto> items = this.shoppingCartService.getShoppingCartItemsByUser(userId);
        if (items.isEmpty()) throw new IllegalStateException("Purchase cannot be finished for empty shopping carts.");

        List<Long> gameIds = items.stream().map(ShoppingCartItemDto::gameId).toList();
        List<Game> games = this.gameService.getByIds(gameIds);

        order.setUser(user);
        order.getGames().addAll(games);
        order.setPurchaseTime(LocalDateTime.now());

        this.orderRepository.save(order);

        List<Long> shoppingCartItemIds = items.stream().map(ShoppingCartItemDto::id).toList();
        this.shoppingCartService.deleteShoppingCartItems(shoppingCartItemIds);
    }

    @Override
    public List<OrderDto> getOrdersByUser(Long userId) {
        List<Order> orders = this.orderRepository.findAllByUserId(userId);
        return orders.stream().map(x -> this.modelMapper.map(x, OrderDto.class)).toList();
    }
}
