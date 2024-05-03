int start = int.Parse(Console.ReadLine());
int end = int.Parse(Console.ReadLine());
int magicNumber = int.Parse(Console.ReadLine());

bool isFound = false;
int order = 0;
for (int i = start; i <= end; i++)
{
    for (int j = start; j <= end; j++)
    {
        order++;

        if (i + j == magicNumber)
        {
            Console.WriteLine($"Combination N:{order} ({i} + {j} = {magicNumber})");
            isFound = true;
            break;
        }
    }

    if (isFound) break;
}

if (!isFound) Console.WriteLine($"{order} combinations - neither equals {magicNumber}");