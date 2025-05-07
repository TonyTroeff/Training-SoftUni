package exercise_6.entities;

import jakarta.persistence.*;

import java.util.HashSet;
import java.util.Set;

@Entity
@Table(name = "players")
public class Player extends BaseEntity {
    @Basic
    @Column(name = "name", nullable = false)
    private String name;

    @Basic
    @Column(name = "squad_number", nullable = false)
    private Integer squadNumber;

    @Basic
    @Column(name = "is_currently_injured")
    private Boolean isCurrentlyInjured;

    @ManyToOne
    @JoinColumn(name = "team_id", referencedColumnName = "id")
    private Team team;

    @ManyToOne(optional = false)
    @JoinColumn(name = "position_id", referencedColumnName = "id", nullable = false)
    private Position position;

    @OneToMany(mappedBy = "player")
    private Set<Performance> performances = new HashSet<>();

    public String getName() {
        return this.name;
    }

    public void setName(String name) {
        this.name = name;
    }

    public Integer getSquadNumber() {
        return this.squadNumber;
    }

    public void setSquadNumber(Integer squadNumber) {
        this.squadNumber = squadNumber;
    }

    public Boolean getIsCurrentlyInjured() {
        return this.isCurrentlyInjured;
    }

    public void setIsCurrentlyInjured(Boolean isCurrentlyInjured) {
        this.isCurrentlyInjured = isCurrentlyInjured;
    }

    public Team getTeam() {
        return this.team;
    }

    public void setTeam(Team team) {
        this.team = team;
    }

    public Position getPosition() {
        return this.position;
    }

    public void setPosition(Position position) {
        this.position = position;
    }

    public Set<Performance> getPerformances() {
        return this.performances;
    }

    public void setPerformances(Set<Performance> performances) {
        this.performances = performances;
    }
}
