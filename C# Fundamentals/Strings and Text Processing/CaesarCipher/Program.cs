namespace CaesarCipher;

using System.Text;

public class Program
{
    public static void Main(string[] args)
    {
        string text = Console.ReadLine();

        StringBuilder sb = new StringBuilder(capacity: text.Length);
        for (int i = 0; i < text.Length; i++) sb.Append((char)(text[i] + 3));

        Console.WriteLine(sb.ToString());
    }
}