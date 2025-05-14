package exercise_1.repositories;

import exercise_1.dtos.AuthorSummaryDto;
import exercise_1.models.Author;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.data.jpa.repository.Query;

import java.time.LocalDate;
import java.util.List;

public interface AuthorRepository extends JpaRepository<Author, Long> {
    List<Author> findDistinctByBooks_ReleaseDateBefore(LocalDate releaseDate);

    @Query("select new exercise_1.dtos.AuthorSummaryDto(a.firstName, a.lastName, size(a.books)) from Author as a order by size(a.books) desc")
    List<AuthorSummaryDto> getSummary();
}
