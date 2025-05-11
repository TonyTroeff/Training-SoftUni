package lab_1;

import lab_1.models.Account;
import lab_1.models.User;
import lab_1.services.AccountService;
import lab_1.services.UserService;
import org.springframework.boot.CommandLineRunner;
import org.springframework.stereotype.Component;

import java.math.BigDecimal;

@Component
public class Runner implements CommandLineRunner {
    private final UserService userService;
    private final AccountService accountService;

    public Runner(UserService userService, AccountService accountService) {
        this.userService = userService;
        this.accountService = accountService;
    }

    @Override
    // @Transactional
    public void run(String... args) {
        User user = this.userService.registerUser("Troeff", 23);
        Account account = this.accountService.createAccount(user);
        this.accountService.deposit(account.getId(), BigDecimal.valueOf(16.18));
        this.accountService.withdraw(account.getId(), BigDecimal.valueOf(3.14));
    }
}
