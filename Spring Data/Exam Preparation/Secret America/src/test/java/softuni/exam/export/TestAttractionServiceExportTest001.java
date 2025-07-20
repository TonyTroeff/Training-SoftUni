package softuni.exam.export;
//TestAttractionServiceExportTest001

import org.junit.jupiter.api.Assertions;
import org.junit.jupiter.api.Test;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.boot.test.context.SpringBootTest;
import org.springframework.test.annotation.DirtiesContext;
import org.springframework.test.context.jdbc.Sql;
import softuni.exam.service.impl.AttractionServiceImpl;

import java.io.IOException;

@SpringBootTest
@DirtiesContext(classMode = DirtiesContext.ClassMode.BEFORE_CLASS)
public class TestAttractionServiceExportTest001 {

    @Autowired
    private AttractionServiceImpl attractionService;

    @Sql("/export-test.sql")
    @Test
    void exportDevices() throws IOException {
        String actual = attractionService.exportAttractions();

        String expected =
                """
                 Attraction with ID24:
                 ***Caracol - An ancient Mayan city located in the jungles at an altitude of 500m. somewhere in Belize.
                 Attraction with ID40:
                 ***Caral - One of the oldest civilizations in the Americas, an ancient city at an altitude of 350m. somewhere in Peru.
                 Attraction with ID12:
                 ***Ciudad Perdida - An ancient city in the mountains at an altitude of 1200m. somewhere in Colombia.
                 Attraction with ID1:
                 ***Machu Picchu - An ancient Incan city located in the Andes mountains at an altitude of 2430m. somewhere in Peru.
                 Attraction with ID23:
                 ***Nazca Lines - Ancient geoglyphs etched into the southern desert at an altitude of 520m. somewhere in Peru.
                 Attraction with ID33:
                 ***Ruins of Copan - An ancient Mayan site known for its stunning sculptures at an altitude of 600m. somewhere in Honduras.
                 Attraction with ID50:
                 ***Salt Flats of Maras - A complex of salt evaporation ponds dating back to Incan times at an altitude of 3000m. somewhere in Peru.
                 Attraction with ID16:
                 ***Teotihuacan - An ancient Mesoamerican city with large pyramids at an altitude of 2300m. somewhere in Mexico.
                 Attraction with ID17:
                 ***Tikal - A large Mayan archaeological site at an altitude of 300m. somewhere in Guatemala.""";

        String[] actualSplit = actual.split("\\r\\n?|\\n");
        String[] expectedSplit = expected.split("\\r\\n?|\\n");

        Assertions.assertArrayEquals(expectedSplit,actualSplit);
    }
}
