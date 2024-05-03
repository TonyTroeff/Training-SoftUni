const string expected = "s3cr3t!P@ssw0rd";

string password = Console.ReadLine();

if (password == expected) Console.WriteLine("Welcome");
else Console.WriteLine("Wrong password!");