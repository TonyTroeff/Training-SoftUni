package exercise_1.services;

import exercise_1.dtos.ShoppingCartItemDto;
import exercise_1.models.ShoppingCartItem;
import jakarta.validation.constraints.NotNull;
import org.springframework.validation.annotation.Validated;

import java.util.List;

@Validated
public interface ShoppingCartService {
    ShoppingCartItem getShoppingCartItem(@NotNull Long userId, @NotNull String gameTitle);
    List<ShoppingCartItemDto> getShoppingCartItemsByUser(@NotNull Long userId);

    void createShoppingCartItem(@NotNull Long userId, @NotNull String gameTitle);

    void deleteShoppingCartItem(@NotNull Long id);
    void deleteShoppingCartItems(@NotNull List<Long> ids);
}
