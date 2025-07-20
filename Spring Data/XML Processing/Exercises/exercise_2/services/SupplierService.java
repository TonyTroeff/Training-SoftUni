package exercise_2.services;

import exercise_2.dtos.SupplierDto;
import exercise_2.dtos.SupplierInputDto;
import exercise_2.dtos.SupplierReportDto;
import exercise_2.entities.Supplier;
import jakarta.validation.Valid;
import jakarta.validation.constraints.NotNull;
import org.springframework.validation.annotation.Validated;

import java.util.List;

@Validated
public interface SupplierService {
    SupplierDto create(@Valid SupplierInputDto inputDto);

    List<SupplierReportDto> generateReport(boolean isImporter);

    Supplier getReferenceById(@NotNull Long id);
}
