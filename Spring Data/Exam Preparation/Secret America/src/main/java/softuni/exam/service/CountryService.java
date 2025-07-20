package softuni.exam.service;

import softuni.exam.models.entity.Country;

import java.io.IOException;

public interface CountryService {
    boolean areImported();

    String readCountryFileContent() throws IOException;

    String importCountries() throws IOException;

    Country getReferenceById(Long id);
}
