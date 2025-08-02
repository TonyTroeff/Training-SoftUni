package exercise_2.dtos;

import jakarta.xml.bind.annotation.XmlAccessType;
import jakarta.xml.bind.annotation.XmlAccessorType;
import jakarta.xml.bind.annotation.XmlElement;
import jakarta.xml.bind.annotation.XmlRootElement;

import java.util.ArrayList;
import java.util.List;

@XmlRootElement(name = "cars")
@XmlAccessorType(XmlAccessType.FIELD)
public class CarsExportDto {
    @XmlElement(name = "car")
    private final List<CarDto> cars;

    public CarsExportDto() {
        this.cars = new ArrayList<>();
    }

    public CarsExportDto(List<CarDto> cars) {
        this.cars = cars;
    }
}
