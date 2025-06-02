package exercise_1.repositories;

import exercise_1.dtos.ProductSummaryDto;
import exercise_1.models.Product;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.data.jpa.repository.Query;
import org.springframework.stereotype.Repository;

import java.math.BigDecimal;
import java.util.List;

@Repository
public interface ProductRepository extends JpaRepository<Product, Long> {
    @Query("select new exercise_1.dtos.ProductSummaryDto(p.name, p.price, case when p.seller.firstName is null then p.seller.lastName else concat(p.seller.firstName, ' ', p.seller.lastName) end) from Product as p where p.buyer is null and :minPrice <= p.price and p.price <= :maxPrice order by p.price asc")
    List<ProductSummaryDto> getProductsSummary(BigDecimal minPrice, BigDecimal maxPrice);
}
