namespace PokemonTrainer;

public class Program
{
    public static void Main()
    {
        Dictionary<string, Trainer> trainerByName = new Dictionary<string, Trainer>();

        string input = Console.ReadLine();
        while (input != "Tournament")
        {
            string[] data = input.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            
            string trainerName = data[0];
            if (!trainerByName.ContainsKey(trainerName))
                trainerByName[trainerName] = new Trainer(trainerName);

            Trainer trainer = trainerByName[trainerName];
            Pokemon pokemon = new Pokemon(data[1], data[2], int.Parse(data[3]));

            trainer.Pokemons.Add(pokemon);

            input = Console.ReadLine();
        }

        input = Console.ReadLine();
        while (input != "End")
        {
            foreach (Trainer trainer in trainerByName.Values)
            {
                if (trainer.Pokemons.Any(p => p.Element == input)) trainer.Badges++;
                else
                {
                    for (int i = trainer.Pokemons.Count - 1; i >= 0; i--)
                    {
                        if (trainer.Pokemons[i].Health <= 10) trainer.Pokemons.RemoveAt(i);
                        else trainer.Pokemons[i].Health -= 10;
                    }
                }
            }

            input = Console.ReadLine();
        }

        foreach (Trainer trainer in trainerByName.Values.OrderByDescending(t => t.Badges))
            Console.WriteLine($"{trainer.Name} {trainer.Badges} {trainer.Pokemons.Count}");
    }
}