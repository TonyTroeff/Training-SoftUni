int maxBadGradesCount = int.Parse(Console.ReadLine());

int currentBadGradesCount = 0, sum = 0, count = 0;
bool isSuccessful = true;

string input = Console.ReadLine(), lastProblem = string.Empty;
while (input != "Enough")
{
    int grade = int.Parse(Console.ReadLine());

    sum += grade;
    count++;
    lastProblem = input;

    if (grade <= 4)
    {
        currentBadGradesCount++;
        if (currentBadGradesCount == maxBadGradesCount)
        {
            isSuccessful = false;
            break;
        }
    }

    // NOTE: The previous if statement is equivalent to:
    // if (grade <= 4 && ++currentBadGradesCount == maxBadGradesCount) {
    // 	  isSuccessful = false;
    //	  break;
    // }

    input = Console.ReadLine();
}

if (isSuccessful)
{
    Console.WriteLine($"Average score: {1.0 * sum / count:f2}");
    Console.WriteLine($"Number of problems: {count}");
    Console.WriteLine($"Last problem: {lastProblem}");
}
else
{
    Console.WriteLine($"You need a break, {currentBadGradesCount} poor grades.");
}