package exercise_2.models;

import jakarta.persistence.Basic;
import jakarta.persistence.Column;
import jakarta.persistence.Entity;
import jakarta.persistence.Table;

@Entity
@Table(name = "towns")
public class Town extends BaseEntity {
    @Basic
    @Column(name = "name", nullable = false)
    private String name;

    @Basic
    @Column(name = "country", nullable = false)
    private String country;

    public String getName() {
        return this.name;
    }

    public void setName(String name) {
        this.name = name;
    }

    public String getCountry() {
        return this.country;
    }

    public void setCountry(String country) {
        this.country = country;
    }
}
