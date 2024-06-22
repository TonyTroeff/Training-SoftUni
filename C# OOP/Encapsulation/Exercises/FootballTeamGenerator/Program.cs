namespace FootballTeamGenerator;

using System;
using System.Collections.Generic;

public static class Program
{
    static void Main(string[] args)
    {
        var teams = new Dictionary<string, Team>();

        var input = Console.ReadLine();
        while (input != "END")
        {
            var data = input.Split(';');

            var command = data[0];
            var teamName = data[1];

            try
            {
                if (command == "Team") teams[teamName] = new Team(teamName);
                else if (!teams.TryGetValue(teamName, out var team)) Console.WriteLine($"Team {teamName} does not exist.");
                else if (command == "Rating") Console.WriteLine($"{team.Name} - {team.Rating}");
                else if (command == "Add")
                {
                    var player = new Player(data[2], int.Parse(data[3]), int.Parse(data[4]), int.Parse(data[5]), int.Parse(data[6]), int.Parse(data[7]));
                    team.AddPlayer(player);
                }
                else if (command == "Remove")
                {
                    var playerName = data[2];
                    if (!team.RemovePlayer(playerName)) Console.WriteLine($"Player {playerName} is not in {team.Name} team.");
                }
            }
            catch (ArgumentException e)
            {
                Console.WriteLine(e.Message);
            }

            input = Console.ReadLine();
        }
    }
}