package softuni.exam.models.dto;

import jakarta.xml.bind.annotation.XmlAccessType;
import jakarta.xml.bind.annotation.XmlAccessorType;
import jakarta.xml.bind.annotation.XmlElement;
import jakarta.xml.bind.annotation.XmlRootElement;

import java.util.List;

@XmlRootElement(name = "personal_datas")
@XmlAccessorType(XmlAccessType.FIELD)
public class PersonalDataImportDto {
    @XmlElement(name = "personal_data")
    private List<PersonalDataInputDto> input;

    public List<PersonalDataInputDto> getInput() {
        return input;
    }

    public void setInput(List<PersonalDataInputDto> input) {
        this.input = input;
    }
}
