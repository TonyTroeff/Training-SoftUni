namespace Template;

public class WholeWheat : Bread
{
    protected override string Bake() => "Baking the whole wheat bread. (15 minutes)";

    protected override string MixIngredients() => "Mixing ingredients for whole wheat bread.";

    protected override string Slice() => $"{base.Slice()} It will be delicious!";
}