int width = int.Parse(Console.ReadLine());
int height = int.Parse(Console.ReadLine());
int depth = int.Parse(Console.ReadLine());

int freeSpace = width * height * depth;
bool spaceIsEnough = true;

string input = Console.ReadLine();
while (input != "Done")
{
    int spaceToTake = int.Parse(input);
    freeSpace -= spaceToTake;

    if (freeSpace < 0)
    {
        spaceIsEnough = false;
        break;
    }

    input = Console.ReadLine();
}

if (spaceIsEnough) Console.WriteLine($"{freeSpace} Cubic meters left.");
else Console.WriteLine($"No more free space! You need {freeSpace * -1} Cubic meters more.");