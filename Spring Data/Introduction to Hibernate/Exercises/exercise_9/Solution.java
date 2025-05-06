package exercise_9;

import dtos.EmployeeSummaryDto;
import jakarta.persistence.*;

import java.util.HashSet;
import java.util.List;
import java.util.Set;

public class Solution {
    public static void main(String[] args) {
        Set<String> departments = new HashSet<>();
        departments.add("Engineering");
        departments.add("Tool Design");
        departments.add("Marketing");
        departments.add("Information Services");

        List<EmployeeSummaryDto> result;
        try (EntityManagerFactory emf = Persistence.createEntityManagerFactory("application-unit")) {
            try (EntityManager em = emf.createEntityManager()) {
                TypedQuery<Integer> departmentIdsQuery = em.createQuery("select d.id from Department as d where d.name in (:departments)", Integer.class)
                        .setParameter("departments", departments);
                List<Integer> departmentIds = departmentIdsQuery.getResultList();

                TypedQuery<Integer> employeeIdsQuery = em.createQuery("select e.id from Employee as e where e.department.id in (:ids)", Integer.class)
                        .setParameter("ids", departmentIds);
                List<Integer> employeeIds = employeeIdsQuery.getResultList();

                em.getTransaction().begin();

                Query updateQuery = em.createQuery("update Employee as e set e.salary = e.salary * 1.12 where e.id in (:ids)")
                        .setParameter("ids", employeeIds);
                updateQuery.executeUpdate();

                TypedQuery<EmployeeSummaryDto> resultQuery = em.createQuery("select new dtos.EmployeeSummaryDto(e.id, e.firstName, e.lastName, e.jobTitle, e.salary) from Employee as e where e.id in (:ids)", EmployeeSummaryDto.class)
                        .setParameter("ids", employeeIds);
                result = resultQuery.getResultList();

                em.getTransaction().commit();
            }
        }

        for (EmployeeSummaryDto employee : result)
            System.out.printf("%s %s ($%.2f)%n", employee.firstName(), employee.lastName(), employee.salary());
    }
}
