package exercise_2.dtos;

public class PartRelationsDto {
    private final Long supplierId;

    public PartRelationsDto(Long supplierId) {
        this.supplierId = supplierId;
    }

    public Long getSupplierId() {
        return supplierId;
    }
}
