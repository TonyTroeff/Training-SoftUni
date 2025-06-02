package exercise_1.services;

import exercise_1.dtos.ProductInputDto;
import exercise_1.dtos.ProductSummaryDto;
import exercise_1.models.Product;
import jakarta.validation.Valid;
import jakarta.validation.constraints.NotNull;
import org.springframework.validation.annotation.Validated;

import java.math.BigDecimal;
import java.util.List;

@Validated
public interface ProductService {
    Product createProduct(@Valid ProductInputDto dto);

    List<ProductSummaryDto> findActiveOffersInPriceRange(@NotNull BigDecimal min, @NotNull BigDecimal max);
}
