using System.Globalization;

namespace DateModifier;

public class DateModifier
{
    public static int CalculateDifferenceInDays(string first, string second)
    {
        DateTime firstDate = DateTime.ParseExact(first, "yyyy MM dd", CultureInfo.InvariantCulture);
        DateTime secondDate = DateTime.ParseExact(second, "yyyy MM dd", CultureInfo.InvariantCulture);

        TimeSpan diff = firstDate - secondDate;
        return (int)Math.Abs(diff.TotalDays);
    }
}
