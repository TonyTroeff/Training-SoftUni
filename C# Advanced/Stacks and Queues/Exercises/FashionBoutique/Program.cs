Stack<int> boxesOfClothes = new Stack<int>(Console.ReadLine().Split().Select(int.Parse));
int rackCapacity = int.Parse(Console.ReadLine());

int remainingCapacity = rackCapacity;
int racks = 1;

while (boxesOfClothes.Count > 0)
{
    int clothes = boxesOfClothes.Pop();

    if (clothes > remainingCapacity)
    {
        remainingCapacity = rackCapacity;
        racks++;
    }

    remainingCapacity -= clothes;
}

Console.WriteLine(racks);