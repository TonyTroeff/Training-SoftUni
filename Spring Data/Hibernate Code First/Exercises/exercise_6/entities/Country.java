package exercise_6.entities;

import jakarta.persistence.*;

import java.util.HashSet;
import java.util.Set;

@Entity
@Table(name = "countries")
public class Country extends BaseEntity {
    @Basic
    @Column(name = "name", nullable = false)
    private String name;

    @Basic
    @Column(name = "initials", length = 3, nullable = false)
    private String initials;

    @ManyToMany
    @JoinTable(name = "countries_continents", joinColumns = @JoinColumn(name = "country_id", referencedColumnName = "id"), inverseJoinColumns = @JoinColumn(name = "continent_id", referencedColumnName = "id"))
    private Set<Continent> continents = new HashSet<>();

    @OneToMany(mappedBy = "country")
    private Set<Town> towns = new HashSet<>();

    public String getName() {
        return this.name;
    }

    public void setName(String name) {
        this.name = name;
    }

    public String getInitials() {
        return this.initials;
    }

    public void setInitials(String initials) {
        this.initials = initials;
    }

    public Set<Continent> getContinents() {
        return this.continents;
    }

    public void setContinents(Set<Continent> continents) {
        this.continents = continents;
    }

    public Set<Town> getTowns() {
        return this.towns;
    }

    public void setTowns(Set<Town> towns) {
        this.towns = towns;
    }
}
