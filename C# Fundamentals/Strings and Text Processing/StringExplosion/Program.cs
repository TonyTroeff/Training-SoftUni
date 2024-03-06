namespace StringExplosion;

using System.Text;

public class Program
{
    public static void Main(string[] args)
    {
        string text = Console.ReadLine();

        StringBuilder result = new StringBuilder(capacity: text.Length);

        int index = 0;
        int destructionPower = 0;
        while (index < text.Length)
        {
            if (text[index] == '>')
            {
                result.Append(text[index]);

                int currentStrength = 0;
                while (index + 1 < text.Length && char.IsDigit(text[index + 1]))
                {
                    currentStrength = currentStrength * 10 + (text[index + 1] - '0') - 1; // We need to "delete" each digit from the original text. That's why we subtract one for each digit.
                    index++;
                }

                destructionPower += currentStrength;
            }
            else
            {
                if (destructionPower > 0) destructionPower--;
                else result.Append(text[index]);
            }

            index++;
        }

        Console.WriteLine(result.ToString());
    }
}