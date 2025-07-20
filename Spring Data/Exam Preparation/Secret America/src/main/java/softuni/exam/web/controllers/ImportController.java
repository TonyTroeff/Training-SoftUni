package softuni.exam.web.controllers;

import org.springframework.stereotype.Controller;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.PostMapping;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.servlet.ModelAndView;
import softuni.exam.service.AttractionService;
import softuni.exam.service.CountryService;
import softuni.exam.service.PersonalDataService;
import softuni.exam.service.VisitorService;

import java.io.IOException;
import jakarta.xml.bind.JAXBException;



@Controller
@RequestMapping("/import")
public class ImportController extends BaseController {

    private final AttractionService attractionService;
    private final CountryService countryService;
    private final PersonalDataService personalDataService;
    private final VisitorService visitorService;


    public ImportController(AttractionService attractionService, CountryService countryService, PersonalDataService personalDataService, VisitorService visitorService) {
        this.attractionService = attractionService;
        this.countryService = countryService;
        this.personalDataService = personalDataService;
        this.visitorService = visitorService;
    }

    @GetMapping("/json")
    public ModelAndView importJson() {

        boolean[] areImported = new boolean[]{
                this.attractionService.areImported(),
                this.countryService.areImported()
        };

        return super.view("json/import-json", "areImported", areImported);
    }

    @GetMapping("/xml")
    public ModelAndView importXml() {
        boolean[] areImported = new boolean[]{
                this.personalDataService.areImported(),
                this.visitorService.areImported()
        };

        return super.view("xml/import-xml", "areImported", areImported);
    }

    @GetMapping("/countries")
    public ModelAndView importCountries() throws IOException {
        String fileContent = this.countryService.readCountryFileContent();
        return super.view("json/import-countries", "countries", fileContent);
    }

    @PostMapping("/countries")
    public ModelAndView importCountriesConfirm() throws IOException {
        System.out.println(this.countryService.importCountries());

        return super.redirect("/import/json");
    }

    @GetMapping("/attractions")
    public ModelAndView importAttractions() throws IOException {
        String fileContent = this.attractionService.readAttractionsFileContent();
        return super.view("json/import-attractions", "attractions", fileContent);
    }

    @PostMapping("/attractions")
    public ModelAndView importAttractionsConfirm() throws IOException {
        System.out.println(this.attractionService.importAttractions());

        return super.redirect("/import/json");
    }

    @GetMapping("/personalData")
    public ModelAndView importPersonalData() throws IOException, JAXBException {
        String fileContent = this.personalDataService.readPersonalDataFileContent();

        return super.view("xml/import-personalData", "personalData", fileContent);
    }

    @PostMapping("/personalData")
    public ModelAndView importPersonalDataConfirm() throws IOException, JAXBException {
        System.out.println(this.personalDataService.importPersonalData());

        return super.redirect("/import/xml");
    }

    @GetMapping("/visitors")
    public ModelAndView importVisitors() throws IOException, JAXBException {
        String fileContent = this.visitorService.readVisitorsFileContent();

        return super.view("xml/import-visitors", "visitors", fileContent);
    }

    @PostMapping("/visitors")
    public ModelAndView importVisitorsConfirm() throws IOException, JAXBException {
        System.out.println(this.visitorService.importVisitors());

        return super.redirect("/import/xml");
    }

}
