package exercise_2.services;

import exercise_2.dtos.PartDto;
import exercise_2.dtos.PartInputDto;
import exercise_2.dtos.PartRelationsDto;
import exercise_2.entities.Part;
import exercise_2.entities.Supplier;
import exercise_2.repositories.PartRepository;
import org.modelmapper.ModelMapper;
import org.springframework.stereotype.Service;

@Service
public class PartServiceImpl implements PartService {
    private final SupplierService supplierService;
    private final PartRepository repository;
    private final ModelMapper modelMapper;

    public PartServiceImpl(SupplierService supplierService, PartRepository repository, ModelMapper modelMapper) {
        this.supplierService = supplierService;
        this.repository = repository;
        this.modelMapper = modelMapper;
    }

    @Override
    public PartDto create(PartInputDto inputDto, PartRelationsDto relationsDto) {
        Part part = modelMapper.map(inputDto, Part.class);

        Supplier supplier = supplierService.getReferenceById(relationsDto.getSupplierId());
        part.setSupplier(supplier);

        repository.save(part);

        return modelMapper.map(part, PartDto.class);
    }

    @Override
    public Part getReferenceById(Long id) {
        return repository.getReferenceById(id);
    }
}
