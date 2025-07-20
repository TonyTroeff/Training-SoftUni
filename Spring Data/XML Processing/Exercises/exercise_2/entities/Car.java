package exercise_2.entities;

import jakarta.persistence.*;

import java.util.Set;

@Entity
@Table(name = "cars")
public class Car extends BaseEntity {
    @Column(name = "make", nullable = false)
    private String make;

    @Column(name = "model", nullable = false)
    private String model;

    @Column(name = "travelled_distance", nullable = false)
    private Long travelledDistance;

    @ManyToMany
    @JoinTable(name = "cars_parts", joinColumns = @JoinColumn(name = "car_id"), inverseJoinColumns = @JoinColumn(name = "part_id"))
    private Set<Part> parts;

    public String getMake() {
        return make;
    }

    public void setMake(String make) {
        this.make = make;
    }

    public String getModel() {
        return model;
    }

    public void setModel(String model) {
        this.model = model;
    }

    public Long getTravelledDistance() {
        return travelledDistance;
    }

    public void setTravelledDistance(Long travelledDistance) {
        this.travelledDistance = travelledDistance;
    }

    public Set<Part> getParts() {
        return parts;
    }

    public void setParts(Set<Part> parts) {
        this.parts = parts;
    }
}
