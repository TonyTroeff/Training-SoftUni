import entities.Employee;
import jakarta.persistence.EntityManager;
import jakarta.persistence.EntityManagerFactory;
import jakarta.persistence.Persistence;
import jakarta.persistence.TypedQuery;

import java.util.List;

public class Exercise4 {
    public static void main(String[] args) {
        List<Employee> result;
        try (EntityManagerFactory emf = Persistence.createEntityManagerFactory("application-unit")) {
            try (EntityManager em = emf.createEntityManager()) {
                TypedQuery<Employee> query = em.createQuery("select e from Employee as e where e.department.name = 'Research and Development' order by e.salary asc, e.id asc", Employee.class);
                result = query.getResultList();
            }
        }

        for (Employee employee : result) {
            System.out.printf("%s %s from %s - $%.2f%n", employee.getFirstName(), employee.getLastName(), employee.getDepartment().getName(), employee.getSalary());
        }
    }
}
