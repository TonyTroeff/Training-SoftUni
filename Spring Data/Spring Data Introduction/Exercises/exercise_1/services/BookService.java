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

    List<Book> findBooksReleasedAfter(int year);
    List<Book> findBooksWrittenBy(String firstName, String lastName);
}
