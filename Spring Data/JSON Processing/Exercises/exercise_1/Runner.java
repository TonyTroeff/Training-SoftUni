package exercise_1;

import com.google.gson.Gson;
import exercise_1.dtos.ProductSummaryDto;
import exercise_1.services.CategoryService;
import exercise_1.services.DataPreparationService;
import exercise_1.services.ProductService;
import exercise_1.services.UserService;
import org.springframework.boot.CommandLineRunner;
import org.springframework.stereotype.Component;

import java.io.IOException;
import java.math.BigDecimal;
import java.util.List;

@Component
public class Runner implements CommandLineRunner {
    private final DataPreparationService dataPreparationService;
    private final CategoryService categoryService;
    private final ProductService productService;
    private final UserService userService;
    private final Gson gson;

    public Runner(DataPreparationService dataPreparationService, CategoryService categoryService, ProductService productService, UserService userService, Gson gson) {
        this.dataPreparationService = dataPreparationService;
        this.categoryService = categoryService;
        this.productService = productService;
        this.userService = userService;
        this.gson = gson;
    }

    @Override
    public void run(String... args) throws IOException {
        this.dataPreparationService.seedData();

        System.out.println("Query #1:");
        List<ProductSummaryDto> activeProductsInRange = this.productService.findActiveOffersInPriceRange(BigDecimal.valueOf(500), BigDecimal.valueOf(1000));
        System.out.println(this.gson.toJson(activeProductsInRange));
    }
}
