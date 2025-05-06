package lab_1.entities;

import jakarta.persistence.*;

import java.util.Set;

@Entity
@Table(name = "companies")
public class Company {
    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    private Long id;

    @Basic
    private String name;

    @OneToMany(mappedBy = "company")
    private Set<Plane> planes;

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

    public Set<Plane> getPlanes() {
        return this.planes;
    }

    public void setPlanes(Set<Plane> planes) {
        this.planes = planes;
    }
}
