package exercise_1.services;

import exercise_1.dtos.AuthorSummaryDto;
import exercise_1.models.Author;
import exercise_1.repositories.AuthorRepository;
import org.springframework.stereotype.Service;

import java.time.LocalDate;
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
    public List<Author> findAuthorsWithBookReleasedBefore(int year) {
        LocalDate releaseDate = LocalDate.of(year, 1, 1);
        return this.authorRepository.findDistinctByBooks_ReleaseDateBefore(releaseDate);
    }

    @Override
    public List<AuthorSummaryDto> getSummary() {
        return this.authorRepository.getSummary();
    }
}
