package softuni.exam.import_Attractions;
//TestImportAttractionWithInvalidTypeLength003

import org.junit.jupiter.api.Assertions;
import org.junit.jupiter.api.Test;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.boot.test.context.SpringBootTest;
import org.springframework.test.annotation.DirtiesContext;
import org.springframework.test.context.jdbc.Sql;
import softuni.exam.service.impl.AttractionServiceImpl;

import java.io.File;
import java.io.FileWriter;
import java.io.IOException;
import java.nio.file.Files;
import java.nio.file.Path;

@SpringBootTest(webEnvironment = SpringBootTest.WebEnvironment.NONE)
@DirtiesContext(classMode = DirtiesContext.ClassMode.BEFORE_CLASS)
public class TestImportAttractionWithInvalidTypeLength003 {

    @Autowired
    private AttractionServiceImpl attractionService;

    @Test
    @Sql("/countries-test-imports.sql")
    void importAttractionsInvalidEntries003() throws IOException {

        copyRewriteFileForTest();

        String expected = "Invalid attraction\n" +
                "Successfully imported attraction Machu Picchu\n" +
                "Successfully imported attraction Christ the Redeemer\n";
        String[] expectedSplit = expected.split("\\r\\n?|\\n");
        String actual = attractionService.importAttractions();
        String[] actualSplit = actual.split("\\r\\n?|\\n");

        returnOriginalValue();
        Assertions.assertArrayEquals(expectedSplit, actualSplit);
    }

    private void copyRewriteFileForTest() throws IOException {
        File originalJsonFile = getOriginalFile();

        String testJSON = "[\n" +
                "  {\n" +
                "    \"name\": \"Machu Picchu\",\n" +
                "    \"description\": \"An ancient Incan city located in the Andes mountains\",\n" +
                "    \"type\": \"h\",\n" +
                "    \"elevation\": 2430,\n" +
                "    \"country\": 134\n" +
                "  },\n" +
                "  {\n" +
                "    \"name\": \"Machu Picchu\",\n" +
                "    \"description\": \"An ancient Incan city located in the Andes mountains\",\n" +
                "    \"type\": \"historical site\",\n" +
                "    \"elevation\": 2430,\n" +
                "    \"country\": 134\n" +
                "  },\n" +
                "  {\n" +
                "    \"name\": \"Christ the Redeemer\",\n" +
                "    \"description\": \"A massive statue of Jesus Christ overlooking Rio de Janeiro\",\n" +
                "    \"type\": \"monument\",\n" +
                "    \"elevation\": 710,\n" +
                "    \"country\": 24\n" +
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

    private File getOriginalFile() {
        return new File("src/main/resources/files/json/attractions.json");
    }

    private void returnOriginalValue() {

        try {
            FileWriter f2 = new FileWriter(getOriginalFile(), false);
            String testOriginalFile = Files.readString(Path.of("src/test/resources/original-files/attractions.json"));
            f2.write(testOriginalFile);
            f2.close();

        } catch (IOException e) {
            e.printStackTrace();
        }
    }
}
