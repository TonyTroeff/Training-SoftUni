package lab_1.repositories;

import jakarta.transaction.Transactional;
import lab_1.entities.Ingredient;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.data.jpa.repository.Modifying;
import org.springframework.data.jpa.repository.Query;
import org.springframework.stereotype.Repository;

import java.util.Collection;
import java.util.List;

@Repository
public interface IngredientRepository extends JpaRepository<Ingredient, Long> {
    List<Ingredient> findAllByNameStartingWith(String prefix);

    List<Ingredient> findAllByNameInOrderByPriceAsc(Collection<String> names);

    @Modifying
    @Transactional
    @Query("delete from Ingredient as i where i.name in :name")
    void deleteByName(String name);

    @Modifying
    @Transactional
    @Query("update Ingredient as i set i.price = i.price * (1.0 + :percentageIncrease)")
    void increasePrice(double percentageIncrease);
}
