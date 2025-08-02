package exercise_2;

import exercise_2.dtos.*;
import exercise_2.services.*;
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
    private final CarService carService;
    private final CustomerService customerService;
    private final PartService partService;
    private final SaleService saleService;
    private final SupplierService supplierService;

    public Runner(CarService carService, CustomerService customerService, PartService partService, SaleService saleService, SupplierService supplierService) {
        this.carService = carService;
        this.customerService = customerService;
        this.partService = partService;
        this.saleService = saleService;
        this.supplierService = supplierService;
    }

    @Override
    public void run(String... args) throws Exception {
        JAXBContext jaxbImportContext = JAXBContext.newInstance(CarsImportDto.class, CustomersImportDto.class, PartsImportDto.class, SuppliersImportDto.class);
        Unmarshaller unmarshaller = jaxbImportContext.createUnmarshaller();

        JAXBContext jaxbExportContext = JAXBContext.newInstance(CarsExportDto.class, CarsExtendedExportDto.class, CustomersExportDto.class, SuppliersReportExportDto.class);
        Marshaller marshaller = jaxbExportContext.createMarshaller();
        marshaller.setProperty(Marshaller.JAXB_FORMATTED_OUTPUT, true);

        List<SupplierDto> suppliers = seedSuppliers(unmarshaller);
        List<PartDto> parts = seedParts(unmarshaller, suppliers);
        List<CarDto> cars = seedCars(unmarshaller, parts);
        List<CustomerDto> customers = seedCustomers(unmarshaller);
        seedSales(cars, customers);

        System.out.println("Seeding finished");

        query1(marshaller);
        query2(marshaller);
        query3(marshaller);
        query4(marshaller);
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

    private List<CarDto> seedCars(Unmarshaller unmarshaller, List<PartDto> parts) throws JAXBException, IOException {
        CarsImportDto importDto;
        try (InputStream inputStream = readResourceFileAsStream("cars.xml")) {
            importDto = (CarsImportDto) unmarshaller.unmarshal(inputStream);
        }

        List<CarDto> result = new ArrayList<>();
        for (CarInputDto inputDto : importDto.getCars()) {
            Set<Long> partIds = new HashSet<>();
            int randomPartsCount = ThreadLocalRandom.current().nextInt(0, 3);

            for (int i = 0; i < randomPartsCount; i++) {
                int randomPartIndex = ThreadLocalRandom.current().nextInt(parts.size());
                PartDto randomPart = parts.get(randomPartIndex);
                partIds.add(randomPart.getId());
            }

            CarRelationsDto relationsDto = new CarRelationsDto(partIds);

            CarDto createdCar = carService.create(inputDto, relationsDto);
            result.add(createdCar);
        }

        return result;
    }

    private List<CustomerDto> seedCustomers(Unmarshaller unmarshaller) throws JAXBException, IOException {
        CustomersImportDto importDto;
        try (InputStream inputStream = readResourceFileAsStream("customers.xml")) {
            importDto = (CustomersImportDto) unmarshaller.unmarshal(inputStream);
        }

        List<CustomerDto> result = new ArrayList<>();
        for (CustomerInputDto inputDto : importDto.getCustomers()) {
            CustomerDto createdCustomer = customerService.create(inputDto);
            result.add(createdCustomer);
        }

        return result;
    }

    private void seedSales(List<CarDto> cars, List<CustomerDto> customers) {
        int randomSalesCount = ThreadLocalRandom.current().nextInt(100, 300);
        for (int i = 0; i < randomSalesCount; i++) {
            SaleInputDto inputDto = new SaleInputDto();
            inputDto.setDiscount(0.05 * ThreadLocalRandom.current().nextInt(11));

            int randomCarIndex = ThreadLocalRandom.current().nextInt(cars.size());
            CarDto randomCar = cars.get(randomCarIndex);

            int randomCustomerIndex = ThreadLocalRandom.current().nextInt(customers.size());
            CustomerDto randomCustomer = customers.get(randomCustomerIndex);

            SaleRelationsDto relationsDto = new SaleRelationsDto(randomCar.getId(), randomCustomer.getId());

            saleService.create(inputDto, relationsDto);
        }
    }

    private void query1(Marshaller marshaller) throws JAXBException {
        List<CustomerDto> customers = customerService.exportAll();
        CustomersExportDto exportDto = new CustomersExportDto(customers);
        marshaller.marshal(exportDto, System.out);
    }

    private void query2(Marshaller marshaller) throws JAXBException {
        List<CarDto> cars = carService.exportAllByMake("Toyota");
        CarsExportDto exportDto = new CarsExportDto(cars);
        marshaller.marshal(exportDto, System.out);
    }

    private void query3(Marshaller marshaller) throws JAXBException {
        List<SupplierReportDto> supplierReports = supplierService.generateReport(false);
        SuppliersReportExportDto exportDto = new SuppliersReportExportDto(supplierReports);
        marshaller.marshal(exportDto, System.out);
    }

    private void query4(Marshaller marshaller) throws JAXBException {
        List<CarExtendedDto> extendedCars = carService.getExtended();
        CarsExtendedExportDto carsExtendedExportDto = new CarsExtendedExportDto(extendedCars);
        marshaller.marshal(carsExtendedExportDto, System.out);
    }

    private static InputStream readResourceFileAsStream(String path) throws IOException {
        ClassPathResource resource = new ClassPathResource(path);
        return resource.getInputStream();
    }
}
