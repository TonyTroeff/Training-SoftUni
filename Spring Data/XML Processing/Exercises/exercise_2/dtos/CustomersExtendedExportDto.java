package exercise_2.dtos;

import jakarta.xml.bind.annotation.XmlAccessType;
import jakarta.xml.bind.annotation.XmlAccessorType;
import jakarta.xml.bind.annotation.XmlElement;
import jakarta.xml.bind.annotation.XmlRootElement;

import java.util.ArrayList;
import java.util.List;

@XmlRootElement(name = "customers")
@XmlAccessorType(XmlAccessType.FIELD)
public class CustomersExtendedExportDto {
    @XmlElement(name = "customer")
    private final List<CustomerExtendedDto> customers;

    public CustomersExtendedExportDto() {
        this.customers = new ArrayList<>();
    }

    public CustomersExtendedExportDto(List<CustomerExtendedDto> customers) {
        this.customers = customers;
    }
}
