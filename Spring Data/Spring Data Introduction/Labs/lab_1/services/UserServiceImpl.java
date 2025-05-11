package lab_1.services;

import lab_1.models.User;
import lab_1.repositories.UserRepository;
import org.springframework.stereotype.Service;

@Service
public class UserServiceImpl implements UserService {
    private final UserRepository userRepository;

    public UserServiceImpl(UserRepository userRepository) {
        this.userRepository = userRepository;
    }

    @Override
    public User registerUser(String username, Integer age) {
        var user = new User();
        user.setUsername(username);
        user.setAge(age);

        return this.userRepository.save(user);
    }
}
