int primeSum = 0, nonPrimeSum = 0;

string command = Console.ReadLine();
while (command != "stop")
{
    int number = int.Parse(command);

    if (number < 0) Console.WriteLine("Number is negative.");
    else
    {
        bool isPrime = true;
        double numSqrt = Math.Sqrt(number);
        for (int i = 2; i <= numSqrt; i++)
        {
            if (number % i == 0)
            {
                isPrime = false;
                break;
            }
        }

        if (isPrime) primeSum += number;
        else nonPrimeSum += number;
    }

    command = Console.ReadLine();
}

Console.WriteLine($"Sum of all prime numbers is: {primeSum}");
Console.WriteLine($"Sum of all non prime numbers is: {nonPrimeSum}");