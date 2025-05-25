package exercise_1.services;

import exercise_1.enums.AgeRestriction;
import exercise_1.enums.BookEditionType;
import exercise_1.models.Author;
import exercise_1.models.Book;
import exercise_1.models.Category;

import java.math.BigDecimal;
import java.time.LocalDate;
import java.util.List;
import java.util.Set;

public interface BookService {
    Book createBook(String title, String description, BookEditionType editionType, BigDecimal price, Integer copies, LocalDate releaseDate, AgeRestriction ageRestriction, Author author, Set<Category> categories);

    List<Book> findAllByAgeRestriction(AgeRestriction ageRestriction);

    List<Book> findAllGoldenBooks();

    List<Book> findAllOutsidePriceBounds(BigDecimal min, BigDecimal max);

    List<Book> findAllNotReleasedInYear(int year);

    List<Book> findAllReleasedBefore(LocalDate date);

    List<Book> searchByTitle(String searchTerm);

    List<Book> searchByAuthor(String lastNamePrefix);

    long countBooksWithTitleLongerThan(int length);
}
