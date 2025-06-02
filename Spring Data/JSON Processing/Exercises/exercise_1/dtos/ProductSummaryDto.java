package exercise_1.dtos;

import com.google.gson.annotations.Expose;

import java.math.BigDecimal;

public class ProductSummaryDto {
    @Expose
    private final String name;

    @Expose
    private final BigDecimal price;

    @Expose
    private final String seller;

    public ProductSummaryDto(String name, BigDecimal price, String seller) {
        this.name = name;
        this.price = price;
        this.seller = seller;
    }

    public String getName() {
        return this.name;
    }

    public BigDecimal getPrice() {
        return this.price;
    }

    public String getSeller() {
        return this.seller;
    }
}
