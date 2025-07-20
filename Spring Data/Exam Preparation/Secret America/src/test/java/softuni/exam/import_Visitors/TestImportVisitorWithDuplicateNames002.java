package softuni.exam.import_Visitors;
//TestImportVisitorWithDuplicateNames002

import jakarta.xml.bind.JAXBException;
import org.junit.jupiter.api.Assertions;
import org.junit.jupiter.api.Test;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.boot.test.context.SpringBootTest;
import org.springframework.test.annotation.DirtiesContext;
import org.springframework.test.context.jdbc.Sql;
import softuni.exam.service.impl.VisitorServiceImpl;

import java.io.File;
import java.io.FileWriter;
import java.io.IOException;
import java.nio.file.Files;
import java.nio.file.Path;

@SpringBootTest(webEnvironment = SpringBootTest.WebEnvironment.NONE)
@DirtiesContext(classMode = DirtiesContext.ClassMode.BEFORE_CLASS)
public class TestImportVisitorWithDuplicateNames002 {

    @Autowired
    private VisitorServiceImpl visitorService;

    @Sql({"/countries-test-imports.sql", "/attractions-test-imports.sql", "/personal_data-test-imports.sql"})
    @Test
    void importVisitorWithDuplicateName() throws IOException, JAXBException, NoSuchFieldException, IllegalAccessException {

        rewriteFileForTest();
        String actual = visitorService.importVisitors();
        String[] actualSplit = actual.split("\\r\\n?|\\n");
        String expected = """
                Successfully imported visitor James Williams
                Invalid visitor""";
        String[] expectedSplit = expected.split("\\r\\n?|\\n");

        returnOriginalValue();
        Assertions.assertArrayEquals(expectedSplit, actualSplit);
    }

    private void rewriteFileForTest() {
        File originalJsonFile = getOriginalFile();

        String testXML = """
                <?xml version='1.0' encoding='UTF-8'?>
                <visitors>
                    <visitor>
                       <first_name>James</first_name>
                       <last_name>Williams</last_name>
                       <attraction_id>29</attraction_id>
                       <country_id>87</country_id>
                       <personal_data_id>3</personal_data_id>
                   </visitor>
                   <visitor>
                       <first_name>James</first_name>
                       <last_name>Williams</last_name>
                       <attraction_id>30</attraction_id>
                       <country_id>188</country_id>
                       <personal_data_id>69</personal_data_id>
                   </visitor>
                </visitors>""";

        try {
            FileWriter f2 = new FileWriter(originalJsonFile, false);
            f2.write(testXML);
            f2.close();

        } catch (IOException e) {
            e.printStackTrace();
        }
    }

    private File getOriginalFile() {
        return new File("src/main/resources/files/xml/visitors.xml");
    }

    private void returnOriginalValue() {

        try {
            FileWriter f2 = new FileWriter(getOriginalFile(), false);
            String testOriginalFile = Files.readString(Path.of("src/test/resources/original-files/visitors.xml"));
            f2.write(testOriginalFile);
            f2.close();

        } catch (IOException e) {
            e.printStackTrace();
        }
    }
}
