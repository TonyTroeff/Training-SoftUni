package exercise_1;

import exercise_1.dtos.*;
import exercise_1.models.ShoppingCartItem;
import exercise_1.services.GameService;
import exercise_1.services.OrderService;
import exercise_1.services.ShoppingCartService;
import exercise_1.services.UserService;
import org.springframework.boot.CommandLineRunner;
import org.springframework.stereotype.Component;

import java.math.BigDecimal;
import java.util.HashMap;
import java.util.List;
import java.util.Map;
import java.util.Scanner;
import java.util.function.Function;

@Component
public class Runner implements CommandLineRunner {
    private final GameService gameService;
    private final OrderService orderService;
    private final ShoppingCartService shoppingCartService;
    private final UserService userService;

    private UserDto user;

    public Runner(GameService gameService, OrderService orderService, ShoppingCartService shoppingCartService, UserService userService) {
        this.gameService = gameService;
        this.orderService = orderService;
        this.shoppingCartService = shoppingCartService;
        this.userService = userService;
    }

    @Override
    public void run(String... args) {
        Scanner scanner = new Scanner(System.in);

        Map<String, Function<String[], String>> commands = new HashMap<>();
        commands.put("RegisterUser", this::registerUser);
        commands.put("Login", this::login);
        commands.put("Logout", this::logout);
        commands.put("AddGame", this::addGame);
        commands.put("DeleteGame", this::deleteGame);
        commands.put("AllGames", this::showAllGames);
        commands.put("AddItem", this::addToCart);
        commands.put("RemoveItem", this::removeFromCart);
        commands.put("ShoppingCart", this::showGamesInShoppingCart);
        commands.put("Buy", this::buy);
        commands.put("Orders", this::showOrders);

        String input;
        while (!(input = scanner.nextLine()).equals("Exit")) {
            String[] data = input.split("\\|");
            String command = data[0];

            Function<String[], String> handler = commands.get(command);

            String result;
            try {
                if (handler == null) result = "Invalid command!";
                else result = handler.apply(data);
            } catch (Exception e) {
                result = e.getMessage();
            }

            System.out.println(result);
        }
    }

    private String registerUser(String[] args) {
        UserRegisterDto dto = new UserRegisterDto(args[1], args[2], args[3]);
        this.userService.registerUser(dto);

        return "A new user was registered successfully!";
    }

    private String login(String[] args) {
        if (this.user != null) return "There is already a logged in user.";

        UserLoginDto dto = new UserLoginDto(args[1], args[2]);
        UserDto user = this.userService.login(dto);

        if (user == null) return "Invalid email or password!";

        this.user = user;
        return String.format("Hello, %s!", user.getFullName());
    }

    private String logout(String[] args) {
        this.ensureUserIsLoggedIn();

        this.user = null;
        return "You have successfully logged out.";
    }

    private String addGame(String[] args) {
        this.ensureUserIsLoggedIn();

        GameInputDto dto = new GameInputDto(args[1], new BigDecimal(args[2]), args[3], args[4], args[5]);
        GameDto game = this.gameService.createGame(dto);

        return String.format("The game \"%s\" was created successfully.", game.getTitle());
    }

    private String deleteGame(String[] args) {
        this.ensureUserIsLoggedIn();

        Long id = Long.parseLong(args[1]);
        GameDto game = this.gameService.findById(id);
        if (game == null) return "The game was not found.";

        this.gameService.deleteGame(id);

        return String.format("The game \"%s\" was deleted successfully.", game.getTitle());
    }

    private String showAllGames(String[] args) {
        List<GameDto> games = this.gameService.all();
        return prepareGamesInfo(games);
    }

    private String addToCart(String[] args) {
        this.ensureUserIsLoggedIn();

        this.shoppingCartService.createShoppingCartItem(this.user.getId(), args[1]);

        return "The item was added to your shopping cart.";
    }

    private String removeFromCart(String[] args) {
        this.ensureUserIsLoggedIn();

        ShoppingCartItem item = this.shoppingCartService.getShoppingCartItem(this.user.getId(), args[1]);
        if (item == null) return "The item was not found in your shopping cart.";

        this.shoppingCartService.deleteShoppingCartItem(item.getId());

        return "The item was removed from your shopping cart.";
    }

    private String showGamesInShoppingCart(String[] args) {
        this.ensureUserIsLoggedIn();

        List<ShoppingCartItemDto> items = this.shoppingCartService.getShoppingCartItemsByUser(this.user.getId());
        if (items.isEmpty()) return "Your shopping cart is empty.";

        List<Long> gameIds = items.stream().map(ShoppingCartItemDto::gameId).toList();
        List<GameDto> games = this.gameService.findByIds(gameIds);
        return prepareGamesInfo(games);
    }

    private String buy(String[] args) {
        this.ensureUserIsLoggedIn();

        this.orderService.finishPurchase(this.user.getId());
        return "Your purchase was finished successfully.";
    }

    private String showOrders(String[] args) {
        this.ensureUserIsLoggedIn();

        List<OrderDto> orders = this.orderService.getOrdersByUser(this.user.getId());
        if (orders.isEmpty()) return "You have no orders.";

        StringBuilder sb = new StringBuilder();

        for (int i = 0; i < orders.size(); i++) {
            if (i > 0) sb.append(String.format("%n-----%n"));

            OrderDto currentOrder = orders.get(i);
            sb.append(String.format("Order #%d (%s):%n", i + 1, currentOrder.getPurchaseTime()));
            sb.append(prepareGamesInfo(currentOrder.getGames()));
        }

        return sb.toString();
    }

    private static String prepareGamesInfo(List<GameDto> games) {
        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < games.size(); i++) {
            if (i > 0) sb.append(System.lineSeparator());

            GameDto currentGame = games.get(i);
            sb.append(String.format("%d. %s %.2f", i + 1, currentGame.getTitle(), currentGame.getPrice()));
        }

        return sb.toString();
    }

    private boolean userIsLoggedIn() {
        return this.user != null;
    }

    private void ensureUserIsLoggedIn() {
        if (!this.userIsLoggedIn()) throw new IllegalStateException("You must be logged in to perform this action.");
    }
}
