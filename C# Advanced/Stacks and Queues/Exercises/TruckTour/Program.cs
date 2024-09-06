int n = int.Parse(Console.ReadLine());

Queue<int[]> stations = new Queue<int[]>();
for (int i = 0; i < n; i++)
{
    int[] currentData = Console.ReadLine().Split().Select(int.Parse).ToArray();
    stations.Enqueue(currentData);
}

int ans = -1;

for (int i = 0; i < n; i++)
{
    bool canFinishRoute = true;
    int fuel = 0;

    foreach (int[] station in stations)
    {
        fuel += station[0] - station[1];
        if (fuel < 0)
        {
            canFinishRoute = false;
            break;
        }
    }

    if (canFinishRoute)
    {
        ans = i;
        break;
    }

    stations.Enqueue(stations.Dequeue());
}

Console.WriteLine(ans);