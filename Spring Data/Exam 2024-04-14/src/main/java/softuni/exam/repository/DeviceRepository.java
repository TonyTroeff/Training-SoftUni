package softuni.exam.repository;

import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.data.jpa.repository.Query;
import org.springframework.stereotype.Repository;
import softuni.exam.models.entity.Device;
import softuni.exam.models.enums.DeviceType;

import java.util.List;

@Repository
public interface DeviceRepository extends JpaRepository<Device, Long> {
    @Query("select d from Device as d where d.type = :type and d.price < :maxPriceExclusive and d.storage >= :minStorageInclusive order by lower(d.brand) asc")
    List<Device> findExportable(DeviceType type, Double maxPriceExclusive, Integer minStorageInclusive);
}
