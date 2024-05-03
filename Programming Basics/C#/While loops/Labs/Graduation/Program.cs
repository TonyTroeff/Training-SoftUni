string studentName = Console.ReadLine();

int grade = 0;
int fails = 0;
double sum = 0;

while (grade < 12)
{
    double marks = double.Parse(Console.ReadLine());
    if (marks < 4)
    {
        if (fails == 1) break;
        fails++;
    }
    else
    {
        sum += marks;
        grade++;
        fails = 0;
    }
}

if (grade == 12) Console.WriteLine($"{studentName} graduated. Average grade: {sum / 12:f2}");
else Console.WriteLine($"{studentName} has been excluded at {grade + 1} grade");