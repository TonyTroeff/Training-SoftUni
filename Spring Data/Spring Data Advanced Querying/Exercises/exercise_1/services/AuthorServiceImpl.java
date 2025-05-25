package exercise_1.services;

import exercise_1.dtos.AuthorSalesDto;
import exercise_1.models.Author;
import exercise_1.repositories.AuthorRepository;
import org.springframework.stereotype.Service;

import java.util.List;

@Service
public class AuthorServiceImpl implements AuthorService {
    private final AuthorRepository authorRepository;

    public AuthorServiceImpl(AuthorRepository authorRepository) {
        this.authorRepository = authorRepository;
    }

    @Override
    public Author createAuthor(String firstName, String lastName) {
        Author author = new Author();
        author.setFirstName(firstName);
        author.setLastName(lastName);

        return this.authorRepository.save(author);
    }

    @Override
    public List<Author> findAllWhoseFirstNameEndsWith(String suffix) {
        return this.authorRepository.findAllByFirstNameEndingWith(suffix);
    }

    @Override
    public List<AuthorSalesDto> aggregateSales() {
        return this.authorRepository.aggregateSales();
    }

    @Override
    public int countBooks(String firstName, String lastName) {
        return this.authorRepository.countBooks(firstName, lastName);
    }
}
