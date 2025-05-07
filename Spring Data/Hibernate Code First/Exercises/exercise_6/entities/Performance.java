package exercise_6.entities;

import jakarta.persistence.*;

@Entity
@Table(name = "performances")
public class Performance extends BaseEntity {
    @Basic
    @Column(name = "goals", nullable = false)
    private Integer goals;

    @Basic
    @Column(name = "assists", nullable = false)
    private Integer assists;

    @Basic
    @Column(name = "play_time_in_minutes", nullable = false)
    private Float playTimeInMinutes;

    @ManyToOne(optional = false)
    @JoinColumn(name = "game_id", referencedColumnName = "id", nullable = false)
    private Game game;

    @ManyToOne(optional = false)
    @JoinColumn(name = "player_id", referencedColumnName = "id", nullable = false)
    private Player player;

    public Integer getGoals() {
        return this.goals;
    }

    public void setGoals(Integer goals) {
        this.goals = goals;
    }

    public Integer getAssists() {
        return this.assists;
    }

    public void setAssists(Integer assists) {
        this.assists = assists;
    }

    public Float getPlayTimeInMinutes() {
        return this.playTimeInMinutes;
    }

    public void setPlayTimeInMinutes(Float playTimeInMinutes) {
        this.playTimeInMinutes = playTimeInMinutes;
    }

    public Game getGame() {
        return this.game;
    }

    public void setGame(Game game) {
        this.game = game;
    }

    public Player getPlayer() {
        return this.player;
    }

    public void setPlayer(Player player) {
        this.player = player;
    }
}
