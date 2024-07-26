namespace Template;

public class Sourdough : Bread
{
    protected override string Bake() => "Baking the sourdough bread. (20 minutes)";

    protected override string MixIngredients() => "Mixing ingredients for sourdough.";
}