package exercise_2.services;

import exercise_2.dtos.SaleDto;
import exercise_2.dtos.SaleInputDto;
import exercise_2.dtos.SaleRelationsDto;
import exercise_2.entities.Car;
import exercise_2.entities.Customer;
import exercise_2.entities.Sale;
import exercise_2.repositories.SaleRepository;
import org.modelmapper.ModelMapper;
import org.springframework.stereotype.Service;

@Service
public class SaleServiceImpl implements SaleService {
    private final CarService carService;
    private final CustomerService customerService;
    private final SaleRepository repository;
    private final ModelMapper modelMapper;

    public SaleServiceImpl(CarService carService, CustomerService customerService, SaleRepository repository, ModelMapper modelMapper) {
        this.carService = carService;
        this.customerService = customerService;
        this.repository = repository;
        this.modelMapper = modelMapper;
    }

    @Override
    public SaleDto create(SaleInputDto inputDto, SaleRelationsDto relationsDto) {
        Sale sale = modelMapper.map(inputDto, Sale.class);

        Car car = carService.getReferenceById(relationsDto.getCarId());
        sale.setCar(car);

        Customer customer = customerService.getReferenceById(relationsDto.getCustomerId());
        sale.setCustomer(customer);

        repository.save(sale);

        return modelMapper.map(sale, SaleDto.class);
    }
}
