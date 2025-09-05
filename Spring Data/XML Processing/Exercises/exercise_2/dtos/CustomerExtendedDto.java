package exercise_2.dtos;

import jakarta.xml.bind.annotation.XmlAccessType;
import jakarta.xml.bind.annotation.XmlAccessorType;
import jakarta.xml.bind.annotation.XmlAttribute;

import java.math.BigDecimal;

@XmlAccessorType(XmlAccessType.FIELD)
public class CustomerExtendedDto {
    @XmlAttribute(name = "full-name")
    private final String name;

    @XmlAttribute(name = "bought-cars")
    private final Long boughtCars;

    @XmlAttribute(name = "spent-money")
    private final BigDecimal spentMoney;

    public CustomerExtendedDto(String name, Long boughtCars, BigDecimal spentMoney) {
        this.name = name;
        this.boughtCars = boughtCars;
        this.spentMoney = spentMoney;
    }

    public String getName() {
        return name;
    }

    public Long getBoughtCars() {
        return boughtCars;
    }

    public BigDecimal getSpentMoney() {
        return spentMoney;
    }
}
