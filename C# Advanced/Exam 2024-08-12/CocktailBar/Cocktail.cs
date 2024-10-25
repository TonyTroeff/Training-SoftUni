using System.Text;

namespace CocktailBar;

public class Cocktail
{
    private List<string> _ingredients;

    public Cocktail(string name, decimal price, double volume, string ingredients)
    {
        this.Name = name;
        this.Price = price;
        this.Volume = volume;
        this._ingredients = ingredients.Split(", ").ToList();
    }

    public string Name { get; }
    public decimal Price { get; }
    public double Volume { get; }
    public List<string> Ingredients { get => this._ingredients; }

    public override string ToString()
    {
        StringBuilder sb = new StringBuilder();
        sb.AppendLine($"{this.Name}, Price: {this.Price:F2} BGN, Volume: {this.Volume} ml");
        sb.Append($"Ingredients: {string.Join(", ", this.Ingredients)}");

        return sb.ToString();
    }
}
