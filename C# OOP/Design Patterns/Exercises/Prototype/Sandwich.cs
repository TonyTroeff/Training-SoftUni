namespace Prototype;

using Prototype.Interfaces;

public class Sandwich : ICopyable<Sandwich>
{
    private readonly string _bread;
    private readonly string _meat;
    private readonly string _cheese;
    private readonly string[] _veggies;

    public Sandwich(string bread, string meat, string cheese, params string[] veggies)
    {
        this._bread = bread;
        this._meat = meat;
        this._cheese = cheese;
        this._veggies = veggies;
    }

    public Sandwich DeepCopy()
    {
        var veggiesCopy = new string[this._veggies.Length];
        Array.Copy(this._veggies, veggiesCopy, this._veggies.Length);

        return new Sandwich(this._bread, this._meat, this._cheese, veggiesCopy);
    }

    public Sandwich ShallowCopy() => new(this._bread, this._meat, this._cheese, this._veggies);

    public override string ToString()
    {
        var veggiesInfo = string.Join(", ", this._veggies);
        return $"{this._bread}, {this._meat}, {this._cheese}, {veggiesInfo}";
    }
}