package exercise_2.services;

import exercise_2.dtos.SaleDto;
import exercise_2.dtos.SaleInputDto;
import exercise_2.dtos.SaleRelationsDto;
import jakarta.validation.Valid;
import org.springframework.validation.annotation.Validated;

@Validated
public interface SaleService {
    SaleDto create(@Valid SaleInputDto inputDto, @Valid SaleRelationsDto relationsDto);
}
