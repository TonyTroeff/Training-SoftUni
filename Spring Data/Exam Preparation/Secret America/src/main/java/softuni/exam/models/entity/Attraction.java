package softuni.exam.models.entity;

import jakarta.persistence.*;

@Entity
@Table(name = "attractions")
public class Attraction extends BaseEntity {
    @Column(name = "description", nullable = false)
    private String description;

    @Column(name = "elevation", nullable = false)
    private Integer elevation;

    @Column(name = "name", nullable = false, unique = true)
    private String name;

    @Column(name = "type", nullable = false)
    private String type;

    @ManyToOne
    @JoinColumn(name = "country_id", nullable = false)
    private Country country;

    public String getDescription() {
        return description;
    }

    public void setDescription(String description) {
        this.description = description;
    }

    public Integer getElevation() {
        return elevation;
    }

    public void setElevation(Integer elevation) {
        this.elevation = elevation;
    }

    public String getName() {
        return name;
    }

    public void setName(String name) {
        this.name = name;
    }

    public String getType() {
        return type;
    }

    public void setType(String type) {
        this.type = type;
    }

    public Country getCountry() {
        return country;
    }

    public void setCountry(Country country) {
        this.country = country;
    }
}
