package exercise_1.services;

import exercise_1.dtos.AuthorSalesDto;
import exercise_1.models.Author;

import java.util.List;

public interface AuthorService {
    Author createAuthor(String firstName, String lastName);

    List<Author> findAllWhoseFirstNameEndsWith(String suffix);

    List<AuthorSalesDto> aggregateSales();
}
