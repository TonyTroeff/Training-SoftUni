package exercise_6.entities;

import jakarta.persistence.*;

import java.math.BigDecimal;
import java.time.LocalDateTime;

@Entity
@Table(name = "bets")
public class Bet extends BaseEntity {
    @Basic
    @Column(name = "amount", nullable = false)
    private BigDecimal amount;

    @Basic
    @Column(name = "time", nullable = false)
    private LocalDateTime time;

    @ManyToOne(optional = false)
    @JoinColumn(name = "game_id", referencedColumnName = "id", nullable = false)
    private Game game;

    @ManyToOne(optional = false)
    @JoinColumn(name = "bettor_id", referencedColumnName = "id", nullable = false)
    private Bettor bettor;

    public BigDecimal getAmount() {
        return this.amount;
    }

    public void setAmount(BigDecimal amount) {
        this.amount = amount;
    }

    public LocalDateTime getTime() {
        return this.time;
    }

    public void setTime(LocalDateTime time) {
        this.time = time;
    }

    public Game getGame() {
        return this.game;
    }

    public void setGame(Game game) {
        this.game = game;
    }

    public Bettor getBettor() {
        return this.bettor;
    }

    public void setBettor(Bettor bettor) {
        this.bettor = bettor;
    }
}
