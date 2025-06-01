package exercise_1.repositories;

import exercise_1.dtos.ShoppingCartItemDto;
import exercise_1.models.ShoppingCartItem;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.data.jpa.repository.Query;
import org.springframework.stereotype.Repository;

import java.util.Collection;
import java.util.List;

@Repository
public interface ShoppingCartItemRepository extends JpaRepository<ShoppingCartItem, Long> {
    @Query(value = "select s from ShoppingCartItem as s where s.user.id = :userId and s.game.title = :gameTitle")
    ShoppingCartItem findByUserAndGame(Long userId, String gameTitle);

    @Query(value = "select new exercise_1.dtos.ShoppingCartItemDto(s.id, s.game.id) from ShoppingCartItem as s where s.user.id = :userId")
    List<ShoppingCartItemDto> findAllByUser(Long userId);

    void removeById(Long id);

    void removeAllByIdIn(Collection<Long> ids);
}
