int limit = int.Parse(Console.ReadLine());

int num = 1;
while (num <= limit)
{
    Console.WriteLine(num);
    num = 2 * num + 1;
}