package exercise_2.services;

import exercise_2.dtos.CustomerDto;
import exercise_2.dtos.CustomerInputDto;
import exercise_2.entities.Customer;
import jakarta.validation.Valid;
import org.springframework.validation.annotation.Validated;

import java.util.List;

@Validated
public interface CustomerService {
    CustomerDto create(@Valid CustomerInputDto inputDto);

    List<CustomerDto> exportAll();

    Customer getReferenceById(Long id);
}
