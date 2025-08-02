package exercise_2.services;

import exercise_2.dtos.CustomerDto;
import exercise_2.dtos.CustomerInputDto;
import exercise_2.entities.Customer;
import exercise_2.repositories.CustomerRepository;
import org.modelmapper.ModelMapper;
import org.springframework.stereotype.Service;

@Service
public class CustomerServiceImpl implements CustomerService {
    private final CustomerRepository customerRepository;
    private final ModelMapper modelMapper;

    public CustomerServiceImpl(CustomerRepository customerRepository, ModelMapper modelMapper) {
        this.customerRepository = customerRepository;
        this.modelMapper = modelMapper;
    }

    @Override
    public CustomerDto create(CustomerInputDto inputDto) {
        Customer customer = modelMapper.map(inputDto, Customer.class);
        customerRepository.save(customer);

        return modelMapper.map(customer, CustomerDto.class);
    }
}
