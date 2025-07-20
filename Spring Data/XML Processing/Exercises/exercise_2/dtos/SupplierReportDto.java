package exercise_2.dtos;

import jakarta.xml.bind.annotation.XmlAccessType;
import jakarta.xml.bind.annotation.XmlAccessorType;
import jakarta.xml.bind.annotation.XmlAttribute;

@XmlAccessorType(XmlAccessType.FIELD)
public class SupplierReportDto {
    @XmlAttribute(name = "id")
    private final Long id;

    @XmlAttribute(name = "name")
    private final String name;

    @XmlAttribute(name = "parts-count")
    private final Integer partsCount;

    public SupplierReportDto(Long id, String name, Integer partsCount) {
        this.id = id;
        this.name = name;
        this.partsCount = partsCount;
    }

    public Long getId() {
        return id;
    }

    public String getName() {
        return name;
    }

    public Integer getPartsCount() {
        return partsCount;
    }
}
