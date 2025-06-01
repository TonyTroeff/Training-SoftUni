package exercise_1.services;

import exercise_1.dtos.CategoryInputDto;
import exercise_1.models.Category;
import exercise_1.repositories.CategoryRepository;
import org.modelmapper.ModelMapper;
import org.springframework.stereotype.Service;

@Service
public class CategoryServiceImpl implements CategoryService {
    private final CategoryRepository repository;
    private final ModelMapper modelMapper;

    public CategoryServiceImpl(CategoryRepository repository, ModelMapper modelMapper) {
        this.repository = repository;
        this.modelMapper = modelMapper;
    }

    @Override
    public Category createCategory(CategoryInputDto dto) {
        Category category = this.modelMapper.map(dto, Category.class);
        return this.repository.save(category);
    }
}
