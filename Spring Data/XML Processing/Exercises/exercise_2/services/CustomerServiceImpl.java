package exercise_2.services;

import exercise_2.dtos.CustomerDto;
import exercise_2.dtos.CustomerExtendedDto;
import exercise_2.dtos.CustomerInputDto;
import exercise_2.entities.Customer;
import exercise_2.repositories.CustomerRepository;
import org.modelmapper.ModelMapper;
import org.springframework.stereotype.Service;

import java.util.ArrayList;
import java.util.List;

@Service
public class CustomerServiceImpl implements CustomerService {
    private final CustomerRepository repository;
    private final ModelMapper modelMapper;

    public CustomerServiceImpl(CustomerRepository repository, ModelMapper modelMapper) {
        this.repository = repository;
        this.modelMapper = modelMapper;
    }

    @Override
    public CustomerDto create(CustomerInputDto inputDto) {
        Customer customer = modelMapper.map(inputDto, Customer.class);
        repository.save(customer);

        return modelMapper.map(customer, CustomerDto.class);
    }

    @Override
    public List<CustomerDto> exportAll() {
        List<Customer> customers = repository.findAllByOrderByBirthDateAscIsYoungDriverAsc();

        List<CustomerDto> result = new ArrayList<>();
        for (Customer customer : customers) {
            CustomerDto customerDto = modelMapper.map(customer, CustomerDto.class);
            result.add(customerDto);
        }

        return result;
    }

    @Override
    public List<CustomerExtendedDto> exportExtended() {
        return repository.findAllWithAggregatedSales();
    }

    @Override
    public Customer getReferenceById(Long id) {
        return repository.getReferenceById(id);
    }
}
