package exercise_2.repositories;

import exercise_2.models.User;
import org.springframework.data.jpa.repository.JpaRepository;

import java.util.List;

public interface UserRepository extends JpaRepository<User, Long> {
    List<User> findAllByEmailEndingWithIgnoreCase(String suffix);
}
