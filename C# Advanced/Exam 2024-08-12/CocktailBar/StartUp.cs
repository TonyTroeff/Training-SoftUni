namespace CocktailBar;

public class StartUp
{
    static void Main(string[] args)
    {
        //Initialize the repository (CocktailBar)
        Menu cocktailMenu = new Menu(5);

        //Initialize entity (Cocktail)

        Cocktail mojito = new Cocktail("Mojito", 8.50m, 200, "White Rum, Soda Water, Fresh Mint, Lime, Brown Sugar");

        Cocktail fakeMojito = new Cocktail("Mojito", 18.50m, 180, "Red Rum, Soda Water, Fresh Mint, Lime, Brown Sugar");

        Cocktail pinaColada = new Cocktail("Pina Colada", 7.00m, 150, " White Rum, Coconut Cream, Pineapple Juice");

        Cocktail sexOnTheBeach = new Cocktail("Sex On The Beach", 11.00m, 200, "Vodka, Peach Schnapps, Orange Juice, Cranberry Juice, Glase Cherry");

        Cocktail margarita = new Cocktail("Margarita", 10.50m, 150, " Tequila, Triple Sec, Lime Juice, Salt");

        Cocktail dryMartini = new Cocktail("Dry Martini", 7.50m, 120, "Gin, Vermouth");

        Cocktail longIsland = new Cocktail("Long Island", 13.00m, 300, " Vodka, Tequilla, White Rum, Cointreau, Gin, Lemon Juice, Cola");

        //Adding coctails to the repository
        cocktailMenu.AddCocktail(mojito);
        //The first cocktails is added, 4 more positions available

        //Name duplication is NOT allowed
        cocktailMenu.AddCocktail(fakeMojito);
        //The cocktail should NOT be added, 4 more positions available

        cocktailMenu.AddCocktail(pinaColada);
        cocktailMenu.AddCocktail(sexOnTheBeach);
        cocktailMenu.AddCocktail(margarita);
        cocktailMenu.AddCocktail(dryMartini);
        //The capacity is full after adding dryMartini

        //Try to add cocktail over the allowed capacity
        cocktailMenu.AddCocktail(longIsland);
        //The last cocktail should not be added, because there are no space for it

        //Removing cocktails from the repository
        //Try to remove not existing cocktail, should return False
        cocktailMenu.RemoveCocktail("Long Island");
        //Try to remove existing cocktail, should return True
        cocktailMenu.RemoveCocktail("Pina Colada");

        //Now there is one position availabe, for adding a cocktail, that should not be added previously
        cocktailMenu.AddCocktail(longIsland);

        //Finding the cocktail with the greatest variety of ingredients
        Console.WriteLine(cocktailMenu.GetMostDiverse());
        //Long Island, Price: 13.00 BGN, Volume: 300 ml
        //Ingredients:  Vodka, Tequilla, White Rum, Cointreau, Gin, Lemon Juice, Cola

        //Getting cocktail details
        Console.WriteLine(cocktailMenu.Details("Mojito"));
        //Mojito, Price: 8.50 BGN, Volume: 200 ml
        //Ingredients: White Rum, Soda Water, Fresh Mint, Lime, Brown Sugar

        //Get a list of all cocktails
        Console.WriteLine(cocktailMenu.GetAll());
        // All Cocktails:
        //Dry Martini
        //Long Island
        //Margarita
        //Mojito
        //Sex On The Beach

    }
}
