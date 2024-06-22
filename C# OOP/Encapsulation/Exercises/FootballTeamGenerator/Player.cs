namespace FootballTeamGenerator;

using System;

public class Player
{
    private const int MinStat = 0;
    private const int MaxStat = 100;
    
    public Player(string name, int endurance, int sprint, int dribble, int passing, int shooting)
    {
        if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("A name should not be empty.");
        if (endurance < MinStat || endurance > MaxStat) throw new ArgumentException(PrepareStatExceptionMessage("Endurance"));
        if (sprint < MinStat || sprint > MaxStat) throw new ArgumentException(PrepareStatExceptionMessage("Sprint"));
        if (dribble < MinStat || dribble > MaxStat) throw new ArgumentException(PrepareStatExceptionMessage("Dribble"));
        if (passing < MinStat || passing > MaxStat) throw new ArgumentException(PrepareStatExceptionMessage("Passing"));
        if (shooting < MinStat || shooting > MaxStat) throw new ArgumentException(PrepareStatExceptionMessage("Shooting"));
        
        this.Name = name;
        this.Endurance = endurance;
        this.Sprint = sprint;
        this.Dribble = dribble;
        this.Passing = passing;
        this.Shooting = shooting;
    }

    public string Name { get; }
    public int Endurance { get; }
    public int Sprint { get; }
    public int Dribble { get; }
    public int Passing { get; }
    public int Shooting { get; }
    public double SkillLevel => (this.Endurance + this.Sprint + this.Dribble + this.Passing + this.Shooting) / 5.0;

    private static string PrepareStatExceptionMessage(string stat) => $"{stat} should be between {MinStat} and {MaxStat}.";
}