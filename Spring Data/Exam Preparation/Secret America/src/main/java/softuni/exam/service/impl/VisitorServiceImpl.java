package softuni.exam.service.impl;

import jakarta.validation.Validator;
import jakarta.xml.bind.JAXBContext;
import jakarta.xml.bind.JAXBException;
import jakarta.xml.bind.Unmarshaller;
import org.modelmapper.ModelMapper;
import org.springframework.stereotype.Service;
import softuni.exam.models.dto.VisitorImportDto;
import softuni.exam.models.dto.VisitorInputDto;
import softuni.exam.models.entity.Attraction;
import softuni.exam.models.entity.Country;
import softuni.exam.models.entity.PersonalData;
import softuni.exam.models.entity.Visitor;
import softuni.exam.repository.VisitorRepository;
import softuni.exam.service.AttractionService;
import softuni.exam.service.CountryService;
import softuni.exam.service.PersonalDataService;
import softuni.exam.service.VisitorService;

import java.io.IOException;
import java.io.StringReader;
import java.nio.file.Files;
import java.nio.file.Path;
import java.nio.file.Paths;

@Service
public class VisitorServiceImpl implements VisitorService {
    private final AttractionService attractionService;
    private final CountryService countryService;
    private final PersonalDataService personalDataService;
    private final VisitorRepository repository;
    private final JAXBContext jaxbContext;
    private final Validator validator;
    private final ModelMapper modelMapper;

    public VisitorServiceImpl(AttractionService attractionService, CountryService countryService, PersonalDataService personalDataService, VisitorRepository repository, JAXBContext jaxbContext, Validator validator, ModelMapper modelMapper) {
        this.attractionService = attractionService;
        this.countryService = countryService;
        this.personalDataService = personalDataService;
        this.repository = repository;
        this.jaxbContext = jaxbContext;
        this.validator = validator;
        this.modelMapper = modelMapper;
    }

    @Override
    public boolean areImported() {
        return repository.count() > 0;
    }

    @Override
    public String readVisitorsFileContent() throws IOException {
        Path path = Paths.get("src/main/resources/files/xml/visitors.xml");
        return Files.readString(path);
    }

    @Override
    public String importVisitors() throws JAXBException, IOException {
        Unmarshaller unmarshaller = this.jaxbContext.createUnmarshaller();

        VisitorImportDto importDto;
        try (StringReader reader = new StringReader(readVisitorsFileContent())) {
            importDto = (VisitorImportDto) unmarshaller.unmarshal(reader);
        }

        StringBuilder sb = new StringBuilder();
        for (VisitorInputDto inputDto : importDto.getInput()) {
            Visitor createdVisitor = create(inputDto);

            if (createdVisitor == null)
                sb.append(String.format("Invalid visitor%n"));
            else
                sb.append(String.format("Successfully imported visitor %s %s%n", createdVisitor.getFirstName(), createdVisitor.getLastName()));
        }

        return sb.toString();
    }

    private Visitor create(VisitorInputDto inputDto) {
        if (!validator.validate(inputDto).isEmpty()) return null;

        try {
            Visitor visitor = modelMapper.map(inputDto, Visitor.class);

            Attraction attraction = attractionService.getReferenceById(inputDto.getAttraction());
            Country country = countryService.getReferenceById(inputDto.getCountry());
            PersonalData personalData = personalDataService.getReferenceById(inputDto.getPersonalData());

            visitor.setAttraction(attraction);
            visitor.setCountry(country);
            visitor.setPersonalData(personalData);

            repository.save(visitor);

            return visitor;
        } catch (Exception e) {
            return null;
        }
    }
}
