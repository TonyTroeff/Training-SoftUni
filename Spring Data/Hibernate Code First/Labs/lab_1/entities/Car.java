package lab_1.entities;

import jakarta.persistence.*;

// For "table-per-class" or "joined" setup:
@Table(name = "cars")

// For "single-table" setup:
// @DiscriminatorValue("car")

@Entity
public class Car extends Vehicle {
    @Basic
    private Integer seats;

    @OneToOne
    @JoinColumn(name = "plate_number_id", referencedColumnName = "id")
    private PlateNumber plateNumber;

    public Integer getSeats() {
        return this.seats;
    }

    public void setSeats(Integer seats) {
        this.seats = seats;
    }

    public PlateNumber getPlateNumber() {
        return this.plateNumber;
    }

    public void setPlateNumber(PlateNumber plateNumber) {
        this.plateNumber = plateNumber;
    }
}
