package exercise_2.repositories;

import exercise_2.entities.Car;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.data.jpa.repository.Query;
import org.springframework.stereotype.Repository;

import java.util.List;

@Repository
public interface CarRepository extends JpaRepository<Car, Long> {
    @Query("select c from Car as c left join fetch c.parts")
    List<Car> findAllWithPrefetchedParts();

    List<Car> findAllByMake(String make);
}
