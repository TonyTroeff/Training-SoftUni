package softuni.exam.models.dto;

import com.google.gson.annotations.Expose;
import org.hibernate.validator.constraints.Length;

import jakarta.validation.constraints.NotNull;

public class SellerInputDto {
    @Expose
    @NotNull
    @Length(min = 2, max = 30)
    private String firstName;

    @Expose
    @NotNull
    @Length(min = 2, max = 30)
    private String lastName;

    @Expose
    @NotNull
    @Length(min = 3, max = 6)
    private String personalNumber;

    public String getFirstName() {
        return firstName;
    }

    public void setFirstName(String firstName) {
        this.firstName = firstName;
    }

    public String getLastName() {
        return lastName;
    }

    public void setLastName(String lastName) {
        this.lastName = lastName;
    }

    public String getPersonalNumber() {
        return personalNumber;
    }

    public void setPersonalNumber(String personalNumber) {
        this.personalNumber = personalNumber;
    }
}
