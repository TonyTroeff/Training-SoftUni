package exercise_1.services;

import exercise_1.dtos.OrderDto;
import jakarta.validation.constraints.NotNull;
import org.springframework.validation.annotation.Validated;

import java.util.List;

@Validated
public interface OrderService {
    void finishPurchase(@NotNull Long userId);

    List<OrderDto> getOrdersByUser(@NotNull Long userId);
}
