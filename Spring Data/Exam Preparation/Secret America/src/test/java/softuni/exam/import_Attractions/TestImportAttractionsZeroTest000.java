package softuni.exam.import_Attractions;
//TestImportAttractionsZeroTest000

import jakarta.xml.bind.JAXBException;
import org.junit.jupiter.api.Assertions;
import org.junit.jupiter.api.Test;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.boot.test.context.SpringBootTest;
import org.springframework.test.annotation.DirtiesContext;
import org.springframework.test.context.jdbc.Sql;
import softuni.exam.service.impl.AttractionServiceImpl;

import java.io.IOException;

@SpringBootTest(webEnvironment = SpringBootTest.WebEnvironment.NONE)
@DirtiesContext(classMode = DirtiesContext.ClassMode.BEFORE_CLASS)
public class TestImportAttractionsZeroTest000 {

    @Autowired
    private AttractionServiceImpl attractionService;
    @Sql("/countries-test-imports.sql")
//    @Sql({"/countries-test-imports.sql", "/attractions-test-imports.sql"})
    @Test
    void importDevicesZeroTest() throws JAXBException, IOException {
        String expected =
                "Successfully imported attraction Machu Picchu\n" +
                        "Invalid attraction\n" +
                        "Successfully imported attraction Christ the Redeemer\n" +
                        "Successfully imported attraction Iguazu Falls\n" +
                        "Successfully imported attraction Salar de Uyuni\n" +
                        "Successfully imported attraction Galapagos Islands\n" +
                        "Successfully imported attraction Atacama Desert\n" +
                        "Successfully imported attraction Angel Falls\n" +
                        "Successfully imported attraction Torres del Paine\n" +
                        "Successfully imported attraction La Sagrada Familia\n" +
                        "Successfully imported attraction Lake Titicaca\n" +
                        "Successfully imported attraction Valley of the Moon\n" +
                        "Successfully imported attraction Ciudad Perdida\n" +
                        "Successfully imported attraction Uyuni Train Cemetery\n" +
                        "Successfully imported attraction Canoa Quebrada\n" +
                        "Successfully imported attraction Perito Moreno Glacier\n" +
                        "Successfully imported attraction Teotihuacan\n" +
                        "Successfully imported attraction Tikal\n" +
                        "Successfully imported attraction Cotopaxi\n" +
                        "Successfully imported attraction Copacabana Beach\n" +
                        "Successfully imported attraction Rapa Nui\n" +
                        "Successfully imported attraction Itaipu Dam\n" +
                        "Successfully imported attraction Aconcagua\n" +
                        "Successfully imported attraction Nazca Lines\n" +
                        "Successfully imported attraction Caracol\n" +
                        "Successfully imported attraction San Blas Islands\n" +
                        "Successfully imported attraction Monteverde Cloud Forest\n" +
                        "Successfully imported attraction Cayos Cochinos\n" +
                        "Successfully imported attraction Lake Atitlan\n" +
                        "Successfully imported attraction Salt Cathedral of Zipaquira\n" +
                        "Successfully imported attraction El Yunque National Forest\n" +
                        "Successfully imported attraction Tarapoto\n" +
                        "Successfully imported attraction La Guaira\n" +
                        "Successfully imported attraction Ruins of Copan\n" +
                        "Successfully imported attraction The Pink Sea\n" +
                        "Successfully imported attraction Poas Volcano\n" +
                        "Successfully imported attraction Mount Roraima\n" +
                        "Successfully imported attraction La Mano de Punta del Este\n" +
                        "Successfully imported attraction Los Glaciares National Park\n" +
                        "Successfully imported attraction Los Roques Archipelago\n" +
                        "Successfully imported attraction Caral\n" +
                        "Successfully imported attraction San Rafael Waterfall\n" +
                        "Successfully imported attraction Fernando de Noronha\n" +
                        "Successfully imported attraction Cerro de los Siete Colores\n" +
                        "Successfully imported attraction Chimborazo\n" +
                        "Successfully imported attraction Rio Secreto\n" +
                        "Successfully imported attraction Marieta Islands\n" +
                        "Successfully imported attraction Pantanal\n" +
                        "Successfully imported attraction Punta Tombo\n" +
                        "Successfully imported attraction Laguna Colorada\n" +
                        "Successfully imported attraction Salt Flats of Maras\n";


        String[] expectedSplit = expected.split("\\r\\n?|\\n");

        String actual = attractionService.importAttractions();
        String[] actualSplit = actual.split("\\r\\n?|\\n");


        Assertions.assertArrayEquals(expectedSplit, actualSplit);
    }
}

