package exercise_2.dtos;

import jakarta.xml.bind.annotation.XmlAccessType;
import jakarta.xml.bind.annotation.XmlAccessorType;
import jakarta.xml.bind.annotation.XmlElement;
import jakarta.xml.bind.annotation.XmlRootElement;

import java.util.List;

@XmlRootElement(name = "suppliers")
@XmlAccessorType(XmlAccessType.FIELD)
public class SuppliersImportDto {
    @XmlElement(name = "supplier")
    private List<SupplierInputDto> suppliers;

    public List<SupplierInputDto> getSuppliers() {
        return suppliers;
    }

    public void setSuppliers(List<SupplierInputDto> suppliers) {
        this.suppliers = suppliers;
    }
}
