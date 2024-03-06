namespace CharacterMultiplier;

public class Program
{
    public static void Main(string[] args)
    {
        string[] input = Console.ReadLine().Split();

        ulong result = MultiplyCharacters(input[0], input[1]);
        Console.WriteLine(result);
    }

    private static ulong MultiplyCharacters(string a, string b)
    {
        int index = 0;
        ulong result = 0;
        while (index < a.Length || index < b.Length)
        {
            uint multiplier1 = 1, multiplier2 = 1;
            if (index < a.Length) multiplier1 = a[index];
            if (index < b.Length) multiplier2 = b[index];

            result += multiplier1 * multiplier2;
            index++;
        }

        return result;
    }
}