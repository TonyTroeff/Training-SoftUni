package exercise_2.services;

import exercise_2.models.User;

import java.util.List;

public interface UserService {
    User registerUser(String firstName, String lastName, String username, String password, String email, Integer age);

    List<User> findByDomain(String domain);
}
