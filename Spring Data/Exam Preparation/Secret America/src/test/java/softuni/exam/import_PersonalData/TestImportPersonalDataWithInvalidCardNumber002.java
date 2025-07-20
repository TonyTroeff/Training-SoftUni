package softuni.exam.import_PersonalData;
//TestImportPersonalDataWithInvalidCardNumber002

import jakarta.xml.bind.JAXBException;
import org.junit.jupiter.api.Assertions;
import org.junit.jupiter.api.Test;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.boot.test.context.SpringBootTest;
import org.springframework.test.annotation.DirtiesContext;
import softuni.exam.service.impl.PersonalDataServiceImpl;

import java.io.File;
import java.io.FileWriter;
import java.io.IOException;
import java.nio.file.Files;
import java.nio.file.Path;

@SpringBootTest(webEnvironment = SpringBootTest.WebEnvironment.NONE)
@DirtiesContext(classMode = DirtiesContext.ClassMode.BEFORE_CLASS)
public class TestImportPersonalDataWithInvalidCardNumber002 {

    @Autowired
    private PersonalDataServiceImpl personalDataService;

    @Test
    void importPersonalDataWithInvalidCardNumber() throws IOException, JAXBException, NoSuchFieldException, IllegalAccessException {

        rewriteFileForTest();
        String actual = personalDataService.importPersonalData();
        String[] actualSplit = actual.split("\\r\\n?|\\n");
        String expected = """
                Successfully imported personal data for visitor with card number 123456789
                Invalid personal data""";
        String[] expectedSplit = expected.split("\\r\\n?|\\n");

        returnOriginalValue();
        Assertions.assertArrayEquals(expectedSplit, actualSplit);
    }

    private void rewriteFileForTest() {
        File originalJsonFile = getOriginalFile();

        String testXML = """
                <?xml version='1.0' encoding='UTF-8'?>
                                <personal_datas>
                                    <personal_data>
                                        <age>33</age>
                                        <gender>M</gender>
                                        <birth_date>1991-05-12</birth_date>
                                        <card_number>123456789</card_number>
                                    </personal_data>
                                    <personal_data>
                                        <age>37</age>
                                        <gender>M</gender>
                                        <birth_date>1987-04-04</birth_date>
                                        <card_number>123456789</card_number>
                                    </personal_data>
                                </personal_datas>""";

        try {
            FileWriter f2 = new FileWriter(originalJsonFile, false);
            f2.write(testXML);
            f2.close();

        } catch (IOException e) {
            e.printStackTrace();
        }
    }

    private File getOriginalFile() {
        return new File("src/main/resources/files/xml/personal_data.xml");
    }

    private void returnOriginalValue() {

        try {
            FileWriter f2 = new FileWriter(getOriginalFile(), false);
            String testOriginalFile = Files.readString(Path.of("src/test/resources/original-files/personal_data.xml"));
            f2.write(testOriginalFile);
            f2.close();

        } catch (IOException e) {
            e.printStackTrace();
        }
    }
}
