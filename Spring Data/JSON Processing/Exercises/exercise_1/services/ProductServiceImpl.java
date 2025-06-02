package exercise_1.services;

import exercise_1.dtos.ProductInputDto;
import exercise_1.dtos.ProductSummaryDto;
import exercise_1.models.Product;
import exercise_1.repositories.ProductRepository;
import org.modelmapper.ModelMapper;
import org.springframework.stereotype.Service;
import org.springframework.transaction.annotation.Transactional;

import java.math.BigDecimal;
import java.util.List;

@Service
public class ProductServiceImpl implements ProductService {
    private final ProductRepository repository;
    private final ModelMapper modelMapper;

    public ProductServiceImpl(ProductRepository repository, ModelMapper modelMapper) {
        this.repository = repository;
        this.modelMapper = modelMapper;
    }

    @Override
    public Product createProduct(ProductInputDto dto) {
        Product product = this.modelMapper.map(dto, Product.class);
        return this.repository.save(product);
    }

    @Transactional
    @Override
    public List<ProductSummaryDto> findActiveOffersInPriceRange(BigDecimal min, BigDecimal max) {
        return this.repository.getProductsSummary(min, max);
    }
}
