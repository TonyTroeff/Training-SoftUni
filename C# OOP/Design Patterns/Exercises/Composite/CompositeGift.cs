namespace Composite;

using Composite.Interfaces;

public class CompositeGift : IGift
{
    private readonly List<IGift> _children = new();

    public CompositeGift(string description)
    {
        this.Description = description;
    }

    public string Description { get; }

    public void AddGift(IGift gift)
    {
        if (gift is null) throw new ArgumentNullException(nameof(gift));

        this._children.Add(gift);
    }

    public int CalculateTotalPrice() => this._children.Sum(x => x.CalculateTotalPrice());

    public override string ToString() => $"{this.Description}({string.Join(", ", this._children)}) -> {this.CalculateTotalPrice()}";
}