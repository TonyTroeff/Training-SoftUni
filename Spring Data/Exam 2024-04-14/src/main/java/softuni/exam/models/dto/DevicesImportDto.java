package softuni.exam.models.dto;

import jakarta.xml.bind.annotation.XmlAccessType;
import jakarta.xml.bind.annotation.XmlAccessorType;
import jakarta.xml.bind.annotation.XmlElement;
import jakarta.xml.bind.annotation.XmlRootElement;
import java.util.List;

@XmlRootElement(name = "devices")
@XmlAccessorType(XmlAccessType.FIELD)
public class DevicesImportDto {
    @XmlElement(name = "device")
    private List<DeviceInputDto> input;

    public List<DeviceInputDto> getInput() {
        return input;
    }

    public void setInput(List<DeviceInputDto> input) {
        this.input = input;
    }
}
