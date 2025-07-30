package lab_1.entities;

import jakarta.persistence.*;

import java.math.BigDecimal;


// For "table-per-class" setup:
@Inheritance(strategy = InheritanceType.TABLE_PER_CLASS)

// For "joined" setup:
// @Inheritance(strategy = InheritanceType.JOINED)
// @Table(name = "vehicles")

// For "single-table" setup:
// @Inheritance(strategy = InheritanceType.SINGLE_TABLE)
// @Table(name = "vehicles")

@Entity
public abstract class Vehicle {
    // For "table-per-class" or "joined" setup:
    @GeneratedValue(strategy = GenerationType.TABLE)

    // For "single-table" setup:
    // @GeneratedValue(strategy = GenerationType.IDENTITY)

    @Id
    private Long id;

    @Column(name = "type")
    private String type;

    @Column(name = "model")
    private String model;

    @Column(name = "price")
    private BigDecimal price;

    @Column(name = "fuel_type")
    private String fuelType;

    public Long getId() {
        return this.id;
    }

    public void setId(Long id) {
        this.id = id;
    }

    public String getType() {
        return this.type;
    }

    public void setType(String type) {
        this.type = type;
    }

    public String getModel() {
        return this.model;
    }

    public void setModel(String model) {
        this.model = model;
    }

    public BigDecimal getPrice() {
        return this.price;
    }

    public void setPrice(BigDecimal price) {
        this.price = price;
    }

    public String getFuelType() {
        return this.fuelType;
    }

    public void setFuelType(String fuelType) {
        this.fuelType = fuelType;
    }
}
