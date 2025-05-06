package lab_1.entities;

import jakarta.persistence.*;

import java.util.Set;

@Entity
@Table(name = "drivers")
public class Driver {
    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    private Long id;

    @Basic
    @Column(name = "full_name")
    private String name;

    @ManyToMany(mappedBy = "drivers")
    private Set<Truck> trucks;

    public Long getId() {
        return this.id;
    }

    public void setId(Long id) {
        this.id = id;
    }

    public String getName() {
        return this.name;
    }

    public void setName(String name) {
        this.name = name;
    }

    public Set<Truck> getTrucks() {
        return this.trucks;
    }

    public void setTrucks(Set<Truck> trucks) {
        this.trucks = trucks;
    }
}
