package exercise_2.services;

import exercise_2.dtos.PartDto;
import exercise_2.dtos.PartInputDto;
import exercise_2.dtos.PartRelationsDto;
import exercise_2.entities.Part;
import jakarta.validation.Valid;
import org.springframework.validation.annotation.Validated;

@Validated
public interface PartService {
    PartDto create(@Valid PartInputDto inputDto, @Valid PartRelationsDto relationsDto);

    Part getReferenceById(Long id);
}
