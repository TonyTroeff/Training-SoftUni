package softuni.exam.import_Countries;
//TestImportCountriesWithInvalidValue002

import org.junit.jupiter.api.Assertions;
import org.junit.jupiter.api.Test;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.boot.test.context.SpringBootTest;
import org.springframework.test.annotation.DirtiesContext;
import softuni.exam.service.impl.CountryServiceImpl;

import java.io.File;
import java.io.FileWriter;
import java.io.IOException;
import java.nio.file.Files;
import java.nio.file.Path;

@SpringBootTest(webEnvironment = SpringBootTest.WebEnvironment.NONE)
@DirtiesContext(classMode = DirtiesContext.ClassMode.BEFORE_CLASS)

public class TestImportCountriesWithInvalidNameLength002 {

    @Autowired
    private CountryServiceImpl countryService;


    @Test
    void importCountryInvalidEntries002() throws IOException {
        String expected = "Invalid country\n" +
                "Successfully imported country Albania\n" +
                "Successfully imported country Algeria\n" +
                "Invalid country\n";

        String[] expectedSplit = expected.split("\\r\\n?|\\n");

        copyRewriteFileForTest();
        String actual = countryService.importCountries();
        String[] actualSplit = actual.split("\\r\\n?|\\n");

        returnOriginalValue();
        Assertions.assertArrayEquals(expectedSplit,actualSplit);
    }

    private void copyRewriteFileForTest() throws IOException {
        File originalJsonFile = new File("src/main/resources/files/json/countries.json");

        String testJSON = "[\n" +
                "  {\n" +
                "    \"name\": \"Afghanistaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaan\",\n" +
                "    \"area\": \"652230\"\n" +
                "  },\n" +
                "  {\n" +
                "    \"name\": \"Albania\",\n" +
                "    \"area\": \"28748\"\n" +
                "  },\n" +
                "  {\n" +
                "    \"name\": \"Algeria\",\n" +
                "    \"area\": \"2381741\"\n" +
                "  },\n" +
                "  {\n" +
                "    \"name\": \"A\",\n" +
                "    \"area\": \"2381741\"\n" +
                "  }\n" +
                "]";


        try {
            FileWriter f2 = new FileWriter(originalJsonFile, false);
            f2.write(testJSON);
            f2.close();

        } catch (IOException e) {
            e.printStackTrace();
        }
    }

    private void returnOriginalValue() {
        File originalJsonFileSrc = new File("src/main/resources/files/json/countries.json");

        try {
            FileWriter f2 = new FileWriter(originalJsonFileSrc, false);
            String testOriginalFile = Files.readString(Path.of("src/test/resources/original-files/countries.json"));
            f2.write(testOriginalFile);
            f2.close();

        } catch (IOException e) {
            e.printStackTrace();
        }
    }
}
