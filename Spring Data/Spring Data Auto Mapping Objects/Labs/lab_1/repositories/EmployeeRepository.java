package lab_1.repositories;

import lab_1.models.Employee;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.data.jpa.repository.Query;
import org.springframework.stereotype.Repository;

import java.util.List;

@Repository
public interface EmployeeRepository extends JpaRepository<Employee, Long> {
    @Query("select e from Employee as e join fetch e.subordinates where size(e.subordinates) > 0")
    List<Employee> findAllManagers();

    @Query("select e from Employee as e join fetch e.managers where size(e.managers) > 0")
    List<Employee> findAllSubordinates();
}