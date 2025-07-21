package softuni.exam.web.controllers;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Controller;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.servlet.ModelAndView;
import softuni.exam.service.*;

@Controller
public class HomeController extends BaseController {
    private final DeviceService volcanoService;
    private final SaleService saleService;
    private final SellerService sellerService;

    @Autowired
    public HomeController(DeviceService volcanoService, SaleService saleService, SellerService sellerService) {
        this.volcanoService = volcanoService;
        this.saleService = saleService;
        this.sellerService = sellerService;
    }

    @GetMapping("/")
    public ModelAndView index() {
        boolean areImported = this.volcanoService.areImported() &&
                this.sellerService.areImported() &&
                this.volcanoService.areImported()  &&
                this.saleService.areImported();

        return super.view("index", "areImported", areImported);
    }
}
