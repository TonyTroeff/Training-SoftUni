namespace ReplaceRepeatingChars;

using System.Text;

public class Program
{
    public static void Main(string[] args)
    {
        string text = Console.ReadLine();

        StringBuilder result = new StringBuilder(capacity: text.Length);

        int index = 0;
        while (index < text.Length)
        {
            while (index + 1 < text.Length && text[index] == text[index + 1]) index++;

            result.Append(text[index]);
            index++;
        }

        Console.WriteLine(result.ToString());
    }
}