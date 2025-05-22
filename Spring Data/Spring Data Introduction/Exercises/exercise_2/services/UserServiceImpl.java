package exercise_2.services;

import exercise_2.models.User;
import exercise_2.repositories.UserRepository;
import org.springframework.stereotype.Service;

import java.time.LocalDateTime;
import java.util.List;

@Service
public class UserServiceImpl implements UserService {
    private final UserRepository userRepository;

    public UserServiceImpl(UserRepository userRepository) {
        this.userRepository = userRepository;
    }

    @Override
    public User registerUser(String firstName, String lastName, String username, String password, String email, Integer age) {
        User user = new User();
        user.setFirstName(firstName);
        user.setLastName(lastName);
        user.setUsername(username);
        user.setPassword(password);
        user.setEmail(email);
        user.setRegisteredOn(LocalDateTime.now());
        user.setAge(age);

        return this.userRepository.save(user);
    }

    @Override
    public List<User> findByDomain(String domain) {
        String expectedSuffix = String.format("@%s", domain);
        return this.userRepository.findAllByEmailEndingWithIgnoreCase(expectedSuffix);
    }
}
