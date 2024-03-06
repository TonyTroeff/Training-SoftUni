namespace MultiplyBigNumber;

using System.Text;

public class Program
{
    public static void Main(string[] args)
    {
        string number = Console.ReadLine();
        int multiplier = int.Parse(Console.ReadLine());

        if (multiplier == 0) Console.WriteLine(0);
        else
        {
            StringBuilder result = new StringBuilder(capacity: number.Length + 1);
            int carry = 0;
            for (int i = number.Length - 1; i >= 0; i--)
            {
                int multiplicationResult = (number[i] - '0') * multiplier + carry;

                int lastDigit = multiplicationResult % 10;
                result.Append(lastDigit);

                carry = multiplicationResult / 10;
            }

            if (carry != 0) result.Append(carry);
        
            Reverse(result);

            Console.WriteLine(result.ToString());
        }
    }

    private static void Reverse(StringBuilder sb)
    {
        for (int i = 0; i < sb.Length / 2; i++)
        {
            char swap = sb[i];
            sb[i] = sb[sb.Length - (i + 1)];
            sb[sb.Length - (i + 1)] = swap;
        }
    }
}