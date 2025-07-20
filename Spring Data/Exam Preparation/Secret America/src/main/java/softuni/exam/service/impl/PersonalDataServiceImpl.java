package softuni.exam.service.impl;

import jakarta.validation.Validator;
import jakarta.xml.bind.JAXBContext;
import jakarta.xml.bind.JAXBException;
import jakarta.xml.bind.Unmarshaller;
import org.modelmapper.ModelMapper;
import org.springframework.stereotype.Service;
import softuni.exam.models.dto.PersonalDataImportDto;
import softuni.exam.models.dto.PersonalDataInputDto;
import softuni.exam.models.entity.PersonalData;
import softuni.exam.repository.PersonalDataRepository;
import softuni.exam.service.PersonalDataService;

import java.io.IOException;
import java.io.StringReader;
import java.nio.file.Files;
import java.nio.file.Path;
import java.nio.file.Paths;

@Service
public class PersonalDataServiceImpl implements PersonalDataService {
    private final PersonalDataRepository repository;
    private final JAXBContext jaxbContext;
    private final Validator validator;
    private final ModelMapper modelMapper;

    public PersonalDataServiceImpl(PersonalDataRepository repository, JAXBContext jaxbContext, Validator validator, ModelMapper modelMapper) {
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
    public String readPersonalDataFileContent() throws IOException {
        Path path = Paths.get("src/main/resources/files/xml/personal_data.xml");
        return Files.readString(path);
    }

    @Override
    public String importPersonalData() throws JAXBException, IOException {
        Unmarshaller unmarshaller = this.jaxbContext.createUnmarshaller();

        PersonalDataImportDto importDto;
        try (StringReader reader = new StringReader(readPersonalDataFileContent())) {
            importDto = (PersonalDataImportDto) unmarshaller.unmarshal(reader);
        }

        StringBuilder sb = new StringBuilder();
        for (PersonalDataInputDto inputDto : importDto.getInput()) {
            PersonalData createdPersonalData = create(inputDto);

            if (createdPersonalData == null)
                sb.append(String.format("Invalid personal data%n"));
            else
                sb.append(String.format("Successfully imported personal data for visitor with card number %s%n", createdPersonalData.getCardNumber()));
        }

        return sb.toString();
    }

    @Override
    public PersonalData getReferenceById(Long id) {
        return repository.getReferenceById(id);
    }

    private PersonalData create(PersonalDataInputDto inputDto) {
        if (!validator.validate(inputDto).isEmpty()) return null;

        try {
            PersonalData personalData = modelMapper.map(inputDto, PersonalData.class);
            repository.save(personalData);

            return personalData;
        } catch (Exception e) {
            return null;
        }
    }
}
