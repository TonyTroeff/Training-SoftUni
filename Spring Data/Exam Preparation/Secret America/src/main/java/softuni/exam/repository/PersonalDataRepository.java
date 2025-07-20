package softuni.exam.repository;

import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.stereotype.Repository;
import softuni.exam.models.entity.PersonalData;

@Repository
public interface PersonalDataRepository extends JpaRepository<PersonalData, Long> {
}
