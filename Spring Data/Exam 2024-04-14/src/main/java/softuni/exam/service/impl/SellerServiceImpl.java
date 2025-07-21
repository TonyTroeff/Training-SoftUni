package softuni.exam.service.impl;

import com.google.gson.Gson;
import org.modelmapper.ModelMapper;
import org.springframework.stereotype.Service;
import softuni.exam.models.dto.SellerInputDto;
import softuni.exam.models.entity.Seller;
import softuni.exam.repository.SellerRepository;
import softuni.exam.service.SellerService;
import softuni.exam.util.ValidationUtil;

import java.io.IOException;
import java.nio.file.Files;
import java.nio.file.Path;
import java.nio.file.Paths;

@Service
public class SellerServiceImpl implements SellerService {
    private final SellerRepository repository;
    private final Gson gson;
    private final ValidationUtil validationUtil;
    private final ModelMapper modelMapper;

    public SellerServiceImpl(SellerRepository repository, Gson gson, ValidationUtil validationUtil, ModelMapper modelMapper) {
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
    public String readSellersFromFile() throws IOException {
        Path path = Paths.get("src/main/resources/files/json/sellers.json");
        return Files.readString(path);
    }

    @Override
    public String importSellers() throws IOException {
        SellerInputDto[] inputDtos = gson.fromJson(readSellersFromFile(), SellerInputDto[].class);

        StringBuilder sb = new StringBuilder();
        for (SellerInputDto inputDto : inputDtos) {
            Seller createdSeller = create(inputDto);
            if (createdSeller == null) sb.append(String.format("Invalid seller%n"));
            else sb.append(String.format("Successfully imported seller %s %s%n", createdSeller.getFirstName(), createdSeller.getLastName()));
        }

        return sb.toString();
    }

    @Override
    public Seller getReferenceById(Long id) {
        return repository.getReferenceById(id);
    }

    private Seller create(SellerInputDto inputDto) {
        if (!validationUtil.isValid(inputDto)) return null;

        try {
            Seller seller = modelMapper.map(inputDto, Seller.class);
            repository.save(seller);

            return seller;
        } catch (Exception e) {
            return null;
        }
    }
}
