package exercise_2.dtos;

import jakarta.xml.bind.annotation.XmlAccessType;
import jakarta.xml.bind.annotation.XmlAccessorType;
import jakarta.xml.bind.annotation.XmlElement;
import jakarta.xml.bind.annotation.XmlRootElement;

import java.util.ArrayList;
import java.util.List;

@XmlRootElement(name = "customers")
@XmlAccessorType(XmlAccessType.FIELD)
public class CustomersExportDto {
    @XmlElement(name = "customer")
    private final List<CustomerDto> customers;

    public CustomersExportDto() {
        this.customers = new ArrayList<>();
    }

    public CustomersExportDto(List<CustomerDto> customers) {
        this.customers = customers;
    }
}
