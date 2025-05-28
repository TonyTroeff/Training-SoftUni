package exercise_1;

import exercise_1.dtos.UserDto;
import exercise_1.dtos.UserLoginDto;
import exercise_1.dtos.UserRegisterDto;
import exercise_1.services.GameService;
import exercise_1.services.UserService;
import org.springframework.boot.CommandLineRunner;
import org.springframework.stereotype.Component;

import java.util.HashMap;
import java.util.Map;
import java.util.Scanner;
import java.util.function.Function;

@Component
public class Runner implements CommandLineRunner {
    private final GameService gameService;
    private final UserService userService;

    private UserDto user;

    public Runner(GameService gameService, UserService userService) {
        this.gameService = gameService;
        this.userService = userService;
    }

    @Override
    public void run(String... args) {
        Scanner scanner = new Scanner(System.in);

        Map<String, Function<String[], String>> commands = new HashMap<>();
        commands.put("RegisterUser", this::registerUser);
        commands.put("Login", this::login);
        commands.put("Logout", this::logout);

        String input;
        while (!(input = scanner.nextLine()).equals("Exit")) {
            String[] data = input.split("\\s+");
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

        return "A new user has been registered successfully!";
    }

    private String login(String[] args) {
        if (this.user != null) return "There is already a logged in user.";

        UserLoginDto dto = new UserLoginDto(args[1], args[2]);
        UserDto user = this.userService.login(dto);

        if (user == null) return "Invalid username or password!";

        this.user = user;
        return String.format("Hello, %s!", user.getFullName());
    }

    private String logout(String[] args) {
        if (this.user == null) return "You are not logged in.";

        this.user = null;
        return "You have successfully logged out.";
    }
}
