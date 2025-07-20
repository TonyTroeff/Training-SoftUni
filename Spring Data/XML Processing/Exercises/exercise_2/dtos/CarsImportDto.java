package exercise_2.dtos;

import jakarta.xml.bind.annotation.XmlAccessType;
import jakarta.xml.bind.annotation.XmlAccessorType;
import jakarta.xml.bind.annotation.XmlElement;
import jakarta.xml.bind.annotation.XmlRootElement;

import java.util.List;

@XmlRootElement(name = "cars")
@XmlAccessorType(XmlAccessType.FIELD)
public class CarsImportDto {
    @XmlElement(name = "car")
    private List<CarInputDto> cars;

    public List<CarInputDto> getCars() {
        return cars;
    }

    public void setCars(List<CarInputDto> cars) {
        this.cars = cars;
    }
}
