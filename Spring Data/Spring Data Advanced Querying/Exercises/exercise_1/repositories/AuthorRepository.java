package exercise_1.repositories;

import exercise_1.dtos.AuthorSalesDto;
import exercise_1.models.Author;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.data.jpa.repository.Query;
import org.springframework.data.jpa.repository.query.Procedure;
import org.springframework.data.repository.query.Param;
import org.springframework.stereotype.Repository;

import java.util.List;

@Repository
public interface AuthorRepository extends JpaRepository<Author, Long> {
    List<Author> findAllByFirstNameEndingWith(String suffix);

    @Query("select new exercise_1.dtos.AuthorSalesDto(a.firstName, a.lastName, sum(b.copies)) from Author as a join a.books as b group by a.id order by sum(b.copies) desc")
    List<AuthorSalesDto> aggregateSales();

    @Procedure(procedureName = "usp_count_books", outputParameterName = "books_count")
    int countBooks(@Param("author_first_name") String firstName, @Param("author_last_name") String lastName);
}
