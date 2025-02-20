namespace Farm;

public class StartUp
{
    public static void Main()
    {
        Dog dog = new Dog();
        dog.Eat();
        dog.Bark();

        Puppy puppy = new Puppy();
        puppy.Eat();
        puppy.Bark();
        puppy.Weep();

        Cat cat = new Cat();
        cat.Eat();
        cat.Meow();
    }
}
