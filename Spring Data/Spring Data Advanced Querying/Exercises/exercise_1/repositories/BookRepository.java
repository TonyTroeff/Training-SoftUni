package exercise_1.repositories;

import exercise_1.enums.AgeRestriction;
import exercise_1.enums.BookEditionType;
import exercise_1.models.Book;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.data.jpa.repository.Query;
import org.springframework.stereotype.Repository;

import java.math.BigDecimal;
import java.time.LocalDate;
import java.util.List;

@Repository
public interface BookRepository extends JpaRepository<Book, Long> {
    List<Book> findAllByAgeRestriction(AgeRestriction ageRestriction);

    List<Book> findAllByEditionTypeAndCopiesLessThan(BookEditionType editionType, int copies);

    List<Book> findAllByPriceLessThanOrPriceGreaterThan(BigDecimal lessThan, BigDecimal greaterThan);

    List<Book> findAllByReleaseDateLessThanOrReleaseDateGreaterThan(LocalDate before, LocalDate after);

    List<Book> findAllByReleaseDateLessThan(LocalDate date);

    List<Book> findAllByTitleContainingIgnoreCase(String title);

    @Query("select b from Book as b join fetch b.author where b.author.lastName like %:lastNamePrefix%")
    List<Book> findAllByAuthor(String lastNamePrefix);

    @Query("select count(b) from Book as b where length(b.title) > :minLength")
    long countAllByTitleLengthGreaterThan(int minLength);
}
