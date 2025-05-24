package lab_1.repositories;

import lab_1.entities.Shampoo;
import lab_1.enums.Size;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.data.jpa.repository.Query;
import org.springframework.stereotype.Repository;

import java.math.BigDecimal;
import java.util.Collection;
import java.util.List;

@Repository
public interface ShampooRepository extends JpaRepository<Shampoo, Long> {
    List<Shampoo> findAllBySizeOrderById(Size size);

    List<Shampoo> findAllBySizeOrLabelIdOrderByPriceAsc(Size size, Long labelId);

    List<Shampoo> findAllByPriceGreaterThanOrderByPriceDesc(BigDecimal lowerBoundary);

    long countAllByPriceLessThan(BigDecimal higherBoundary);

    @Query("select distinct s.brand from Shampoo as s join s.ingredients as i where i.name in :ingredients")
    List<String> findDistinctBrandsByIngredients(Collection<String> ingredients);

    @Query("select distinct s.brand from Shampoo as s where size(s.ingredients) < :threshold")
    List<String> findDistinctBrandsHavingFewerIngredients(int threshold);
}
