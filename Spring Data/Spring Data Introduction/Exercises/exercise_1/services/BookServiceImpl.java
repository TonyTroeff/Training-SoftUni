package exercise_1.services;

import exercise_1.enums.AgeRestriction;
import exercise_1.enums.BookEditionType;
import exercise_1.models.Author;
import exercise_1.models.Book;
import exercise_1.models.Category;
import exercise_1.repositories.BookRepository;
import org.springframework.stereotype.Service;

import java.math.BigDecimal;
import java.time.LocalDate;
import java.util.Set;

@Service
public class BookServiceImpl implements BookService {
    private final BookRepository bookRepository;

    public BookServiceImpl(BookRepository bookRepository) {
        this.bookRepository = bookRepository;
    }

    @Override
    public Book createBook(String title, String description, BookEditionType editionType, BigDecimal price, Integer copies, LocalDate releaseDate, AgeRestriction ageRestriction, Author author, Set<Category> categories) {
        Book book = new Book();
        book.setTitle(title);
        book.setDescription(description);
        book.setEditionType(editionType);
        book.setPrice(price);
        book.setCopies(copies);
        book.setReleaseDate(releaseDate);
        book.setAgeRestriction(ageRestriction);
        book.setAuthor(author);
        book.setCategories(categories);

        return this.bookRepository.save(book);
    }
}
