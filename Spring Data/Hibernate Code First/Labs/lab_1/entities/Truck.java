package lab_1.entities;

import jakarta.persistence.*;

import java.util.HashSet;
import java.util.Set;

// For "table-per-class" or "joined" setup:
@Table(name = "trucks")

// For "single-table" setup:
// @DiscriminatorValue("truck")

@Entity
public class Truck extends Vehicle {
    @Column(name = "load_capacity")
    private double loadCapacity;

    @ManyToMany
    @JoinTable(name = "drivers_trucks", joinColumns = @JoinColumn(name = "truck_id"), inverseJoinColumns = @JoinColumn(name = "driver_id"))
    private Set<Driver> drivers = new HashSet<>();

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
