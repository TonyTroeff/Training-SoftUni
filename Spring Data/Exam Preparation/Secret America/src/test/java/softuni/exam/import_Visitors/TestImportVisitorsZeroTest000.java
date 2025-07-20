package softuni.exam.import_Visitors;
//TestImportPersonalDataZeroTest000

import jakarta.xml.bind.JAXBException;
import org.junit.jupiter.api.Assertions;
import org.junit.jupiter.api.Test;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.boot.test.context.SpringBootTest;
import org.springframework.test.annotation.DirtiesContext;
import org.springframework.test.context.jdbc.Sql;
import softuni.exam.service.impl.VisitorServiceImpl;

import java.io.IOException;

@SpringBootTest(webEnvironment = SpringBootTest.WebEnvironment.NONE)
@DirtiesContext(classMode = DirtiesContext.ClassMode.BEFORE_CLASS)
public class TestImportVisitorsZeroTest000 {

    @Autowired
    private VisitorServiceImpl visitorService;

    @Sql({"/countries-test-imports.sql", "/attractions-test-imports.sql", "/personal_data-test-imports.sql"})
    @Test
    void importAstronomersVisitorsTest() throws IOException, JAXBException {
        String expected =
                "Successfully imported visitor John Smith\n" +
                        "Invalid visitor\n" +
                        "Successfully imported visitor Emma Johnson\n" +
                        "Successfully imported visitor James Williams\n" +
                        "Invalid visitor\n" +
                        "Successfully imported visitor Sophia Brown\n" +
                        "Successfully imported visitor Michael Jones\n" +
                        "Successfully imported visitor Isabella Garcia\n" +
                        "Successfully imported visitor William Miller\n" +
                        "Successfully imported visitor Olivia Davis\n" +
                        "Successfully imported visitor David Martinez\n" +
                        "Successfully imported visitor Emily Lopez\n" +
                        "Successfully imported visitor Christopher Rodriguez\n" +
                        "Successfully imported visitor Abigail Hernandez\n" +
                        "Successfully imported visitor Matthew Wilson\n" +
                        "Successfully imported visitor Madison Gonzalez\n" +
                        "Successfully imported visitor Daniel Anderson\n" +
                        "Successfully imported visitor Ava Thomas\n" +
                        "Successfully imported visitor Anthony Taylor\n" +
                        "Successfully imported visitor Mia Moore\n" +
                        "Successfully imported visitor Joshua Jackson\n" +
                        "Successfully imported visitor Charlotte Martin\n" +
                        "Successfully imported visitor Andrew Lee\n" +
                        "Successfully imported visitor Amelia Perez\n" +
                        "Successfully imported visitor Joseph Thompson\n" +
                        "Successfully imported visitor Evelyn White\n" +
                        "Successfully imported visitor Samuel Harris\n" +
                        "Successfully imported visitor Sofia Sanchez\n" +
                        "Successfully imported visitor Benjamin Clark\n" +
                        "Successfully imported visitor Grace Ramirez\n" +
                        "Successfully imported visitor Alexander Lewis\n" +
                        "Successfully imported visitor Elizabeth Robinson\n" +
                        "Successfully imported visitor Logan Walker\n" +
                        "Successfully imported visitor Lily Young\n" +
                        "Successfully imported visitor Henry King\n" +
                        "Successfully imported visitor Ella Scott\n" +
                        "Successfully imported visitor Ryan Green\n" +
                        "Successfully imported visitor Victoria Baker\n" +
                        "Successfully imported visitor Elijah Adams\n" +
                        "Successfully imported visitor Scarlett Nelson\n" +
                        "Successfully imported visitor Gabriel Hill\n" +
                        "Successfully imported visitor Zoe Carter\n" +
                        "Successfully imported visitor Jackson Mitchell\n" +
                        "Successfully imported visitor Penelope Perez\n" +
                        "Successfully imported visitor Lucas Roberts\n" +
                        "Successfully imported visitor Chloe Turner\n" +
                        "Successfully imported visitor Jack Phillips\n" +
                        "Successfully imported visitor Harper Campbell\n" +
                        "Successfully imported visitor Owen Parker\n" +
                        "Successfully imported visitor Lillian Evans\n" +
                        "Successfully imported visitor Thomas Edwards\n" +
                        "Successfully imported visitor Hannah Collins\n";

        String[] expectedSplit = expected.split("\\r\\n?|\\n");

        String actual = visitorService.importVisitors();
        String[] actualSplit = actual.split("\\r\\n?|\\n");


        Assertions.assertArrayEquals(expectedSplit, actualSplit);
    }
}

