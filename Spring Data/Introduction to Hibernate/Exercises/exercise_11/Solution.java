package exercise_11;

import dtos.DepartmentSummaryDto;
import jakarta.persistence.EntityManager;
import jakarta.persistence.EntityManagerFactory;
import jakarta.persistence.Persistence;
import jakarta.persistence.TypedQuery;

import java.util.List;

public class Solution {
    public static void main(String[] args) {
        List<DepartmentSummaryDto> result;
        try (EntityManagerFactory emf = Persistence.createEntityManagerFactory("application-unit")) {
            try (EntityManager em = emf.createEntityManager()) {
                TypedQuery<DepartmentSummaryDto> query = em.createQuery("select new dtos.DepartmentSummaryDto(d.id, d.name, max(e.salary)) from Department as d join d.employees as e group by d.id having max(e.salary) not between 30000 and 70000", DepartmentSummaryDto.class);
                result = query.getResultList();
            }
        }

        for (DepartmentSummaryDto dto : result)
            System.out.printf("%s %.2f%n", dto.name(), dto.maxSalary());
    }
}
