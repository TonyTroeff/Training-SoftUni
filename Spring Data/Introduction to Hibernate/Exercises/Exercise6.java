import dtos.AddressDto;
import jakarta.persistence.EntityManager;
import jakarta.persistence.EntityManagerFactory;
import jakarta.persistence.Persistence;
import jakarta.persistence.TypedQuery;

import java.util.List;
import java.util.Optional;

public class Exercise6 {
    public static void main(String[] args) {
        List<AddressDto> result;

        try (EntityManagerFactory emf = Persistence.createEntityManagerFactory("application-unit")) {
            try (EntityManager em = emf.createEntityManager()) {
                TypedQuery<AddressDto> query = em.createQuery("select new dtos.AddressDto(a.text, a.town.name, size(a.employees)) from Address as a order by size(a.employees) desc", AddressDto.class)
                        .setMaxResults(10);
                result = query.getResultList();
            }
        }

        for (AddressDto dto : result)
            System.out.printf("%s, %s - %d employee(s)%n", dto.text(), Optional.ofNullable(dto.townName()).orElse("n/a"), dto.employeesCount());
    }
}

