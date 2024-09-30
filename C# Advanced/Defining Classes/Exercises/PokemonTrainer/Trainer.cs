namespace PokemonTrainer;

public class Trainer
{
    public Trainer(string name)
    {
        this.Name = name;
    }

    public string Name { get; }
    public int Badges { get; set; }
    public List<Pokemon> Pokemons { get; } = new List<Pokemon>();
}
