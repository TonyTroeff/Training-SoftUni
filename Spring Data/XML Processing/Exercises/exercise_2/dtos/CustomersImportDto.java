package exercise_2.dtos;

import jakarta.xml.bind.annotation.XmlAccessType;
import jakarta.xml.bind.annotation.XmlAccessorType;
import jakarta.xml.bind.annotation.XmlElement;
import jakarta.xml.bind.annotation.XmlRootElement;

import java.util.List;

@XmlRootElement(name = "customers")
@XmlAccessorType(XmlAccessType.FIELD)
public class CustomersImportDto {
    @XmlElement(name = "customer")
    private List<CustomerInputDto> customers;

    public List<CustomerInputDto> getCustomers() {
        return customers;
    }

    public void setCustomers(List<CustomerInputDto> customers) {
        this.customers = customers;
    }
}
