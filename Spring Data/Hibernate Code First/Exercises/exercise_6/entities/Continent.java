package exercise_6.entities;

import jakarta.persistence.*;

import java.util.HashSet;
import java.util.Set;

@Entity
@Table(name = "continents")
public class Continent extends BaseEntity {
    @Column(name = "name", nullable = false)
    private String name;

    @ManyToMany(mappedBy = "continents")
    private Set<Country> countries = new HashSet<>();

    public String getName() {
        return this.name;
    }

    public void setName(String name) {
        this.name = name;
    }

    public Set<Country> getCountries() {
        return this.countries;
    }

    public void setCountries(Set<Country> countries) {
        this.countries = countries;
    }
}
