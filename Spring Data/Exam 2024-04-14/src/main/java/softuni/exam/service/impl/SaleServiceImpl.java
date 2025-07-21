package softuni.exam.service.impl;

import com.google.gson.Gson;
import org.modelmapper.ModelMapper;
import org.springframework.stereotype.Service;
import softuni.exam.models.dto.SaleInputDto;
import softuni.exam.models.entity.Sale;
import softuni.exam.models.entity.Seller;
import softuni.exam.repository.SaleRepository;
import softuni.exam.service.SaleService;
import softuni.exam.service.SellerService;
import softuni.exam.util.ValidationUtil;

import java.io.IOException;
import java.nio.file.Files;
import java.nio.file.Path;
import java.nio.file.Paths;
import java.time.LocalDateTime;
import java.time.format.DateTimeFormatter;

@Service
public class SaleServiceImpl implements SaleService {
    private final SellerService sellerService;
    private final SaleRepository repository;
    private final Gson gson;
    private final ValidationUtil validationUtil;
    private final ModelMapper modelMapper;

    public SaleServiceImpl(SellerService sellerService, SaleRepository repository, Gson gson, ValidationUtil validationUtil, ModelMapper modelMapper) {
        this.sellerService = sellerService;
        this.repository = repository;
        this.gson = gson;
        this.validationUtil = validationUtil;
        this.modelMapper = modelMapper;
    }

    @Override
    public boolean areImported() {
        return repository.count() > 0;
    }

    @Override
    public String readSalesFileContent() throws IOException {
        Path path = Paths.get("src/main/resources/files/json/sales.json");
        return Files.readString(path);
    }

    @Override
    public String importSales() throws IOException {
        SaleInputDto[] inputDtos = gson.fromJson(readSalesFileContent(), SaleInputDto[].class);

        StringBuilder sb = new StringBuilder();
        for (SaleInputDto inputDto : inputDtos) {
            Sale createdSale = create(inputDto);

            if (createdSale == null) sb.append(String.format("Invalid sale%n"));
            else sb.append(String.format("Successfully imported sale with number %s%n", createdSale.getNumber()));
        }

        return sb.toString();
    }

    @Override
    public Sale getReferenceById(Long id) {
        return this.repository.getReferenceById(id);
    }

    private Sale create(SaleInputDto inputDto) {
        if (!validationUtil.isValid(inputDto)) return null;

        try {
            Sale sale = modelMapper.map(inputDto, Sale.class);
            sale.setSaleDate(LocalDateTime.parse(inputDto.getSaleDate(), DateTimeFormatter.ofPattern("yyyy-MM-dd HH:mm:ss")));

            Long sellerId = inputDto.getSeller();
            if (sellerId != null) {
                Seller seller = sellerService.getReferenceById(sellerId);
                sale.setSeller(seller);
            }

            repository.save(sale);

            return sale;
        } catch (Exception e) {
            return null;
        }
    }
}
