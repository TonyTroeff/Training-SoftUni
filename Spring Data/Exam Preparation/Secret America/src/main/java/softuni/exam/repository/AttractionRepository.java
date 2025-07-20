package softuni.exam.repository;

import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.data.jpa.repository.Query;
import org.springframework.stereotype.Repository;
import softuni.exam.models.dto.AttractionExportDto;
import softuni.exam.models.entity.Attraction;

import java.util.Collection;
import java.util.List;

@Repository
public interface AttractionRepository extends JpaRepository<Attraction, Long> {
    @Query("select new softuni.exam.models.dto.AttractionExportDto(a.id, a.name, a.description, a.elevation, a.country.name) from Attraction as a where a.type in (:types) and a.elevation >= :minElevation order by a.name asc, a.country.name asc")
    List<AttractionExportDto> findExportable(Collection<String> types, Integer minElevation);
}
