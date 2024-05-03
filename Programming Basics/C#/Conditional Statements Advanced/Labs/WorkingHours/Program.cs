int hour = int.Parse(Console.ReadLine());
string day = Console.ReadLine();

bool isWorkingDay = day == "Monday" || day == "Tuesday" || day == "Wednesday" || day == "Thursday" || day == "Friday" || day == "Saturday";
bool isWorkingHour = 10 <= hour && hour <= 18;

if (isWorkingDay && isWorkingHour) Console.WriteLine("open");
else Console.WriteLine("closed");