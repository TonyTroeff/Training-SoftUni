package softuni.exam.models.entity;

import softuni.exam.models.enums.DeviceType;

import jakarta.persistence.*;

@Entity
@Table(name = "devices")
public class Device extends BaseEntity {
    @Column(name = "brand", nullable = false)
    private String brand;

    @Column(name = "device_type")
    @Enumerated(EnumType.STRING)
    private DeviceType type;

    @Column(name = "model", nullable = false, unique = true)
    private String model;

    @Column(name = "price")
    private Double price;

    @Column(name = "storage")
    private Integer storage;

    @ManyToOne
    @JoinColumn(name = "sale_id")
    private Sale sale;

    public String getBrand() {
        return brand;
    }

    public void setBrand(String brand) {
        this.brand = brand;
    }

    public DeviceType getType() {
        return type;
    }

    public void setType(DeviceType type) {
        this.type = type;
    }

    public String getModel() {
        return model;
    }

    public void setModel(String model) {
        this.model = model;
    }

    public Double getPrice() {
        return price;
    }

    public void setPrice(Double price) {
        this.price = price;
    }

    public Integer getStorage() {
        return storage;
    }

    public void setStorage(Integer storage) {
        this.storage = storage;
    }

    public Sale getSale() {
        return sale;
    }

    public void setSale(Sale sale) {
        this.sale = sale;
    }
}
