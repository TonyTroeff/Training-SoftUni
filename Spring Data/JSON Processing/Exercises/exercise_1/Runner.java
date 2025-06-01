package exercise_1;

import com.google.gson.Gson;
import exercise_1.dtos.CategoryInputDto;
import exercise_1.dtos.UserInputDto;
import exercise_1.models.Category;
import exercise_1.models.User;
import exercise_1.services.CategoryService;
import exercise_1.services.UserService;
import org.springframework.boot.CommandLineRunner;
import org.springframework.core.io.ClassPathResource;
import org.springframework.stereotype.Component;

import java.io.BufferedReader;
import java.io.IOException;
import java.io.InputStream;
import java.io.InputStreamReader;
import java.util.ArrayList;
import java.util.List;

@Component
public class Runner implements CommandLineRunner {
    private final CategoryService categoryService;
    private final UserService userService;
    private final Gson gson;

    public Runner(CategoryService categoryService, UserService userService, Gson gson) {
        this.categoryService = categoryService;
        this.userService = userService;
        this.gson = gson;
    }

    @Override
    public void run(String... args) throws IOException {
        UserInputDto[] userDtos = this.gson.fromJson(readResourceFile("users.json"), UserInputDto[].class);
        CategoryInputDto[] categoryDtos = this.gson.fromJson(readResourceFile("categories.json"), CategoryInputDto[].class);

        List<User> users = new ArrayList<>();
        for (UserInputDto dto : userDtos) {
            User user = this.userService.createUser(dto);
            users.add(user);
        }

        List<Category> categories = new ArrayList<>();
        for (CategoryInputDto dto: categoryDtos) {
            Category category = this.categoryService.createCategory(dto);
            categories.add(category);
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
