int age = int.Parse(Console.ReadLine());
double washingMachinePrice = double.Parse(Console.ReadLine());
int toyPrice = int.Parse(Console.ReadLine());

int savedMoney = 0, moneyToReceive = 10;
for (int i = 1; i <= age; i++)
{
    if (i % 2 == 0)
    {
        savedMoney += moneyToReceive - 1;
        moneyToReceive += 10;
    }
    else { savedMoney += toyPrice; }
}

if (savedMoney >= washingMachinePrice) Console.WriteLine($"Yes! {savedMoney - washingMachinePrice:f2}");
else Console.WriteLine($"No! {washingMachinePrice - savedMoney:f2}");