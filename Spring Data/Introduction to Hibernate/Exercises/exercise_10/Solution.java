package exercise_10;

import dtos.EmployeeSummaryDto;
import jakarta.persistence.EntityManager;
import jakarta.persistence.EntityManagerFactory;
import jakarta.persistence.Persistence;
import jakarta.persistence.TypedQuery;

import java.util.List;
import java.util.Scanner;

public class Solution {
    public static void main(String[] args) {
        Scanner scanner = new Scanner(System.in);
        String pattern = scanner.nextLine();

        List<EmployeeSummaryDto> result;
        try (EntityManagerFactory emf = Persistence.createEntityManagerFactory("application-unit")) {
            try (EntityManager em = emf.createEntityManager()) {
                TypedQuery<EmployeeSummaryDto> query = em.createQuery("select new dtos.EmployeeSummaryDto(e.id, e.firstName, e.lastName, e.jobTitle, e.salary) from Employee as e where e.firstName like :pattern", EmployeeSummaryDto.class)
                        .setParameter("pattern", pattern + "%");
                result = query.getResultList();
            }
        }

        for (EmployeeSummaryDto employee : result)
            System.out.printf("%s %s - %s - ($%.2f)%n", employee.firstName(), employee.lastName(), employee.jobTitle(), employee.salary());
    }
}
