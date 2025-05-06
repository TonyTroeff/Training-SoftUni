package lab_1.entities;

import jakarta.persistence.*;

// For "table-per-class" or "joined" setup:
@Table(name = "planes")

// For "single-table" setup:
// @DiscriminatorValue("plane")

@Entity
public class Plane extends Vehicle {
    @Basic
    private int passengersCapacity;

    @ManyToOne
    @JoinColumn(name = "company_id", referencedColumnName = "id")
    private Company company;

    public int getPassengersCapacity() {
        return this.passengersCapacity;
    }

    public void setPassengersCapacity(int passengersCapacity) {
        this.passengersCapacity = passengersCapacity;
    }

    public Company getCompany() {
        return this.company;
    }

    public void setCompany(Company company) {
        this.company = company;
    }
}
