package lab_1.services;

import jakarta.persistence.EntityNotFoundException;
import lab_1.models.Account;
import lab_1.models.User;
import lab_1.repositories.AccountRepository;
import org.springframework.stereotype.Service;

import java.math.BigDecimal;
import java.util.Optional;

@Service
public class AccountServiceImpl implements AccountService {
    private final AccountRepository accountRepository;

    public AccountServiceImpl(AccountRepository accountRepository) {
        this.accountRepository = accountRepository;
    }

    @Override
    public Account createAccount(User user) {
        Account account = new Account();
        account.setUser(user);
        account.setBalance(BigDecimal.ZERO);

        return this.accountRepository.save(account);
    }

    @Override
    public void deposit(Long id, BigDecimal amount) {
        Account account = this.accountRepository.findById(id).orElseThrow(EntityNotFoundException::new);
        account.setBalance(Optional.of(account.getBalance()).orElse(BigDecimal.ZERO).add(amount));

        this.accountRepository.save(account);
    }

    @Override
    public void withdraw(Long id, BigDecimal amount) {
        Account account = this.accountRepository.findById(id).orElseThrow(EntityNotFoundException::new);
        account.setBalance(Optional.of(account.getBalance()).orElse(BigDecimal.ZERO).subtract(amount));

        this.accountRepository.save(account);
    }
}
