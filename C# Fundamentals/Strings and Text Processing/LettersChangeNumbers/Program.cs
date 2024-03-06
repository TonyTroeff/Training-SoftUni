namespace LettersChangeNumbers;

using System.Text;

public class Program
{
    public static void Main(string[] args)
    {
        string[] data = Console.ReadLine().Split(' ', StringSplitOptions.RemoveEmptyEntries);

        decimal result = 0;
        for (int i = 0; i < data.Length; i++)
        {
            string current = data[i];

            char prefix = current[0], suffix = current[^1];
            decimal number = decimal.Parse(current.Substring(1, current.Length - 2));

            // NOTE: Instead of `if (char.IsUpper(prefix))`, we can use `if ('A' <= prefix && prefix <= 'Z')`.
            if (char.IsUpper(prefix)) number /= prefix - 'A' + 1;
            else number *= prefix - 'a' + 1;

            if (char.IsUpper(suffix)) number -= suffix - 'A' + 1;
            else number += suffix - 'a' + 1;

            result += number;
        }

        Console.WriteLine($"{result:f2}");
    }
}