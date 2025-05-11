package lab_1.services;

import lab_1.models.Account;
import lab_1.models.User;

import java.math.BigDecimal;

public interface AccountService {
    Account createAccount(User user);
    void deposit(Long id, BigDecimal amount);
    void withdraw(Long id, BigDecimal amount);
}
