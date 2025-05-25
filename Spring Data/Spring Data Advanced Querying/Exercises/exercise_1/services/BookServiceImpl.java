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
import java.util.List;
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

    @Override
    public List<Book> findAllByAgeRestriction(AgeRestriction ageRestriction) {
        return this.bookRepository.findAllByAgeRestriction(ageRestriction);
    }

    @Override
    public List<Book> findAllGoldenBooks() {
        return this.bookRepository.findAllByEditionTypeAndCopiesLessThan(BookEditionType.Gold, 5000);
    }

    @Override
    public List<Book> findAllOutsidePriceBounds(BigDecimal min, BigDecimal max) {
        return this.bookRepository.findAllByPriceLessThanOrPriceGreaterThan(min, max);
    }

    @Override
    public List<Book> findAllNotReleasedInYear(int year) {
        LocalDate before = LocalDate.of(year, 1, 1);
        LocalDate after = LocalDate.of(year, 12, 31);
        return this.bookRepository.findAllByReleaseDateLessThanOrReleaseDateGreaterThan(before, after);
    }

    @Override
    public List<Book> findAllReleasedBefore(LocalDate date) {
        return this.bookRepository.findAllByReleaseDateLessThan(date);
    }

    @Override
    public List<Book> searchByTitle(String searchTerm) {
        return this.bookRepository.findAllByTitleContainingIgnoreCase(searchTerm);
    }

    @Override
    public List<Book> searchByAuthor(String lastNamePrefix) {
        return this.bookRepository.findAllByAuthor(lastNamePrefix);
    }

    @Override
    public int countBooksWithTitleLongerThan(int length) {
        return this.bookRepository.countAllByTitleLengthGreaterThan(length);
    }

    @Override
    public int increaseCopies(LocalDate releasedAfter, int amount) {
        int modifiedBooks = this.bookRepository.increaseCopies(releasedAfter, amount);
        return modifiedBooks * amount;
    }

    @Override
    public int removeLeastSoldBooks(int threshold) {
        return this.bookRepository.removeAllByCopiesLessThan(threshold);
    }
}
