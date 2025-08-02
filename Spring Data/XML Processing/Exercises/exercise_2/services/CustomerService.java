package exercise_2.services;

import exercise_2.dtos.CustomerDto;
import exercise_2.dtos.CustomerInputDto;
import jakarta.validation.Valid;
import org.springframework.validation.annotation.Validated;

@Validated
public interface CustomerService {
    CustomerDto create(@Valid CustomerInputDto inputDto);
}
