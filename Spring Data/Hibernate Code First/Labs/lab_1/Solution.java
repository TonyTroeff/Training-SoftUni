package lab_1;

import jakarta.persistence.EntityManager;
import jakarta.persistence.EntityManagerFactory;
import jakarta.persistence.Persistence;

public class Solution {
    public static void main(String[] args) {
        try (EntityManagerFactory emf = Persistence.createEntityManagerFactory("application-unit")) {
            try (EntityManager em = emf.createEntityManager()) {
                em.getTransaction().begin();
                em.getTransaction().commit();
            }
        }
    }
}
