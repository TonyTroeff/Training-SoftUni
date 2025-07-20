package softuni.exam.web.controllers;


import org.springframework.stereotype.Controller;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.servlet.ModelAndView;
import softuni.exam.service.AttractionService;

@Controller
@RequestMapping("/export")
public class ExportController extends BaseController {

    private final AttractionService attractionService;

    public ExportController(AttractionService attractionService) {
        this.attractionService = attractionService;
    }

    @GetMapping("attractions")
    public ModelAndView exportVisitors() {

        String attractions = this.attractionService.exportAttractions();

        return super.view("export/export-attractions", "attractions", attractions);
    }
}
