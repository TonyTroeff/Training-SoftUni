package softuni.exam.web.controllers;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Controller;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.servlet.ModelAndView;
import softuni.exam.service.DeviceService;
import softuni.exam.service.SaleService;

@Controller
@RequestMapping("/export")
public class ExportController extends BaseController {
    private final DeviceService deviceService;

    @Autowired
    public ExportController(DeviceService deviceService) {
        this.deviceService = deviceService;
    }


    @GetMapping("/devices")
    public ModelAndView exportDevices() {
        String exportedDevices = this.deviceService.exportDevices();

        return super.view("export/export-devices.html", "exportedDevices", exportedDevices);
    }
}
