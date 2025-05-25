package exercise_1;

import exercise_1.dtos.AuthorSalesDto;
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

        System.out.println("Task #1.1");
        this.bookService.findAllByAgeRestriction(AgeRestriction.Minor).forEach(this::printTitle);

        System.out.println("Task #1.2");
        this.bookService.findAllByAgeRestriction(AgeRestriction.Teen).forEach(this::printTitle);

        System.out.println("Task #2");
        this.bookService.findAllGoldenBooks().forEach(this::printTitle);

        System.out.println("Task #3");
        this.bookService.findAllOutsidePriceBounds(BigDecimal.valueOf(5), BigDecimal.valueOf(40)).forEach(this::printTitleAndPrice);

        System.out.println("Task #4.1");
        this.bookService.findAllNotReleasedInYear(2000).forEach(this::printTitle);

        System.out.println("Task #4.2");
        this.bookService.findAllNotReleasedInYear(1998).forEach(this::printTitle);

        System.out.println("Task #5.1");
        this.bookService.findAllReleasedBefore(LocalDate.of(1992, 4, 12)).forEach(this::printTitleEditionTypeAndPrice);

        System.out.println("Task #5.2");
        this.bookService.findAllReleasedBefore(LocalDate.of(1989, 12, 30)).forEach(this::printTitleEditionTypeAndPrice);

        System.out.println("Task #6.1");
        this.authorService.findAllWhoseFirstNameEndsWith("e").forEach(this::printName);

        System.out.println("Task #6.2");
        this.authorService.findAllWhoseFirstNameEndsWith("dy").forEach(this::printName);

        System.out.println("Task #7.1");
        this.bookService.searchByTitle("sK").forEach(this::printTitle);

        System.out.println("Task #7.2");
        this.bookService.searchByTitle("WOR").forEach(this::printTitle);

        System.out.println("Task #8.1");
        this.bookService.searchByAuthor("Ric").forEach(this::printTitleAndAuthor);

        System.out.println("Task #8.2");
        this.bookService.searchByAuthor("gr").forEach(this::printTitleAndAuthor);

        System.out.println("Task #9.1");
        System.out.println(this.bookService.countBooksWithTitleLongerThan(12));

        System.out.println("Task #9.2");
        System.out.println(this.bookService.countBooksWithTitleLongerThan(40));

        System.out.println("Task #10");
        this.authorService.aggregateSales().forEach(this::printNameAndSales);
    }

    private void printTitle(Book book) {
        System.out.printf("%s%n", book.getTitle());
    }

    private void printTitleAndPrice(Book book) {
        System.out.printf("%s - $%.2f%n", book.getTitle(), book.getPrice());
    }

    private void printTitleEditionTypeAndPrice(Book book) {
        System.out.printf("%s %s $%.2f%n", book.getTitle(), book.getEditionType(), book.getPrice());
    }

    private void printTitleAndAuthor(Book book) {
        System.out.printf("%s (%s %s)%n", book.getTitle(), book.getAuthor().getFirstName(), book.getAuthor().getLastName());
    }

    private void printName(Author author) {
        System.out.printf("%s %s%n", author.getFirstName(), author.getLastName());
    }

    private void printNameAndSales(AuthorSalesDto author) {
        System.out.printf("%s %s - %d%n", author.firstName(), author.lastName(), author.soldCopies());
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
