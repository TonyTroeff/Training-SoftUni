package exercise_2.services;

import exercise_2.dtos.SupplierDto;
import exercise_2.dtos.SupplierInputDto;
import exercise_2.dtos.SupplierReportDto;
import exercise_2.entities.Supplier;
import exercise_2.repositories.SupplierRepository;
import org.modelmapper.ModelMapper;
import org.springframework.stereotype.Service;

import java.util.List;

@Service
public class SupplierServiceImpl implements SupplierService {
    private final SupplierRepository repository;
    private final ModelMapper modelMapper;

    public SupplierServiceImpl(SupplierRepository repository, ModelMapper modelMapper) {
        this.repository = repository;
        this.modelMapper = modelMapper;
    }

    @Override
    public SupplierDto create(SupplierInputDto inputDto) {
        Supplier supplier = modelMapper.map(inputDto, Supplier.class);
        repository.save(supplier);

        return modelMapper.map(supplier, SupplierDto.class);
    }

    @Override
    public List<SupplierReportDto> generateReport(boolean isImporter) {
        return repository.generateReport(isImporter);
    }

    @Override
    public Supplier getReferenceById(Long id) {
        return repository.getReferenceById(id);
    }
}
