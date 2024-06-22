namespace Animals;

using System;

public static class StartUp
{
    public static void Main()
    {
        var animalType = Console.ReadLine();

        while (animalType != "Beast!")
        {
            var info = Console.ReadLine().Split();

            Animal animal = null;
            bool isValid;
            try
            {
                if (animalType == "Dog") animal = new Dog(info[0], int.Parse(info[1]), info[2]);
                else if (animalType == "Cat") animal = new Cat(info[0], int.Parse(info[1]), info[2]);
                else if (animalType == "Frog") animal = new Frog(info[0], int.Parse(info[1]), info[2]);
                else if (animalType == "Kitten") animal = new Kitten(info[0], int.Parse(info[1]));
                else if (animalType == "Tomcat") animal = new Tomcat(info[0], int.Parse(info[1]));

                isValid = animal is not null;
            }
            catch (ArgumentException e)
            {
                isValid = false;
            }
            
            if (isValid) Console.WriteLine(animal);
            else Console.WriteLine("Invalid input!");
            
            animalType = Console.ReadLine();
        }
    }
}