namespace PlayersAndMonsters;

public static class StartUp
{
    public static void Main()
    {
        var hero = new Hero("hero", 1);
        var elf = new Elf("elf", 2);
        var museElf = new MuseElf("muse_elf", 3);
        var wizard = new Wizard("wizard", 4);
        var darkWizard = new DarkWizard("dark_wizard", 5);
        var soulMaster = new SoulMaster("soul_master", 6);
        var knight = new Knight("knight", 7);
        var darkKnight = new DarkKnight("dark_knight", 8);
        var bladeKnight = new BladeKnight("blade_knight", 9);
    }
}