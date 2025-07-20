package softuni.exam.models.dto;

import jakarta.xml.bind.annotation.XmlAccessType;
import jakarta.xml.bind.annotation.XmlAccessorType;
import jakarta.xml.bind.annotation.XmlElement;
import jakarta.xml.bind.annotation.XmlRootElement;

import java.util.List;

@XmlRootElement(name = "visitors")
@XmlAccessorType(XmlAccessType.FIELD)
public class VisitorImportDto {
    @XmlElement(name = "visitor")
    private List<VisitorInputDto> input;

    public List<VisitorInputDto> getInput() {
        return input;
    }

    public void setInput(List<VisitorInputDto> input) {
        this.input = input;
    }
}
