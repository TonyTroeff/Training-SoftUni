double age = double.Parse(Console.ReadLine());
string gender = Console.ReadLine();

string title = string.Empty;
if (gender == "m")
{
    if (age < 16) title = "Master";
    else title = "Mr.";
}
else if (gender == "f")
{
    if (age < 16) title = "Miss";
    else title = "Ms.";
}

Console.WriteLine(title);