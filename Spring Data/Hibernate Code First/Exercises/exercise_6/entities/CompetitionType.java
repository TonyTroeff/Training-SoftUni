package exercise_6.entities;

import jakarta.persistence.*;

import java.util.HashSet;
import java.util.Set;

@Entity
@Table(name = "competition_types")
public class CompetitionType extends BaseEntity {
    @Basic
    @Column(name = "name", nullable = false)
    private String name;

    @OneToMany(mappedBy = "competitionType")
    private Set<Competition> competitions = new HashSet<>();

    public String getName() {
        return this.name;
    }

    public void setName(String name) {
        this.name = name;
    }

    public Set<Competition> getCompetitions() {
        return this.competitions;
    }

    public void setCompetitions(Set<Competition> competitions) {
        this.competitions = competitions;
    }
}
