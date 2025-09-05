package exercise_2.repositories;

import exercise_2.dtos.CustomerExtendedDto;
import exercise_2.entities.Customer;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.data.jpa.repository.Query;

import java.util.List;

public interface CustomerRepository extends JpaRepository<Customer, Long> {
    List<Customer> findAllByOrderByBirthDateAscIsYoungDriverAsc();

    @Query("select c.name, count(s), sum(p.price) from Customer as c join c.sales as s join s.car as cr join cr.parts as p group by c.id order by sum(p.price) desc, count(s) desc")
    List<CustomerExtendedDto> findAllWithAggregatedSales();
}
