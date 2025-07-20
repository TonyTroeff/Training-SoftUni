package exercise_2.services;

import exercise_2.dtos.CarDto;
import exercise_2.dtos.CarExtendedDto;
import exercise_2.dtos.CarInputDto;
import exercise_2.dtos.CarRelationsDto;
import jakarta.validation.Valid;
import org.springframework.validation.annotation.Validated;

import java.util.List;

@Validated
public interface CarService {
    CarDto create(@Valid CarInputDto inputDto, @Valid CarRelationsDto relationsDto);

    List<CarExtendedDto> getExtended();
}
