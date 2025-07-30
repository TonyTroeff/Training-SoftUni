package exercise_6.entities;

import jakarta.persistence.*;

import java.util.HashSet;
import java.util.Set;

@Entity
@Table(name = "positions")
public class Position extends BaseEntity {
    @Column(name = "name", length = 5, nullable = false)
    private String name;

    @Column(name = "description", length = 100, nullable = false)
    private String description;

    @OneToMany(mappedBy = "position")
    private Set<Player> players = new HashSet<>();

    public String getName() {
        return this.name;
    }

    public void setName(String name) {
        this.name = name;
    }

    public String getDescription() {
        return this.description;
    }

    public void setDescription(String description) {
        this.description = description;
    }

    public Set<Player> getPlayers() {
        return this.players;
    }

    public void setPlayers(Set<Player> players) {
        this.players = players;
    }
}
