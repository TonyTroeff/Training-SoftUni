package softuni.exam.import_PersonalData;
//TestImportPersonalDataZeroTest000

import jakarta.xml.bind.JAXBException;
import org.junit.jupiter.api.Assertions;
import org.junit.jupiter.api.Test;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.boot.test.context.SpringBootTest;
import org.springframework.test.annotation.DirtiesContext;
import softuni.exam.service.impl.PersonalDataServiceImpl;

import java.io.IOException;

@SpringBootTest(webEnvironment = SpringBootTest.WebEnvironment.NONE)
@DirtiesContext(classMode = DirtiesContext.ClassMode.BEFORE_CLASS)
public class TestImportPersonalDataZeroTest000 {

    @Autowired
    private PersonalDataServiceImpl personalDataService;

    @Test
    void importAstronomersZeroTest() throws IOException, JAXBException {
        String expected = "Successfully imported personal data for visitor with card number 123456789\n" +
                "Invalid personal data\n" +
                "Successfully imported personal data for visitor with card number 987654321\n" +
                "Successfully imported personal data for visitor with card number 234567890\n" +
                "Successfully imported personal data for visitor with card number 345678901\n" +
                "Successfully imported personal data for visitor with card number 456789012\n" +
                "Successfully imported personal data for visitor with card number 567890123\n" +
                "Successfully imported personal data for visitor with card number 678901234\n" +
                "Successfully imported personal data for visitor with card number 789012345\n" +
                "Successfully imported personal data for visitor with card number 890123456\n" +
                "Successfully imported personal data for visitor with card number 901234567\n" +
                "Successfully imported personal data for visitor with card number 102345678\n" +
                "Successfully imported personal data for visitor with card number 213456789\n" +
                "Successfully imported personal data for visitor with card number 324567890\n" +
                "Successfully imported personal data for visitor with card number 435678901\n" +
                "Successfully imported personal data for visitor with card number 546789012\n" +
                "Successfully imported personal data for visitor with card number 657890123\n" +
                "Successfully imported personal data for visitor with card number 768901234\n" +
                "Successfully imported personal data for visitor with card number 879012345\n" +
                "Successfully imported personal data for visitor with card number 980123456\n" +
                "Successfully imported personal data for visitor with card number 123456780\n" +
                "Successfully imported personal data for visitor with card number 234567891\n" +
                "Successfully imported personal data for visitor with card number 345678902\n" +
                "Successfully imported personal data for visitor with card number 456789013\n" +
                "Successfully imported personal data for visitor with card number 567890124\n" +
                "Successfully imported personal data for visitor with card number 678901235\n" +
                "Successfully imported personal data for visitor with card number 789012346\n" +
                "Successfully imported personal data for visitor with card number 890123457\n" +
                "Successfully imported personal data for visitor with card number 901234568\n" +
                "Successfully imported personal data for visitor with card number 102345679\n" +
                "Successfully imported personal data for visitor with card number 213456780\n" +
                "Successfully imported personal data for visitor with card number 324567891\n" +
                "Successfully imported personal data for visitor with card number 435678902\n" +
                "Successfully imported personal data for visitor with card number 546789013\n" +
                "Successfully imported personal data for visitor with card number 657890124\n" +
                "Successfully imported personal data for visitor with card number 768901235\n" +
                "Successfully imported personal data for visitor with card number 879012346\n" +
                "Successfully imported personal data for visitor with card number 980123457\n" +
                "Successfully imported personal data for visitor with card number 123456791\n" +
                "Successfully imported personal data for visitor with card number 234567892\n" +
                "Successfully imported personal data for visitor with card number 345678903\n" +
                "Successfully imported personal data for visitor with card number 456789014\n" +
                "Successfully imported personal data for visitor with card number 567890125\n" +
                "Successfully imported personal data for visitor with card number 678901236\n" +
                "Successfully imported personal data for visitor with card number 789012347\n" +
                "Successfully imported personal data for visitor with card number 890123458\n" +
                "Successfully imported personal data for visitor with card number 901234569\n" +
                "Successfully imported personal data for visitor with card number 102345680\n" +
                "Successfully imported personal data for visitor with card number 213456791\n" +
                "Successfully imported personal data for visitor with card number 324567892\n" +
                "Successfully imported personal data for visitor with card number 435678903\n" +
                "Successfully imported personal data for visitor with card number 546789014\n" +
                "Successfully imported personal data for visitor with card number 657890125\n" +
                "Successfully imported personal data for visitor with card number 768901236\n" +
                "Successfully imported personal data for visitor with card number 879012347\n" +
                "Successfully imported personal data for visitor with card number 980123458\n" +
                "Successfully imported personal data for visitor with card number 123456792\n" +
                "Successfully imported personal data for visitor with card number 234567893\n" +
                "Successfully imported personal data for visitor with card number 345678904\n" +
                "Successfully imported personal data for visitor with card number 456789015\n" +
                "Successfully imported personal data for visitor with card number 567890126\n" +
                "Successfully imported personal data for visitor with card number 678901237\n" +
                "Successfully imported personal data for visitor with card number 789012348\n" +
                "Successfully imported personal data for visitor with card number 890123459\n" +
                "Successfully imported personal data for visitor with card number 901234570\n" +
                "Successfully imported personal data for visitor with card number 102345681\n" +
                "Successfully imported personal data for visitor with card number 213456792\n" +
                "Successfully imported personal data for visitor with card number 324567893\n" +
                "Successfully imported personal data for visitor with card number 435678904\n" +
                "Successfully imported personal data for visitor with card number 546789015\n" +
                "Successfully imported personal data for visitor with card number 657890126\n" +
                "Successfully imported personal data for visitor with card number 768901237\n" +
                "Successfully imported personal data for visitor with card number 879012348\n" +
                "Successfully imported personal data for visitor with card number 980123459\n" +
                "Successfully imported personal data for visitor with card number 123456793\n";

        String[] expectedSplit = expected.split("\\r\\n?|\\n");

        String actual = personalDataService.importPersonalData();
        String[] actualSplit = actual.split("\\r\\n?|\\n");


        Assertions.assertArrayEquals(expectedSplit, actualSplit);
    }
}

