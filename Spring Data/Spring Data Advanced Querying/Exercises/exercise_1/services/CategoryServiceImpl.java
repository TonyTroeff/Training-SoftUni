package exercise_1.services;

import exercise_1.models.Category;
import exercise_1.repositories.CategoryRepository;
import org.springframework.stereotype.Service;

@Service
public class CategoryServiceImpl implements CategoryService {
    private final CategoryRepository categoryRepository;

    public CategoryServiceImpl(CategoryRepository categoryRepository) {
        this.categoryRepository = categoryRepository;
    }

    @Override
    public Category createCategory(String name) {
        Category category = new Category();
        category.setName(name);

        return this.categoryRepository.save(category);
    }
}
