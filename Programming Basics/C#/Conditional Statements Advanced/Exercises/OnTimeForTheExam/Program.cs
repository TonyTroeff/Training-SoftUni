int examHour = int.Parse(Console.ReadLine());
int examMinutes = int.Parse(Console.ReadLine());
int arrivalHour = int.Parse(Console.ReadLine());
int arrivalMinutes = int.Parse(Console.ReadLine());

int normalizedExamTime = examHour * 60 + examMinutes, normalizedArrivalTime = arrivalHour * 60 + arrivalMinutes;

// Positive values => we have arrived early
// Negative values => we are late
int diffInMinutes = normalizedExamTime - normalizedArrivalTime;

if (diffInMinutes > 30) Console.WriteLine("Early");
else if (diffInMinutes >= 0) Console.WriteLine("On time");
else Console.WriteLine("Late");

if (diffInMinutes != 0)
{
    int absoluteDiffInMinutes = Math.Abs(diffInMinutes);
    if (absoluteDiffInMinutes < 60) Console.Write($"{absoluteDiffInMinutes} minutes");
    else
    {
        int formattedHours = absoluteDiffInMinutes / 60;
        int formattedMinutes = absoluteDiffInMinutes % 60;

        // NOTE: The D2 format identifier will force padding with zeros.
        Console.Write($"{formattedHours}:{formattedMinutes:d2} hours");
    }

    if (diffInMinutes > 0) Console.Write(" before");
    else Console.Write(" after");

    Console.WriteLine(" the start");
}