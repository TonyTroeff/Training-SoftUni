package lab_1;

import lab_1.dtos.ManagerDto;
import lab_1.dtos.SubordinateDto;
import lab_1.models.Employee;
import lab_1.repositories.EmployeeRepository;
import org.modelmapper.ModelMapper;
import org.springframework.boot.CommandLineRunner;
import org.springframework.stereotype.Component;

import java.math.BigDecimal;
import java.time.LocalDate;
import java.util.Collections;
import java.util.HashSet;
import java.util.List;

@Component
public class Runner implements CommandLineRunner {
    private final EmployeeRepository employeeRepository;

    public Runner(EmployeeRepository employeeRepository) {
        this.employeeRepository = employeeRepository;
    }

    @Override
    public void run(String... args) {
        this.seedEmployees();

        ModelMapper modelMapper = new ModelMapper();
        this.printAllManagers(modelMapper);
        this.printAllSubordinates(modelMapper);
    }

    private void printAllManagers(ModelMapper modelMapper) {
        List<Employee> managers = this.employeeRepository.findAllManagers();
        for (Employee manager : managers) {
            ManagerDto dto = modelMapper.map(manager, ManagerDto.class);
            System.out.println(dto);
        }
    }

    private void printAllSubordinates(ModelMapper modelMapper) {
        List<Employee> subordinates = this.employeeRepository.findAllSubordinates();
        for (Employee subordinate : subordinates) {
            SubordinateDto dto = modelMapper.map(subordinate, SubordinateDto.class);
            System.out.println(dto);
        }
    }

    private void seedEmployees() {
        Employee e1 = createEmployee("Carol", "White", "789 Pine St", BigDecimal.valueOf(4000), LocalDate.of(1990, 7, 7));
        Employee e2 = createEmployee("David", "Brown", "101 Maple Rd", BigDecimal.valueOf(4100), LocalDate.of(1991, 9, 9));
        Employee e3 = createEmployee("Eve", "Davis", "202 Elm St", BigDecimal.valueOf(4200), LocalDate.of(1989, 1, 14));
        Employee e4 = createEmployee("Frank", "Wilson", "303 Cedar Dr", BigDecimal.valueOf(4300), LocalDate.of(1992, 12, 2));
        Employee e5 = createEmployee("Grace", "Martinez", "404 Birch Blvd", BigDecimal.valueOf(4450), LocalDate.of(1988, 4, 20));
        Employee e6 = createEmployee("Henry", "Anderson", "505 Spruce Ln", BigDecimal.valueOf(4300), LocalDate.of(1994, 6, 23));
        Employee e7 = createEmployee("Ivy", "Thomas", "606 Willow Ct", BigDecimal.valueOf(4150), LocalDate.of(1993, 11, 4));
        Employee e8 = createEmployee("Jake", "Moore", "707 Aspen Pl", BigDecimal.valueOf(4400), LocalDate.of(1995, 8, 15));

        Employee m1 = createEmployee("Alice", "Smith", "123 Main St", BigDecimal.valueOf(7000), LocalDate.of(1980, 5, 15), List.of(e1, e2, e3, e4, e5));
        Employee m2 = createEmployee("Bob", "Johnson", "456 Oak Ave", BigDecimal.valueOf(6800), LocalDate.of(1978, 3, 10), List.of(e1, e4, e6, e7, e8));

        this.employeeRepository.saveAll(List.of(e1, e2, e3, e4, e5, e6, e7, e8, m1, m2));
    }

    private Employee createEmployee(String firstName, String lastName, String address, BigDecimal salary, LocalDate birthday) {
        return this.createEmployee(firstName, lastName, address, salary, birthday, Collections.emptyList());
    }

    private Employee createEmployee(String firstName, String lastName, String address, BigDecimal salary, LocalDate birthday, List<Employee> subordinates) {
        Employee employee = new Employee();
        employee.setFirstName(firstName);
        employee.setLastName(lastName);
        employee.setAddress(address);
        employee.setSalary(salary);
        employee.setBirthday(birthday);
        employee.setSubordinates(new HashSet<>(subordinates));

        return employee;
    }

}
