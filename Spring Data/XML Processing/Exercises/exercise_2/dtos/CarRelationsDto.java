package exercise_2.dtos;

import java.util.Set;

public class CarRelationsDto {
    private final Set<Long> partIds;

    public CarRelationsDto(Set<Long> partIds) {
        this.partIds = partIds;
    }

    public Set<Long> getPartIds() {
        return partIds;
    }
}
