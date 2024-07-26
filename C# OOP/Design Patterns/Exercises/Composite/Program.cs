namespace Composite;

public static class Program
{
    public static void Main()
    {
        var perfume = new Gift("Perfume", 15);
        var shampoo = new Gift("Shampoo", 10);
        var toy = new Gift("Toy", 5);
        var money = new Gift("Money", 20);

        var cosmeticBox = new CompositeGift("Cosmetic Box");
        cosmeticBox.AddGift(perfume);
        cosmeticBox.AddGift(shampoo);

        var giftsFromRelatives = new CompositeGift("Gifts from relatives");
        giftsFromRelatives.AddGift(cosmeticBox);
        giftsFromRelatives.AddGift(toy);
        giftsFromRelatives.AddGift(money);

        var giftsFromFriends = new CompositeGift("Gifts from friends");

        var allGifts = new CompositeGift("All gifts");
        allGifts.AddGift(giftsFromRelatives);
        allGifts.AddGift(giftsFromFriends);

        Console.WriteLine(allGifts);
    }
}