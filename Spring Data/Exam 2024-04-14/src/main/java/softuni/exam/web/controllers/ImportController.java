package softuni.exam.web.controllers;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Controller;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.PostMapping;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.servlet.ModelAndView;
import softuni.exam.service.SellerService;
import softuni.exam.service.SaleService;
import softuni.exam.service.DeviceService;

import jakarta.xml.bind.JAXBException;
import java.io.IOException;

@Controller
@RequestMapping("/import")
public class ImportController extends BaseController {
    private final DeviceService deviceService;
    private final SaleService saleService;
    private final SellerService sellerService;

    @Autowired
    public ImportController(DeviceService deviceService, SaleService saleService, SellerService sellerService) {
        this.deviceService = deviceService;
        this.saleService = saleService;
        this.sellerService = sellerService;
    }

    @GetMapping("/json")
    public ModelAndView importJson() {
        boolean[] areImported = new boolean[]{
                this.saleService.areImported(),
                this.sellerService.areImported()
        };

        return super.view("json/import-json", "areImported", areImported);
    }

    @GetMapping("/xml")
    public ModelAndView importXml() {
        boolean[] areImported = new boolean[]{
                this.deviceService.areImported()
        };

        return super.view("xml/import-xml", "areImported", areImported);
    }

    @GetMapping("/devices")
    public ModelAndView importDevices() throws IOException {
        String devicesXmlFileContent = this.deviceService.readDevicesFromFile();

        return super.view("xml/import-devices", "devices", devicesXmlFileContent);
    }

    @PostMapping("/devices")
    public ModelAndView importDevicesConfirm() throws IOException, JAXBException {
        System.out.println(this.deviceService.importDevices());

        return super.redirect("/import/xml");
    }

    @GetMapping("/sales")
    public ModelAndView importSales() throws IOException {
        String fileContent = this.saleService.readSalesFileContent();

        return super.view("json/import-sales", "sales", fileContent);
    }

    @PostMapping("/sales")
    public ModelAndView importSalesConfirm() throws IOException {
        System.out.println(this.saleService.importSales());
        return super.redirect("/import/json");
    }

    @GetMapping("/sellers")
    public ModelAndView importSellers() throws IOException {
        String fileContent = this.sellerService.readSellersFromFile();

        return super.view("json/import-sellers", "sellers", fileContent);
    }

    @PostMapping("/sellers")
    public ModelAndView importSellersConfirm() throws IOException, JAXBException {
        System.out.println(this.sellerService.importSellers());
        return super.redirect("/import/json");
    }
}
