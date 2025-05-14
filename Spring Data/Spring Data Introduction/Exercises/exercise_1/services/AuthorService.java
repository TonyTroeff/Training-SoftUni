package exercise_1.services;

import exercise_1.dtos.AuthorSummaryDto;
import exercise_1.models.Author;

import java.util.List;

public interface AuthorService {
    Author createAuthor(String firstName, String lastName);

    List<Author> findAuthorsWithBookReleasedBefore(int year);
    List<AuthorSummaryDto> getSummary();
}
