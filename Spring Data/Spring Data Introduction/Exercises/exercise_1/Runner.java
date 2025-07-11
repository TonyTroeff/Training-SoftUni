package exercise_1;

import exercise_1.dtos.AuthorSummaryDto;
import exercise_1.enums.AgeRestriction;
import exercise_1.enums.BookEditionType;
import exercise_1.models.Author;
import exercise_1.models.Book;
import exercise_1.models.Category;
import exercise_1.services.AuthorService;
import exercise_1.services.BookService;
import exercise_1.services.CategoryService;
import org.springframework.boot.CommandLineRunner;
import org.springframework.core.io.ClassPathResource;
import org.springframework.stereotype.Component;

import java.io.BufferedReader;
import java.io.IOException;
import java.io.InputStream;
import java.io.InputStreamReader;
import java.math.BigDecimal;
import java.time.LocalDate;
import java.time.format.DateTimeFormatter;
import java.util.*;
import java.util.concurrent.ThreadLocalRandom;
import java.util.stream.Collectors;

@Component
public class Runner implements CommandLineRunner {
    private final AuthorService authorService;
    private final BookService bookService;
    private final CategoryService categoryService;

    public Runner(AuthorService authorService, BookService bookService, CategoryService categoryService) {
        this.authorService = authorService;
        this.bookService = bookService;
        this.categoryService = categoryService;
    }

    @Override
    public void run(String... args) throws Exception {
        this.seedData();

        this.executeQuery1();
        this.executeQuery2();
        this.executeQuery3();
        this.executeQuery4();
    }

    private void executeQuery1() {
        List<Book> books = this.bookService.findBooksReleasedAfter(2000);

        System.out.printf("There are %d books released after the year 2000:%n", books.size());
        for (Book book : books)
            System.out.println(book.getTitle());
    }

    private void executeQuery2() {
        List<Author> authors = this.authorService.findAuthorsWithBookReleasedBefore(1990);

        System.out.printf("There are %d authors with at least one book released before the year 1990:%n", authors.size());
        for (Author author : authors)
            System.out.printf("%s %s%n", author.getFirstName(), author.getLastName());
    }

    private void executeQuery3() {
        List<AuthorSummaryDto> authors = this.authorService.getSummary();

        System.out.printf("There are %d authors:%n", authors.size());
        for (AuthorSummaryDto author : authors)
            System.out.printf("%s %s (%d)%n", author.firstName(), author.lastName(), author.booksCount());
    }

    private void executeQuery4() {
        List<Book> books = this.bookService.findBooksWrittenBy("George", "Powell");

        System.out.printf("There are %d books written by George Powell:%n", books.size());
        for (Book book : books)
            System.out.printf("%s - %s, %d copies%n", book.getTitle(), book.getReleaseDate(), book.getCopies());
    }

    private void seedData() throws IOException {
        System.out.println("Start data seeding.");

        List<Author> authors = this.seedAuthors();
        List<Category> categories = this.seedCategories();
        List<Book> books = this.seedBooks(authors, categories);

        System.out.printf("Finished data seeding with %d authors, %d categories and %d books.%n", authors.size(), categories.size(), books.size());
    }

    private List<Author> seedAuthors() throws IOException {
        List<String> lines = readLinesFromResourceFile("authors.txt");
        List<Author> result = new ArrayList<>();

        for (String line : lines) {
            String[] data = line.split("\\s+");
            String firstName = data[0];
            String lastName = data[1];

            Author author = this.authorService.createAuthor(firstName, lastName);
            result.add(author);
        }

        return result;
    }

    private List<Category> seedCategories() throws IOException {
        List<String> lines = readLinesFromResourceFile("categories.txt");
        List<Category> result = new ArrayList<>();

        for (String categoryName : lines) {
            Category category = this.categoryService.createCategory(categoryName);
            result.add(category);
        }

        return result;
    }

    private List<Book> seedBooks(List<Author> allAuthors, List<Category> allCategories) throws IOException {
        List<String> lines = readLinesFromResourceFile("books.txt");
        List<Book> result = new ArrayList<>();

        for (String line : lines) {
            String[] data = line.split("\\s+");
            BookEditionType editionType = BookEditionType.values()[Integer.parseInt(data[0])];
            LocalDate releaseDate = LocalDate.parse(data[1], DateTimeFormatter.ofPattern("d/M/yyyy"));
            Integer copies = Integer.parseInt(data[2]);
            BigDecimal price = new BigDecimal(data[3]);
            AgeRestriction ageRestriction = AgeRestriction.values()[Integer.parseInt(data[4])];
            String title = Arrays.stream(data).skip(5).collect(Collectors.joining(" "));

            Author author = allAuthors.get(ThreadLocalRandom.current().nextInt(0, allAuthors.size()));

            int categoriesCount = ThreadLocalRandom.current().nextInt(5);
            Set<Category> categories = new HashSet<>();
            for (int i = 0; i < categoriesCount; i++)
                categories.add(allCategories.get(ThreadLocalRandom.current().nextInt(0, allCategories.size())));

            Book book = this.bookService.createBook(title, null, editionType, price, copies, releaseDate, ageRestriction, author, categories);
            result.add(book);
        }

        return result;
    }

    private static List<String> readLinesFromResourceFile(String path) throws IOException {
        ClassPathResource resource = new ClassPathResource(path);
        try (InputStream inputStream = resource.getInputStream()) {
            InputStreamReader inputStreamReader = new InputStreamReader(inputStream);
            BufferedReader reader = new BufferedReader(inputStreamReader);

            return reader.lines().toList();
        }
    }
}
