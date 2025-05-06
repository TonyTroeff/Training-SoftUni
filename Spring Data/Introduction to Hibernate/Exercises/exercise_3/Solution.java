package exercise_3;

import jakarta.persistence.EntityManager;
import jakarta.persistence.EntityManagerFactory;
import jakarta.persistence.Persistence;
import jakarta.persistence.TypedQuery;

import java.util.List;

public class Solution {
    public static void main(String[] args) {
        List<String> result;
        try (EntityManagerFactory emf = Persistence.createEntityManagerFactory("application-unit")) {
            try (EntityManager em = emf.createEntityManager()) {
                // We only need to print the first name, so we can avoid loading unnecessary data.
                TypedQuery<String> query = em.createQuery("select e.firstName from Employee as e where e.salary > 50000", String.class);
                result = query.getResultList();
            }
        }

        result.forEach(System.out::println);
    }
}
