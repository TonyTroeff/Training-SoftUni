package exercise_2.repositories;

import exercise_2.dtos.SupplierReportDto;
import exercise_2.entities.Supplier;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.data.jpa.repository.Query;
import org.springframework.stereotype.Repository;

import java.util.List;

@Repository
public interface SupplierRepository extends JpaRepository<Supplier, Long> {
    @Query("select s.id, s.name, size(s.parts) from Supplier as s where s.isImporter = :isImporter")
    List<SupplierReportDto> generateReport(boolean isImporter);
}
