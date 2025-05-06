package lab_1.entities;

import jakarta.persistence.*;

import java.util.Set;

// For "table-per-class" or "joined" setup:
@Table(name = "trucks")

// For "single-table" setup:
// @DiscriminatorValue("truck")

@Entity
public class Truck extends Vehicle {
    @Basic
    private double loadCapacity;

    @ManyToMany
    @JoinTable(name = "drivers_trucks", joinColumns = @JoinColumn(name = "truck_id", referencedColumnName = "id"), inverseJoinColumns = @JoinColumn(name = "driver_id", referencedColumnName = "id"))
    private Set<Driver> drivers;

    public double getLoadCapacity() {
        return this.loadCapacity;
    }

    public void setLoadCapacity(double loadCapacity) {
        this.loadCapacity = loadCapacity;
    }

    public Set<Driver> getDrivers() {
        return this.drivers;
    }

    public void setDrivers(Set<Driver> drivers) {
        this.drivers = drivers;
    }
}
