package exercise_2.dtos;

import jakarta.xml.bind.annotation.XmlAccessType;
import jakarta.xml.bind.annotation.XmlAccessorType;
import jakarta.xml.bind.annotation.XmlElement;
import jakarta.xml.bind.annotation.XmlRootElement;

import java.util.ArrayList;
import java.util.List;

@XmlRootElement(name = "suppliers")
@XmlAccessorType(XmlAccessType.FIELD)
public class SuppliersReportExportDto {
    @XmlElement(name = "supplier")
    private final List<SupplierReportDto> suppliers;

    public SuppliersReportExportDto() {
        this.suppliers = new ArrayList<>();
    }

    public SuppliersReportExportDto(List<SupplierReportDto> suppliers) {
        this.suppliers = suppliers;
    }

    public List<SupplierReportDto> getSuppliers() {
        return suppliers;
    }
}
