int number = int.Parse(Console.ReadLine());

bool isValid = number == 0 || (100 <= number && number <= 200);
if (!isValid) Console.WriteLine("invalid");