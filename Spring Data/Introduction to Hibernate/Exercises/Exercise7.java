import dtos.EmployeeSummaryDto;
import jakarta.persistence.*;

import java.util.Collections;
import java.util.List;
import java.util.Scanner;

public class Exercise7 {
    public static void main(String[] args) {
        Scanner scanner = new Scanner(System.in);
        int id = Integer.parseInt(scanner.nextLine());

        EmployeeSummaryDto employee;
        List<String> projects = Collections.emptyList();
        try (EntityManagerFactory emf = Persistence.createEntityManagerFactory("application-unit")) {
            try (EntityManager em = emf.createEntityManager()) {
                TypedQuery<EmployeeSummaryDto> employeeQuery = em.createQuery("select new dtos.EmployeeDto(e.id, e.firstName, e.lastName, e.jobTitle, e.salary) from Employee as e where e.id = :id", EmployeeSummaryDto.class)
                        .setParameter("id", id);
                try {
                    employee = employeeQuery.getSingleResult();
                } catch (NoResultException e) {
                    employee = null;
                }

                if (employee != null) {
                    TypedQuery<String> projectsQuery = em.createQuery("select distinct p.name from Project as p join p.employees as e where e.id = :id order by p.name", String.class)
                            .setParameter("id", id);
                    projects = projectsQuery.getResultList();
                }
            }
        }

        if (employee == null) {
            System.out.println("No such employee.");
        }
        else {
            System.out.printf("%s %s - %s%n", employee.firstName(), employee.lastName(), employee.jobTitle());
            for (String projectName : projects)
                System.out.printf("    %s%n", projectName);
        }
    }
}
