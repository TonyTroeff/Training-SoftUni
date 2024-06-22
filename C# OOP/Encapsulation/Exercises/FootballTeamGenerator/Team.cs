namespace FootballTeamGenerator;

using System;
using System.Collections.Generic;
using System.Linq;

public class Team
{
    private readonly Dictionary<string, Player> _players = new();

    public Team(string name)
    {
        if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("A name should not be empty.");
        this.Name = name;
    }
    
    public string Name { get; }
    public int Rating => this.CalculateRating();

    public bool AddPlayer(Player player) => this._players.TryAdd(player.Name, player);

    public bool RemovePlayer(string playerName) => this._players.Remove(playerName);

    private int CalculateRating()
    {
        if (this._players.Count == 0) return 0;
        return (int) Math.Round(this._players.Average(x => x.Value.SkillLevel));
    }
}