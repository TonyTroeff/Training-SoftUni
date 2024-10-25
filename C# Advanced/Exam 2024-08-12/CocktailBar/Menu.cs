using System.Text;

namespace CocktailBar;

public class Menu
{
    public Menu(int barCapacity)
    {
        this.Cocktails = new List<Cocktail>();
        this.BarCapacity = barCapacity;
    }

    public List<Cocktail> Cocktails { get; }
    public int BarCapacity { get; }

    public void AddCocktail(Cocktail cocktail)
    {
        if (this.Cocktails.Count >= this.BarCapacity || this.Cocktails.Any(c => c.Name == cocktail.Name)) return;

        this.Cocktails.Add(cocktail);
    }

    public bool RemoveCocktail(string name)
    {
        // return this.Cocktails.RemoveAll(c => c.Name == name) > 0;

        Cocktail? cocktail = this.Cocktails.SingleOrDefault(c => c.Name == name);
        if (cocktail is null) return false;

        return this.Cocktails.Remove(cocktail);
    }

    public Cocktail? GetMostDiverse()
    {
        return this.Cocktails.MaxBy(c => c.Ingredients.Count);
    }

    public string Details(string name)
    {
        return this.Cocktails.Single(c => c.Name == name).ToString();
    }

    public string GetAll()
    {
        StringBuilder sb = new StringBuilder();

        sb.AppendLine("All Cocktails:");

        bool isFirst = true;
        foreach (Cocktail cocktail in this.Cocktails.OrderBy(c => c.Name))
        {
            if (isFirst) isFirst = false;
            else sb.AppendLine();
            
            sb.Append(cocktail.Name);
        }

        return sb.ToString();
    }
}
