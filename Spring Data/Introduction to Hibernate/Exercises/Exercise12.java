import jakarta.persistence.*;

import java.util.List;
import java.util.Optional;
import java.util.Scanner;

public class Exercise12 {
    public static void main(String[] args) {
        Scanner scanner = new Scanner(System.in);
        String townName = scanner.nextLine();

        Optional<Integer> townId;
        int deletedAddresses = 0;
        try (EntityManagerFactory emf = Persistence.createEntityManagerFactory("application-unit")) {
            try (EntityManager em = emf.createEntityManager()) {
                TypedQuery<Integer> townIdQuery = em.createQuery("select t.id from Town as t where t.name = :name", Integer.class)
                        .setParameter("name", townName);

                try {
                    townId = Optional.of(townIdQuery.getSingleResult());
                } catch (NoResultException e) {
                    townId = Optional.empty();
                }

                if (townId.isPresent()) {
                    TypedQuery<Integer> addressIdsQuery = em.createQuery("select a.id from Address as a where a.town.id = :id", Integer.class)
                            .setParameter("id", townId.get());
                    List<Integer> addressIds = addressIdsQuery.getResultList();

                    em.getTransaction().begin();

                    Query updateEmployeesQuery = em.createQuery("update Employee as e set e.address = null where e.address.id in (:ids)")
                            .setParameter("ids", addressIds);
                    updateEmployeesQuery.executeUpdate();

                    Query deleteAddressesQuery = em.createQuery("delete from Address as a where a.id in (:ids)")
                            .setParameter("ids", addressIds);
                    deleteAddressesQuery.executeUpdate();

                    Query deleteTownQuery = em.createQuery("delete from Town as t where t.id = :id")
                            .setParameter("id", townId.get());
                    deletedAddresses = deleteTownQuery.executeUpdate();

                    em.getTransaction().commit();
                }
            }
        }

        if (townId.isEmpty()) {
            System.out.println("No such town.");
        } else {
            System.out.printf("%d address in %s deleted%n", deletedAddresses, townName);
        }
    }
}
