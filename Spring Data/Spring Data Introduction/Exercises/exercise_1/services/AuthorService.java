package exercise_1.services;

import exercise_1.models.Author;

public interface AuthorService {
    Author createAuthor(String firstName, String lastName);
}
