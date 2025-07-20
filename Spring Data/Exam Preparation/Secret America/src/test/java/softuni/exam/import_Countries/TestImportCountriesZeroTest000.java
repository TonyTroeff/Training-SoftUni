package softuni.exam.import_Countries;
//TestImportDevicesZeroTest000

import jakarta.xml.bind.JAXBException;
import org.junit.jupiter.api.Assertions;
import org.junit.jupiter.api.Test;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.boot.test.context.SpringBootTest;
import org.springframework.test.annotation.DirtiesContext;
import softuni.exam.service.impl.CountryServiceImpl;

import java.io.IOException;

@SpringBootTest(webEnvironment = SpringBootTest.WebEnvironment.NONE)
@DirtiesContext(classMode = DirtiesContext.ClassMode.BEFORE_CLASS)
public class TestImportCountriesZeroTest000 {

    @Autowired
    private CountryServiceImpl countryService;

    @Test
    void importDevicesZeroTest() throws JAXBException, IOException {
            String expected =
        "Successfully imported country Afghanistan\n" +
        "Successfully imported country Albania\n" +
        "Invalid country\n" +
        "Successfully imported country Algeria\n" +
        "Invalid country\n" +
        "Successfully imported country Andorra\n" +
        "Successfully imported country Angola\n" +
        "Successfully imported country Antigua and Barbuda\n" +
        "Successfully imported country Argentina\n" +
        "Successfully imported country Armenia\n" +
        "Successfully imported country Australia\n" +
        "Successfully imported country Austria\n" +
        "Successfully imported country Azerbaijan\n" +
        "Successfully imported country Bahamas\n" +
        "Successfully imported country Bahrain\n" +
        "Successfully imported country Bangladesh\n" +
        "Successfully imported country Barbados\n" +
        "Successfully imported country Belarus\n" +
        "Successfully imported country Belgium\n" +
        "Successfully imported country Belize\n" +
        "Successfully imported country Benin\n" +
        "Successfully imported country Bhutan\n" +
        "Successfully imported country Bolivia\n" +
        "Successfully imported country Bosnia and Herzegovina\n" +
        "Successfully imported country Botswana\n" +
        "Successfully imported country Brazil\n" +
        "Successfully imported country Brunei\n" +
        "Successfully imported country Bulgaria\n" +
        "Successfully imported country Burkina Faso\n" +
        "Successfully imported country Burundi\n" +
        "Successfully imported country Cabo Verde\n" +
        "Successfully imported country Cambodia\n" +
        "Successfully imported country Cameroon\n" +
        "Successfully imported country Canada\n" +
        "Successfully imported country Central African Republic\n" +
        "Successfully imported country Chad\n" +
        "Successfully imported country Chile\n" +
        "Successfully imported country China\n" +
        "Successfully imported country Colombia\n" +
        "Successfully imported country Comoros\n" +
        "Successfully imported country Congo (Congo-Brazzaville)\n" +
        "Successfully imported country Congo, Democratic Republic of the\n" +
        "Successfully imported country Costa Rica\n" +
        "Successfully imported country Croatia\n" +
        "Successfully imported country Cuba\n" +
        "Successfully imported country Cyprus\n" +
        "Successfully imported country Czech Republic\n" +
        "Successfully imported country Denmark\n" +
        "Successfully imported country Djibouti\n" +
        "Successfully imported country Dominica\n" +
        "Successfully imported country Dominican Republic\n" +
        "Successfully imported country Ecuador\n" +
        "Successfully imported country Egypt\n" +
        "Successfully imported country El Salvador\n" +
        "Successfully imported country Equatorial Guinea\n" +
        "Successfully imported country Eritrea\n" +
        "Successfully imported country Estonia\n" +
        "Successfully imported country Eswatini\n" +
        "Successfully imported country Ethiopia\n" +
        "Successfully imported country Fiji\n" +
        "Successfully imported country Finland\n" +
        "Successfully imported country France\n" +
        "Successfully imported country Gabon\n" +
        "Successfully imported country Gambia\n" +
        "Successfully imported country Georgia\n" +
        "Successfully imported country Germany\n" +
        "Successfully imported country Ghana\n" +
        "Successfully imported country Greece\n" +
        "Successfully imported country Grenada\n" +
        "Successfully imported country Guatemala\n" +
        "Successfully imported country Guinea\n" +
        "Successfully imported country Guinea-Bissau\n" +
        "Successfully imported country Guyana\n" +
        "Successfully imported country Haiti\n" +
        "Successfully imported country Honduras\n" +
        "Successfully imported country Hungary\n" +
        "Successfully imported country Iceland\n" +
        "Successfully imported country India\n" +
        "Successfully imported country Indonesia\n" +
        "Successfully imported country Iran\n" +
        "Successfully imported country Iraq\n" +
        "Successfully imported country Ireland\n" +
        "Successfully imported country Israel\n" +
        "Successfully imported country Italy\n" +
        "Successfully imported country Jamaica\n" +
        "Successfully imported country Japan\n" +
        "Successfully imported country Jordan\n" +
        "Successfully imported country Kazakhstan\n" +
        "Successfully imported country Kenya\n" +
        "Successfully imported country Kiribati\n" +
        "Successfully imported country Kuwait\n" +
        "Successfully imported country Kyrgyzstan\n" +
        "Successfully imported country Laos\n" +
        "Successfully imported country Latvia\n" +
        "Successfully imported country Lebanon\n" +
        "Successfully imported country Lesotho\n" +
        "Successfully imported country Liberia\n" +
        "Successfully imported country Libya\n" +
        "Successfully imported country Liechtenstein\n" +
        "Successfully imported country Lithuania\n" +
        "Successfully imported country Luxembourg\n" +
        "Successfully imported country Madagascar\n" +
        "Successfully imported country Malawi\n" +
        "Successfully imported country Malaysia\n" +
        "Successfully imported country Maldives\n" +
        "Successfully imported country Mali\n" +
        "Successfully imported country Malta\n" +
        "Successfully imported country Marshall Islands\n" +
        "Successfully imported country Mauritania\n" +
        "Successfully imported country Mauritius\n" +
        "Successfully imported country Mexico\n" +
        "Successfully imported country Micronesia\n" +
        "Successfully imported country Moldova\n" +
        "Successfully imported country Monaco\n" +
        "Successfully imported country Mongolia\n" +
        "Successfully imported country Montenegro\n" +
        "Successfully imported country Morocco\n" +
        "Successfully imported country Mozambique\n" +
        "Successfully imported country Myanmar\n" +
        "Successfully imported country Namibia\n" +
        "Successfully imported country Nauru\n" +
        "Successfully imported country Nepal\n" +
        "Successfully imported country Netherlands\n" +
        "Successfully imported country New Zealand\n" +
        "Successfully imported country Nicaragua\n" +
        "Successfully imported country Niger\n" +
        "Successfully imported country Nigeria\n" +
        "Successfully imported country North Macedonia\n" +
        "Successfully imported country Norway\n" +
        "Successfully imported country Oman\n" +
        "Successfully imported country Pakistan\n" +
        "Successfully imported country Palau\n" +
        "Successfully imported country Panama\n" +
        "Successfully imported country Papua New Guinea\n" +
        "Successfully imported country Paraguay\n" +
        "Successfully imported country Peru\n" +
        "Successfully imported country Philippines\n" +
        "Successfully imported country Poland\n" +
        "Successfully imported country Portugal\n" +
        "Successfully imported country Puerto Rico\n" +
        "Successfully imported country Qatar\n" +
        "Successfully imported country Romania\n" +
        "Successfully imported country Russia\n" +
        "Successfully imported country Rwanda\n" +
        "Successfully imported country Saint Kitts and Nevis\n" +
        "Successfully imported country Saint Lucia\n" +
        "Successfully imported country Saint Vincent and the Grenadines\n" +
        "Successfully imported country Samoa\n" +
        "Successfully imported country San Marino\n" +
        "Successfully imported country Sao Tome and Principe\n" +
        "Successfully imported country Saudi Arabia\n" +
        "Successfully imported country Senegal\n" +
        "Successfully imported country Serbia\n" +
        "Successfully imported country Seychelles\n" +
        "Successfully imported country Sierra Leone\n" +
        "Successfully imported country Singapore\n" +
        "Successfully imported country Slovakia\n" +
        "Successfully imported country Slovenia\n" +
        "Successfully imported country Solomon Islands\n" +
        "Successfully imported country Somalia\n" +
        "Successfully imported country South Africa\n" +
        "Successfully imported country South Korea\n" +
        "Successfully imported country South Sudan\n" +
        "Successfully imported country Spain\n" +
        "Successfully imported country Sri Lanka\n" +
        "Successfully imported country Sudan\n" +
        "Successfully imported country Suriname\n" +
        "Successfully imported country Sweden\n" +
        "Successfully imported country Switzerland\n" +
        "Successfully imported country Syria\n" +
        "Successfully imported country Taiwan\n" +
        "Successfully imported country Tajikistan\n" +
        "Successfully imported country Tanzania\n" +
        "Successfully imported country Thailand\n" +
        "Successfully imported country Timor-Leste\n" +
        "Successfully imported country Togo\n" +
        "Successfully imported country Tonga\n" +
        "Successfully imported country Trinidad and Tobago\n" +
        "Successfully imported country Tunisia\n" +
        "Successfully imported country Turkey\n" +
        "Successfully imported country Turkmenistan\n" +
        "Successfully imported country Tuvalu\n" +
        "Successfully imported country Uganda\n" +
        "Successfully imported country Ukraine\n" +
        "Successfully imported country United Arab Emirates\n" +
        "Successfully imported country United Kingdom\n" +
        "Successfully imported country United States of America\n" +
        "Successfully imported country Uruguay\n" +
        "Successfully imported country Uzbekistan\n" +
        "Successfully imported country Vanuatu\n" +
        "Successfully imported country Vatican City\n" +
        "Successfully imported country Venezuela\n" +
        "Successfully imported country Vietnam\n" +
        "Successfully imported country Yemen\n" +
        "Successfully imported country Zambia\n" +
        "Successfully imported country Zimbabwe\n";


        String[] expectedSplit = expected.split("\\r\\n?|\\n");

        String actual = countryService.importCountries();
        String[] actualSplit = actual.split("\\r\\n?|\\n");



        Assertions.assertArrayEquals(expectedSplit, actualSplit);
    }
}

