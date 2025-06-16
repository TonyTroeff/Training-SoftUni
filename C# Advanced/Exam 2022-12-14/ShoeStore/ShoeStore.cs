using System.Text;

namespace ShoeStore;

public class ShoeStore
{
    public string Name { get; }
    public int StorageCapacity { get; }
    public List<Shoe> Shoes { get; }

    public ShoeStore(string name, int storageCapacity)
    {
        this.Name = name;
        this.StorageCapacity = storageCapacity;
        this.Shoes = new List<Shoe>();
    }

    public int Count => this.Shoes.Count;

    public string AddShoe(Shoe shoe)
    {
        if (this.Count == this.StorageCapacity) return "No more space in the storage room.";

        this.Shoes.Add(shoe);
        return $"Successfully added {shoe.Type} {shoe.Material} pair of shoes to the store.";
    }

    public int RemoveShoes(string material)
    {
        int count = 0;
        for (int i = this.Count - 1; i >= 0; i--)
        {
            if (this.Shoes[i].Material == material)
            {
                this.Shoes.RemoveAt(i);
                count++;
            }
        }

        return count;
    }

    public List<Shoe> GetShoesByType(string type)
        => this.Shoes.Where(s => StringComparer.OrdinalIgnoreCase.Equals(s.Type, type)).ToList();

    public Shoe GetShoeBySize(double size)
        => this.Shoes.FirstOrDefault(s => s.Size == size);

    public string StockList(double size, string type)
    {
        Shoe[] result = this.Shoes.Where(s => s.Size == size && s.Type == type).ToArray();
        if (result.Length == 0) return "No matches found!";

        StringBuilder sb = new StringBuilder();
        sb.Append($"Stock list for size {size} - {type} shoes:");

        foreach (Shoe shoe in result)
        {
            sb.AppendLine();
            sb.Append(shoe);
        }

        return sb.ToString();
    }
}
