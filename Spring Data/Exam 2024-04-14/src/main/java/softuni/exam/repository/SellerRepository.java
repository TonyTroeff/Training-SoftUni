package softuni.exam.repository;

import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.stereotype.Repository;
import softuni.exam.models.entity.Seller;

@Repository
public interface SellerRepository extends JpaRepository<Seller, Long> {
}
