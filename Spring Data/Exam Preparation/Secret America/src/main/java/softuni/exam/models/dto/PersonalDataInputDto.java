package softuni.exam.models.dto;

import jakarta.validation.constraints.*;
import jakarta.xml.bind.annotation.XmlAccessType;
import jakarta.xml.bind.annotation.XmlAccessorType;
import jakarta.xml.bind.annotation.XmlElement;
import jakarta.xml.bind.annotation.adapters.XmlJavaTypeAdapter;
import org.hibernate.validator.constraints.Length;
import softuni.exam.util.LocalDateXmlAdapter;

import java.time.LocalDate;

@XmlAccessorType(XmlAccessType.FIELD)
public class PersonalDataInputDto {
    @XmlElement(name = "age")
    @Min(value = 1)
    @Max(value = 100)
    private Integer age;

    @XmlElement(name = "gender")
    @Pattern(regexp = "^[MF]$")
    private String gender;

    @XmlElement(name = "birth_date")
    @XmlJavaTypeAdapter(LocalDateXmlAdapter.class)
    @Past
    private LocalDate birthDate;

    @XmlElement(name = "card_number")
    @NotNull
    @Length(min = 9, max = 9)
    private String cardNumber;

    public Integer getAge() {
        return age;
    }

    public void setAge(Integer age) {
        this.age = age;
    }

    public String getGender() {
        return gender;
    }

    public void setGender(String gender) {
        this.gender = gender;
    }

    public LocalDate getBirthDate() {
        return birthDate;
    }

    public void setBirthDate(LocalDate birthDate) {
        this.birthDate = birthDate;
    }

    public String getCardNumber() {
        return cardNumber;
    }

    public void setCardNumber(String cardNumber) {
        this.cardNumber = cardNumber;
    }
}
