namespace ValidUsernames;

public class Program
{
    public static void Main(string[] args)
    {
        string[] usernames = Console.ReadLine().Split(", ");

        // First approach:
        for (int i = 0; i < usernames.Length; i++)
            if (UsernameIsValid(usernames[i])) Console.WriteLine(usernames[i]);

        // Second approach (using LINQ only):
        // Console.WriteLine(string.Join(Environment.NewLine, usernames.Where(UsernameIsValid)));
    }

    private static bool UsernameIsValid(string username)
    {
        if (username.Length < 3 || username.Length > 16) return false;

        // First approach:
        for (int i = 0; i < username.Length; i++)
            if (!CharacterIsValid(username[i])) return false;
        
        return true;

        // Second approach (using LINQ only):
        // return username.All(CharacterIsValid);
    }

    private static bool CharacterIsValid(char character)
    {
        return char.IsLetter(character) || char.IsNumber(character) || character == '-' || character == '_';
    }
}