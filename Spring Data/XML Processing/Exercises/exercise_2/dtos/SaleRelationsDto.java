package exercise_2.dtos;

public class SaleRelationsDto {
    private final Long carId;
    private final Long customerId;

    public SaleRelationsDto(Long carId, Long customerId) {
        this.carId = carId;
        this.customerId = customerId;
    }

    public Long getCarId() {
        return carId;
    }

    public Long getCustomerId() {
        return customerId;
    }
}
