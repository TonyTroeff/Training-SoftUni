package softuni.exam.models.dto;

import com.google.gson.annotations.Expose;
import org.hibernate.validator.constraints.Length;

import jakarta.validation.constraints.NotNull;

public class SaleInputDto {
    @Expose
    private Boolean discounted;

    @Expose
    @NotNull
    @Length(min = 7, max = 7)
    private String number;

    @Expose
    @NotNull
    private String saleDate;

    @Expose
    private Long seller;

    public Boolean getDiscounted() {
        return discounted;
    }

    public void setDiscounted(Boolean discounted) {
        this.discounted = discounted;
    }

    public String getNumber() {
        return number;
    }

    public void setNumber(String number) {
        this.number = number;
    }

    public String getSaleDate() {
        return saleDate;
    }

    public void setSaleDate(String saleDate) {
        this.saleDate = saleDate;
    }

    public Long getSeller() {
        return seller;
    }

    public void setSeller(Long seller) {
        this.seller = seller;
    }
}
