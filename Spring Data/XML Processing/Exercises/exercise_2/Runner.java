package exercise_2;

import exercise_2.dtos.*;
import exercise_2.services.CarService;
import exercise_2.services.PartService;
import exercise_2.services.SupplierService;
import jakarta.xml.bind.JAXBContext;
import jakarta.xml.bind.JAXBException;
import jakarta.xml.bind.Marshaller;
import jakarta.xml.bind.Unmarshaller;
import org.springframework.boot.CommandLineRunner;
import org.springframework.core.io.ClassPathResource;
import org.springframework.stereotype.Component;

import java.io.IOException;
import java.io.InputStream;
import java.util.ArrayList;
import java.util.HashSet;
import java.util.List;
import java.util.Set;
import java.util.concurrent.ThreadLocalRandom;

@Component
public class Runner implements CommandLineRunner {
    private final SupplierService supplierService;
    private final PartService partService;
    private final CarService carService;

    public Runner(SupplierService supplierService, PartService partService, CarService carService) {
        this.supplierService = supplierService;
        this.partService = partService;
        this.carService = carService;
    }

    @Override
    public void run(String... args) throws Exception {
        JAXBContext jaxbImportContext = JAXBContext.newInstance(SuppliersImportDto.class, PartsImportDto.class, CarsImportDto.class);
        Unmarshaller unmarshaller = jaxbImportContext.createUnmarshaller();

        JAXBContext jaxbExportContext = JAXBContext.newInstance(SuppliersReportExportDto.class, CarsExtendedExportDto.class);
        Marshaller marshaller = jaxbExportContext.createMarshaller();
        marshaller.setProperty(Marshaller.JAXB_FORMATTED_OUTPUT, true);

        List<SupplierDto> suppliers = seedSuppliers(unmarshaller);
        List<PartDto> parts = seedParts(unmarshaller, suppliers);
        seedCars(unmarshaller, parts);

        System.out.println("Seeding finished");

        List<SupplierReportDto> supplierReports = supplierService.generateReport(false);
        SuppliersReportExportDto exportDto = new SuppliersReportExportDto(supplierReports);
        marshaller.marshal(exportDto, System.out);

        List<CarExtendedDto> cars = carService.getExtended();
        CarsExtendedExportDto carsExtendedExportDto = new CarsExtendedExportDto(cars);
        marshaller.marshal(carsExtendedExportDto, System.out);
    }

    private List<SupplierDto> seedSuppliers(Unmarshaller unmarshaller) throws JAXBException, IOException {
        SuppliersImportDto importDto;
        try (InputStream inputStream = readResourceFileAsStream("suppliers.xml")) {
            importDto = (SuppliersImportDto) unmarshaller.unmarshal(inputStream);
        }

        List<SupplierDto> result = new ArrayList<>();
        for (SupplierInputDto inputDto : importDto.getSuppliers()) {
            SupplierDto createdSupplier = supplierService.create(inputDto);
            result.add(createdSupplier);
        }

        return result;
    }

    private List<PartDto> seedParts(Unmarshaller unmarshaller, List<SupplierDto> suppliers) throws JAXBException, IOException {
        PartsImportDto importDto;
        try (InputStream inputStream = readResourceFileAsStream("parts.xml")) {
            importDto = (PartsImportDto) unmarshaller.unmarshal(inputStream);
        }

        List<PartDto> result = new ArrayList<>();
        for (PartInputDto inputDto : importDto.getParts()) {
            int randomSupplierIndex = ThreadLocalRandom.current().nextInt(suppliers.size());
            SupplierDto randomSupplier = suppliers.get(randomSupplierIndex);

            PartRelationsDto relationsDto = new PartRelationsDto(randomSupplier.getId());

            PartDto createdPart = partService.create(inputDto, relationsDto);
            result.add(createdPart);
        }

        return result;
    }

    private void seedCars(Unmarshaller unmarshaller, List<PartDto> parts) throws JAXBException, IOException {
        CarsImportDto importDto;
        try (InputStream inputStream = readResourceFileAsStream("cars.xml")) {
            importDto = (CarsImportDto) unmarshaller.unmarshal(inputStream);
        }

        for (CarInputDto inputDto : importDto.getCars()) {
            Set<Long> partIds = new HashSet<>();
            int randomPartsCount = ThreadLocalRandom.current().nextInt(0, 3);

            for (int i = 0; i < randomPartsCount; i++) {
                int randomPartIndex = ThreadLocalRandom.current().nextInt(parts.size());
                PartDto randomPart = parts.get(randomPartIndex);
                partIds.add(randomPart.getId());
            }

            CarRelationsDto relationsDto = new CarRelationsDto(partIds);

            carService.create(inputDto, relationsDto);
        }
    }

    private static InputStream readResourceFileAsStream(String path) throws IOException {
        ClassPathResource resource = new ClassPathResource(path);
        return resource.getInputStream();
    }
}
