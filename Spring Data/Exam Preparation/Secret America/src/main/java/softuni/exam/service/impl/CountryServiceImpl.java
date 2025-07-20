package softuni.exam.service.impl;

import com.google.gson.Gson;
import jakarta.validation.Validator;
import org.modelmapper.ModelMapper;
import org.springframework.stereotype.Service;
import softuni.exam.models.dto.CountryInputDto;
import softuni.exam.models.entity.Country;
import softuni.exam.repository.CountryRepository;
import softuni.exam.service.CountryService;

import java.io.IOException;
import java.nio.file.Files;
import java.nio.file.Path;
import java.nio.file.Paths;

@Service
public class CountryServiceImpl implements CountryService {
    private final CountryRepository repository;
    private final Validator validator;
    private final Gson gson;
    private final ModelMapper modelMapper;

    public CountryServiceImpl(CountryRepository repository, Validator validator, Gson gson, ModelMapper modelMapper) {
        this.repository = repository;
        this.validator = validator;
        this.gson = gson;
        this.modelMapper = modelMapper;
    }

    @Override
    public boolean areImported() {
        return repository.count() > 0;
    }

    @Override
    public String readCountryFileContent() throws IOException {
        Path path = Paths.get("src/main/resources/files/json/countries.json");
        return Files.readString(path);
    }

    @Override
    public String importCountries() throws IOException {
        CountryInputDto[] inputDtos = gson.fromJson(readCountryFileContent(), CountryInputDto[].class);

        StringBuilder sb = new StringBuilder();
        for (CountryInputDto inputDto : inputDtos) {
            Country createdCountry = create(inputDto);

            if (createdCountry == null)
                sb.append(String.format("Invalid country%n"));
            else
                sb.append(String.format("Successfully imported country %s%n", createdCountry.getName()));
        }

        return sb.toString();
    }

    @Override
    public Country getReferenceById(Long id) {
        return repository.getReferenceById(id);
    }

    private Country create(CountryInputDto inputDto) {
        if (!validator.validate(inputDto).isEmpty()) return null;

        try {
            Country country = modelMapper.map(inputDto, Country.class);
            repository.save(country);

            return country;
        } catch (Exception e) {
            return null;
        }
    }
}
