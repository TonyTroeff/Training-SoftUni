package lab_1.services;

import lab_1.models.User;

public interface UserService {
    User registerUser(String username, Integer age);
}
