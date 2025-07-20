package softuni.exam.web.controllers;


import org.springframework.stereotype.Controller;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.servlet.ModelAndView;
import softuni.exam.service.AttractionService;
import softuni.exam.service.CountryService;
import softuni.exam.service.PersonalDataService;
import softuni.exam.service.VisitorService;

@Controller
public class HomeController extends BaseController {

    private final AttractionService attractionService;
    private final CountryService countryService;
    private final PersonalDataService personalDataService;
    private final VisitorService visitorService;

    public HomeController(AttractionService attractionService, CountryService countryService, PersonalDataService personalDataService, VisitorService visitorService) {
        this.attractionService = attractionService;
        this.countryService = countryService;
        this.personalDataService = personalDataService;
        this.visitorService = visitorService;
    }

    @GetMapping("/")
    public ModelAndView index() {
        boolean areImported = this.attractionService.areImported() &&
                this.countryService.areImported() &&
                this.personalDataService.areImported() &&
                this.visitorService.areImported();

        return super.view("index", "areImported", areImported);
    }
}
