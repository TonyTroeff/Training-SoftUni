int juries = int.Parse(Console.ReadLine());

double presentationScoresSum = 0;
int presentationsCount = 0;

string presentationName = Console.ReadLine();
while (presentationName != "Finish")
{
    double currentScoresSum = 0;
    for (int i = 0; i < juries; i++)
    {
        double juryScore = double.Parse(Console.ReadLine());
        currentScoresSum += juryScore;
    }

    double grade = currentScoresSum / juries;
    Console.WriteLine($"{presentationName} - {grade:f2}.");

    presentationScoresSum += grade;
    presentationsCount++;

    presentationName = Console.ReadLine();
}

double averageAssessment = presentationScoresSum / presentationsCount;
Console.WriteLine($"Student's final assessment is {averageAssessment:f2}.");