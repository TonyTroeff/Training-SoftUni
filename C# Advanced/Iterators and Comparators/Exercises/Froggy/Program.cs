namespace Froggy;

public class Program
{
    public static void Main()
    {
        int[] stones = Console.ReadLine().Split(", ").Select(int.Parse).ToArray();
        Lake lake = new Lake(stones);

        Console.WriteLine(string.Join(", ", lake));
    }
}
