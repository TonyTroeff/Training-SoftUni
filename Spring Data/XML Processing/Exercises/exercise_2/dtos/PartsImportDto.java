package exercise_2.dtos;

import jakarta.xml.bind.annotation.XmlAccessType;
import jakarta.xml.bind.annotation.XmlAccessorType;
import jakarta.xml.bind.annotation.XmlElement;
import jakarta.xml.bind.annotation.XmlRootElement;

import java.util.List;

@XmlRootElement(name = "parts")
@XmlAccessorType(XmlAccessType.FIELD)
public class PartsImportDto {
    @XmlElement(name = "part")
    private List<PartInputDto> parts;

    public List<PartInputDto> getParts() {
        return parts;
    }

    public void setParts(List<PartInputDto> parts) {
        this.parts = parts;
    }
}
