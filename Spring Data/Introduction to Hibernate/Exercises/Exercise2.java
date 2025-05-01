import jakarta.persistence.*;

import java.util.Scanner;

public class Exercise2 {
    public static void main(String[] args) {
        Scanner scanner = new Scanner(System.in);
        String[] names = scanner.nextLine().split("\\s+");
        String firstName = names[0];
        String lastName = names[1];

        long count;
        try (EntityManagerFactory emf = Persistence.createEntityManagerFactory("application-unit")) {
            try (EntityManager em = emf.createEntityManager()) {
                TypedQuery<Long> query = em.createQuery("select count(e) from Employee as e where e.firstName = :fn and e.lastName = :ln", Long.class)
                        .setParameter("fn", firstName)
                        .setParameter("ln", lastName);

                count = query.getSingleResult();
            }
        }

        System.out.println(count == 0 ? "No" : "Yes");
    }
}
