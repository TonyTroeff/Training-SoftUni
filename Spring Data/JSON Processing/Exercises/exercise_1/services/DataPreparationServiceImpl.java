package exercise_1.services;

import com.google.gson.Gson;
import exercise_1.dtos.CategoryInputDto;
import exercise_1.dtos.ProductInputDto;
import exercise_1.dtos.UserInputDto;
import exercise_1.models.Category;
import exercise_1.models.User;
import org.springframework.core.io.ClassPathResource;
import org.springframework.stereotype.Service;
import org.springframework.transaction.annotation.Transactional;

import java.io.BufferedReader;
import java.io.IOException;
import java.io.InputStream;
import java.io.InputStreamReader;
import java.util.ArrayList;
import java.util.Collections;
import java.util.List;
import java.util.concurrent.ThreadLocalRandom;

@Service
public class DataPreparationServiceImpl implements DataPreparationService {
    private final CategoryService categoryService;
    private final ProductService productService;
    private final UserService userService;
    private final Gson gson;

    public DataPreparationServiceImpl(CategoryService categoryService, ProductService productService, UserService userService, Gson gson) {
        this.categoryService = categoryService;
        this.productService = productService;
        this.userService = userService;
        this.gson = gson;
    }

    @Override
    @Transactional
    public void seedData() throws IOException {
        UserInputDto[] userDtos = this.gson.fromJson(readResourceFile("users.json"), UserInputDto[].class);
        ProductInputDto[] productDtos = this.gson.fromJson(readResourceFile("products.json"), ProductInputDto[].class);
        CategoryInputDto[] categoryDtos = this.gson.fromJson(readResourceFile("categories.json"), CategoryInputDto[].class);

        List<User> users = new ArrayList<>();
        for (UserInputDto dto : userDtos) {
            User user = this.userService.createUser(dto);
            users.add(user);
        }

        List<Category> categories = new ArrayList<>();
        for (CategoryInputDto dto : categoryDtos) {
            Category category = this.categoryService.createCategory(dto);
            categories.add(category);
        }

        ThreadLocalRandom random = ThreadLocalRandom.current();
        for (ProductInputDto dto : productDtos) {
            User randomSeller = users.get(random.nextInt(0, users.size()));
            dto.setSeller(randomSeller);

            Collections.shuffle(categories, random);
            List<Category> randomCategories = categories.subList(0, random.nextInt(1, categories.size()));
            dto.setCategories(randomCategories);

            if (random.nextBoolean()) {
                User randomBuyer = users.get(random.nextInt(0, users.size()));
                dto.setBuyer(randomBuyer);
            }

            this.productService.createProduct(dto);
        }
    }

    private static String readResourceFile(String path) throws IOException {
        ClassPathResource resource = new ClassPathResource(path);

        StringBuilder result = new StringBuilder();
        try (InputStream inputStream = resource.getInputStream()) {
            InputStreamReader inputStreamReader = new InputStreamReader(inputStream);
            BufferedReader reader = new BufferedReader(inputStreamReader);

            int read;
            while ((read = reader.read()) != -1)
                result.append((char) read);
        }

        return result.toString();
    }
}
