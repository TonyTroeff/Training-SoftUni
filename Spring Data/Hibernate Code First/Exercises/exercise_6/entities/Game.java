package exercise_6.entities;

import jakarta.persistence.*;

import java.time.LocalDateTime;
import java.util.HashSet;
import java.util.Set;

@Entity
@Table(name = "games")
public class Game extends BaseEntity {
    @Column(name = "home_goals", nullable = false)
    private Integer homeGoals;

    @Column(name = "away_goals", nullable = false)
    private Integer awayGoals;

    @Column(name = "home_win_bet_rate")
    private Float homeWinBetRate;

    @Column(name = "draw_bet_rate")
    private Float drawBetRate;

    @Column(name = "away_win_bet_rate")
    private Float awayWinBetRate;

    @Column(name = "time")
    private LocalDateTime time;

    @ManyToOne(optional = false)
    @JoinColumn(name = "home_team_id", nullable = false)
    private Team homeTeam;

    @ManyToOne(optional = false)
    @JoinColumn(name = "away_team_id", nullable = false)
    private Team awayTeam;

    @ManyToOne
    @JoinColumn(name = "competition_id")
    private Competition competition;

    @ManyToOne
    @JoinColumn(name = "round_id")
    private Round round;

    @OneToMany(mappedBy = "game")
    private Set<Performance> performances = new HashSet<>();

    @OneToMany(mappedBy = "game")
    private Set<Bet> bets = new HashSet<>();

    public Integer getHomeGoals() {
        return this.homeGoals;
    }

    public void setHomeGoals(Integer homeGoals) {
        this.homeGoals = homeGoals;
    }

    public Integer getAwayGoals() {
        return this.awayGoals;
    }

    public void setAwayGoals(Integer awayGoals) {
        this.awayGoals = awayGoals;
    }

    public Float getHomeWinBetRate() {
        return this.homeWinBetRate;
    }

    public void setHomeWinBetRate(Float homeWinBetRate) {
        this.homeWinBetRate = homeWinBetRate;
    }

    public Float getDrawBetRate() {
        return this.drawBetRate;
    }

    public void setDrawBetRate(Float drawBetRate) {
        this.drawBetRate = drawBetRate;
    }

    public Float getAwayWinBetRate() {
        return this.awayWinBetRate;
    }

    public void setAwayWinBetRate(Float awayWinBetRate) {
        this.awayWinBetRate = awayWinBetRate;
    }

    public LocalDateTime getTime() {
        return this.time;
    }

    public void setTime(LocalDateTime time) {
        this.time = time;
    }

    public Team getHomeTeam() {
        return this.homeTeam;
    }

    public void setHomeTeam(Team homeTeam) {
        this.homeTeam = homeTeam;
    }

    public Team getAwayTeam() {
        return this.awayTeam;
    }

    public void setAwayTeam(Team awayTeam) {
        this.awayTeam = awayTeam;
    }

    public Competition getCompetition() {
        return this.competition;
    }

    public void setCompetition(Competition competition) {
        this.competition = competition;
    }

    public Round getRound() {
        return this.round;
    }

    public void setRound(Round round) {
        this.round = round;
    }

    public Set<Performance> getPerformances() {
        return this.performances;
    }

    public void setPerformances(Set<Performance> performances) {
        this.performances = performances;
    }

    public Set<Bet> getBets() {
        return this.bets;
    }

    public void setBets(Set<Bet> bets) {
        this.bets = bets;
    }
}
