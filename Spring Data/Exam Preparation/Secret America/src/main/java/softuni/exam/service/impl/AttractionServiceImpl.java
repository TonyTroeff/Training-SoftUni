package softuni.exam.service.impl;

import com.google.gson.Gson;
import jakarta.validation.Validator;
import org.modelmapper.ModelMapper;
import org.springframework.stereotype.Service;
import softuni.exam.models.dto.AttractionExportDto;
import softuni.exam.models.dto.AttractionInputDto;
import softuni.exam.models.entity.Attraction;
import softuni.exam.models.entity.Country;
import softuni.exam.repository.AttractionRepository;
import softuni.exam.service.AttractionService;
import softuni.exam.service.CountryService;

import java.io.IOException;
import java.nio.file.Files;
import java.nio.file.Path;
import java.nio.file.Paths;
import java.util.List;

@Service
public class AttractionServiceImpl implements AttractionService {
    private final CountryService countryService;
    private final AttractionRepository repository;
    private final Validator validator;
    private final Gson gson;
    private final ModelMapper modelMapper;

    public AttractionServiceImpl(CountryService countryService, AttractionRepository repository, Validator validator, Gson gson, ModelMapper modelMapper) {
        this.countryService = countryService;
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
    public String readAttractionsFileContent() throws IOException {
        Path path = Paths.get("src/main/resources/files/json/attractions.json");
        return Files.readString(path);
    }

    @Override
    public String importAttractions() throws IOException {
        AttractionInputDto[] inputDtos = gson.fromJson(readAttractionsFileContent(), AttractionInputDto[].class);

        StringBuilder sb = new StringBuilder();
        for (AttractionInputDto inputDto : inputDtos) {
            Attraction createdAttraction = create(inputDto);

            if (createdAttraction == null)
                sb.append(String.format("Invalid attraction%n"));
            else
                sb.append(String.format("Successfully imported attraction %s%n", createdAttraction.getName()));
        }

        return sb.toString();
    }

    @Override
    public String exportAttractions() {
        List<AttractionExportDto> exportable = repository.findExportable(List.of("historical site", "archeological site"), 300);

        StringBuilder sb = new StringBuilder();
        for (AttractionExportDto attractionExportDto : exportable)
            sb.append(String.format("Attraction with ID%d:%n***%s - %s at an altitude of %dm. somewhere in %s.%n", attractionExportDto.getId(), attractionExportDto.getName(), attractionExportDto.getDescription(), attractionExportDto.getElevation(), attractionExportDto.getCountryName()));

        return sb.toString();
    }

    @Override
    public Attraction getReferenceById(Long id) {
        return repository.getReferenceById(id);
    }

    private Attraction create(AttractionInputDto inputDto) {
        if (!validator.validate(inputDto).isEmpty()) return null;

        try {
            Attraction attraction = modelMapper.map(inputDto, Attraction.class);

            Country country = countryService.getReferenceById(inputDto.getCountry());
            attraction.setCountry(country);

            repository.save(attraction);

            return attraction;
        } catch (Exception e) {
            return null;
        }
    }
}
