package lab_1;

import lab_1.entities.Ingredient;
import lab_1.entities.Shampoo;
import lab_1.enums.Size;
import lab_1.repositories.IngredientRepository;
import lab_1.repositories.ShampooRepository;
import org.springframework.boot.CommandLineRunner;
import org.springframework.stereotype.Component;

import java.math.BigDecimal;
import java.util.List;

@Component
public class Runner implements CommandLineRunner {
    private final IngredientRepository ingredientRepository;
    private final ShampooRepository shampooRepository;

    public Runner(IngredientRepository ingredientRepository, ShampooRepository shampooRepository) {
        this.ingredientRepository = ingredientRepository;
        this.shampooRepository = shampooRepository;
    }

    @Override
    public void run(String... args) {
        System.out.println("Task #1");
        this.shampooRepository.findAllBySizeOrderById(Size.MEDIUM).forEach(this::printShampoo);

        System.out.println("Task #2");
        this.shampooRepository.findAllBySizeOrLabelIdOrderByPriceAsc(Size.MEDIUM, 10L).forEach(this::printShampoo);

        System.out.println("Task #3");
        this.shampooRepository.findAllByPriceGreaterThanOrderByPriceDesc(BigDecimal.valueOf(5)).forEach(this::printShampoo);

        System.out.println("Task #4");
        this.ingredientRepository.findAllByNameStartingWith("M").forEach(this::printIngredient);

        System.out.println("Task #5");
        this.ingredientRepository.findAllByNameInOrderByPriceAsc(List.of("Lavender", "Herbs", "Apple")).forEach(this::printIngredient);

        System.out.println("Task #6");
        System.out.printf("%d%n", this.shampooRepository.countAllByPriceLessThan(BigDecimal.valueOf(8.5)));

        System.out.println("Task #7");
        this.shampooRepository.findDistinctBrandsByIngredients(List.of("Berry", "Mineral-Collagen")).forEach(System.out::println);

        System.out.println("Task #8");
        this.shampooRepository.findDistinctBrandsHavingFewerIngredients(2).forEach(System.out::println);

        this.ingredientRepository.flush();

        System.out.println("Task #9");
        this.ingredientRepository.deleteByName("Zinc Pyrithione");

        System.out.println("Task #10");
        this.ingredientRepository.increasePrice(0.1);
    }

    private void printIngredient(Ingredient ingredient) {
        System.out.printf("%s%n", ingredient.getName());
    }

    private void printShampoo(Shampoo shampoo) {
        System.out.printf("%s %s %.2flv.%n", shampoo.getBrand(), shampoo.getSize(), shampoo.getPrice());
    }
}
