import entities.Address;
import entities.Employee;
import jakarta.persistence.*;

import java.util.List;
import java.util.Scanner;

public class Exercise5 {
    public static void main(String[] args) {
        Scanner scanner = new Scanner(System.in);
        String employeeLastName = scanner.nextLine();

        List<Employee> employees;
        try (EntityManagerFactory emf = Persistence.createEntityManagerFactory("application-unit")) {
            try (EntityManager em = emf.createEntityManager()) {
                TypedQuery<Employee> query = em.createQuery("select e from Employee as e where e.lastName = :ln", Employee.class)
                        .setParameter("ln", employeeLastName);
                employees = query.getResultList();

                if (!employees.isEmpty()) {
                    em.getTransaction().begin();

                    Address address = new Address();
                    address.setText("Vitoshka 15");

                    em.persist(address);

                    // We can update all employees like this. However, this will result in N update requests.
                    // for (Employee employee : employees)
                    //     employee.setAddress(address);

                    // If we want to use a single (precise) update request, we can create and execute an additional query.
                    Query updateQuery = em.createQuery("update Employee as e set e.address = :address where e.id in (:ids)")
                            .setParameter("address", address)
                            .setParameter("ids", employees.stream().map(Employee::getId).toList());
                    updateQuery.executeUpdate();

                    em.getTransaction().commit();
                }
            }
        }

        if (employees.isEmpty()) {
            System.out.println("There are no employees with such last name.");
        } else {
            for (Employee employee : employees) {
                System.out.printf("Updated the address of %s %s.%n", employee.getFirstName(), employee.getLastName());
            }
        }
    }
}
