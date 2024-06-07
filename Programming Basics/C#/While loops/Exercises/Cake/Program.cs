int width = int.Parse(Console.ReadLine());
int length = int.Parse(Console.ReadLine());

int totalPieces = width * length;
bool cakeIsEnough = true;

string input = Console.ReadLine();
while (input != "STOP")
{
    int piecesToTake = int.Parse(input);
    totalPieces -= piecesToTake;

    if (totalPieces < 0)
    {
        cakeIsEnough = false;
        break;
    }

    input = Console.ReadLine();
}

if (cakeIsEnough) Console.WriteLine($"{totalPieces} pieces are left.");
else Console.WriteLine($"No more cake left! You need {Math.Abs(totalPieces)} pieces more.");