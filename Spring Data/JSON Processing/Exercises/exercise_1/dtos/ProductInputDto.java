package exercise_1.dtos;

import com.google.gson.annotations.Expose;
import exercise_1.models.Category;
import exercise_1.models.User;
import jakarta.validation.constraints.DecimalMin;
import jakarta.validation.constraints.NotEmpty;
import jakarta.validation.constraints.NotNull;
import jakarta.validation.constraints.Size;

import java.math.BigDecimal;
import java.util.List;

public class ProductInputDto {
    @Expose
    @NotNull
    @Size(min = 3)
    private String name;

    @Expose
    @NotNull
    @DecimalMin("0.01")
    private BigDecimal price;

    @NotNull
    private User seller;

    private User buyer;

    @NotNull
    @NotEmpty
    private List<Category> categories;

    public String getName() {
        return this.name;
    }

    public void setName(String name) {
        this.name = name;
    }

    public BigDecimal getPrice() {
        return this.price;
    }

    public void setPrice(BigDecimal price) {
        this.price = price;
    }

    public User getSeller() {
        return this.seller;
    }

    public void setSeller(User seller) {
        this.seller = seller;
    }

    public User getBuyer() {
        return this.buyer;
    }

    public void setBuyer(User buyer) {
        this.buyer = buyer;
    }

    public List<Category> getCategories() {
        return this.categories;
    }

    public void setCategories(List<Category> categories) {
        this.categories = categories;
    }
}
