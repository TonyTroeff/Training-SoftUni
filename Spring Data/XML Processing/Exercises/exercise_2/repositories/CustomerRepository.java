package exercise_2.repositories;

import exercise_2.entities.Customer;
import org.springframework.data.jpa.repository.JpaRepository;

import java.util.List;

public interface CustomerRepository extends JpaRepository<Customer, Long> {
    List<Customer> findAllByOrderByBirthDateAscIsYoungDriverAsc();
}
