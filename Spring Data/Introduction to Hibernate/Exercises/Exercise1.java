import jakarta.persistence.EntityManager;
import jakarta.persistence.EntityManagerFactory;
import jakarta.persistence.Persistence;
import jakarta.persistence.Query;

public class Exercise1 {
    public static void main(String[] args) {
        try (EntityManagerFactory emf = Persistence.createEntityManagerFactory("application-unit")) {
            try (EntityManager em = emf.createEntityManager()) {
                em.getTransaction().begin();

                Query updateQuery = em.createQuery("update Town as t set t.name = upper(t.name) where length(t.name) <= 5");
                updateQuery.executeUpdate();

                em.getTransaction().commit();
            }
        }
    }
}
