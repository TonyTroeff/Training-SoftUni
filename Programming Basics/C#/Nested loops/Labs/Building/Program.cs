int floors = int.Parse(Console.ReadLine());
int rooms = int.Parse(Console.ReadLine());

for (int i = floors; i > 0; i--)
{
    char letter;
    if (i == floors) letter = 'L';
    else if (i % 2 == 0) letter = 'O';
    else letter = 'A';

    for (int j = 0; j < rooms; j++)
    {
        if (j > 0) Console.Write(' ');
        Console.Write($"{letter}{i}{j}");
    }

    Console.WriteLine();
}