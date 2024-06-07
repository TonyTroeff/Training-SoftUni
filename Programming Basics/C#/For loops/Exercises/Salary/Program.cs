int n = int.Parse(Console.ReadLine());
int salary = int.Parse(Console.ReadLine());

for (int i = 0; i < n && salary > 0; i++)
{
    string tab = Console.ReadLine();

    if (tab == "Facebook") salary -= 150;
    else if (tab == "Instagram") salary -= 100;
    else if (tab == "Reddit") salary -= 50;
}

if (salary > 0) Console.WriteLine(salary);
else Console.WriteLine("You have lost your salary.");