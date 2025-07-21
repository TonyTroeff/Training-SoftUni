package softuni.exam.service.impl;

import org.modelmapper.ModelMapper;
import org.springframework.stereotype.Service;
import softuni.exam.models.dto.DeviceInputDto;
import softuni.exam.models.dto.DevicesImportDto;
import softuni.exam.models.entity.Device;
import softuni.exam.models.entity.Sale;
import softuni.exam.models.enums.DeviceType;
import softuni.exam.repository.DeviceRepository;
import softuni.exam.service.DeviceService;
import softuni.exam.service.SaleService;
import softuni.exam.util.ValidationUtil;
import softuni.exam.util.XmlParser;

import jakarta.xml.bind.JAXBException;
import java.io.IOException;
import java.nio.file.Files;
import java.nio.file.Path;
import java.nio.file.Paths;
import java.util.List;

@Service
public class DeviceServiceImpl implements DeviceService {
    private final SaleService saleService;
    private final DeviceRepository repository;
    private final XmlParser xmlParser;
    private final ValidationUtil validationUtil;
    private final ModelMapper modelMapper;

    public DeviceServiceImpl(SaleService saleService, DeviceRepository repository, XmlParser xmlParser, ValidationUtil validationUtil, ModelMapper modelMapper) {
        this.saleService = saleService;
        this.repository = repository;
        this.xmlParser = xmlParser;
        this.validationUtil = validationUtil;
        this.modelMapper = modelMapper;
    }

    @Override
    public boolean areImported() {
        return repository.count() > 0;
    }

    @Override
    public String readDevicesFromFile() throws IOException {
        Path path = Paths.get("src/main/resources/files/xml/devices.xml");
        return Files.readString(path);
    }

    @Override
    public String importDevices() throws IOException, JAXBException {
        DevicesImportDto devicesImportDto = xmlParser.parse(readDevicesFromFile(), DevicesImportDto.class);

        StringBuilder sb = new StringBuilder();
        for (DeviceInputDto inputDto : devicesImportDto.getInput()) {
            Device createdDevice = create(inputDto);

            if (createdDevice == null) sb.append(String.format("Invalid device%n"));
            else sb.append(String.format("Successfully imported device of type %s with brand %s%n", createdDevice.getType(), createdDevice.getBrand()));
        }

        return sb.toString();
    }

    @Override
    public String exportDevices() {
        List<Device> devices = repository.findExportable(DeviceType.SMART_PHONE, 1000.0, 128);

        StringBuilder sb = new StringBuilder();
        for (Device device : devices)
            sb.append(String.format("Device brand: %s%n   *Model: %s%n   **Storage: %d%n   ***Price: %.2f%n", device.getBrand(), device.getModel(), device.getStorage(), device.getPrice()));

        return sb.toString();
    }

    private Device create(DeviceInputDto inputDto) {
        if (!validationUtil.isValid(inputDto)) return null;

        try {
            Device device = modelMapper.map(inputDto, Device.class);

            Long saleId = inputDto.getSale();
            if (saleId != null) {
                Sale sale = saleService.getReferenceById(saleId);
                device.setSale(sale);
            }

            repository.save(device);

            return device;
        } catch (Exception e) {
            return null;
        }
    }
}
