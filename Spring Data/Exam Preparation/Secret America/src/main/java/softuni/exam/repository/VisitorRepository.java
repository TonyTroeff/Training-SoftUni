package softuni.exam.repository;

import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.stereotype.Repository;
import softuni.exam.models.entity.Visitor;

@Repository
public interface VisitorRepository extends JpaRepository<Visitor, Long> {
}
