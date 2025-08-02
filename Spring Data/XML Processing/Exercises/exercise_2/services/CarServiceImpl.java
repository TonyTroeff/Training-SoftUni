package exercise_2.services;

import exercise_2.dtos.CarDto;
import exercise_2.dtos.CarExtendedDto;
import exercise_2.dtos.CarInputDto;
import exercise_2.dtos.CarRelationsDto;
import exercise_2.entities.Car;
import exercise_2.entities.Part;
import exercise_2.repositories.CarRepository;
import org.modelmapper.ModelMapper;
import org.springframework.stereotype.Service;

import java.util.ArrayList;
import java.util.HashSet;
import java.util.List;
import java.util.Set;

@Service
public class CarServiceImpl implements CarService {
    private final PartService partService;
    private final CarRepository repository;
    private final ModelMapper modelMapper;

    public CarServiceImpl(PartService partService, CarRepository repository, ModelMapper modelMapper) {
        this.partService = partService;
        this.repository = repository;
        this.modelMapper = modelMapper;
    }

    @Override
    public CarDto create(CarInputDto inputDto, CarRelationsDto relationsDto) {
        Car car = modelMapper.map(inputDto, Car.class);

        Set<Part> parts = new HashSet<>();
        for (Long partId : relationsDto.getPartIds()) {
            Part part = partService.getReferenceById(partId);
            parts.add(part);
        }

        car.setParts(parts);

        repository.save(car);

        return modelMapper.map(car, CarDto.class);
    }

    @Override
    public List<CarDto> exportAllByMake(String make) {
        List<Car> cars = repository.findAllByMake(make);

        List<CarDto> result = new ArrayList<>();
        for (Car car : cars) {
            CarDto carDto = modelMapper.map(car, CarDto.class);
            result.add(carDto);
        }

        return result;
    }

    @Override
    public List<CarExtendedDto> getExtended() {
        List<Car> cars = repository.findAllWithPrefetchedParts();

        List<CarExtendedDto> result = new ArrayList<>();
        for (Car car : cars) {
            CarExtendedDto carExtendedDto = modelMapper.map(car, CarExtendedDto.class);
            result.add(carExtendedDto);
        }

        return result;
    }

    @Override
    public Car getReferenceById(Long id) {
        return repository.getReferenceById(id);
    }
}
