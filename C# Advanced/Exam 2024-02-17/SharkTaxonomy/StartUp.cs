namespace SharkTaxonomy;

public class StartUp
{
    static void Main(string[] args)
    {
        //Initialize new repository (Classifier)
        Classifier classifier = new(10);

        //Initialize entities (Shark)
        Shark greatWhite = new("Great White", 5, "Seals", "Open Ocean");
        Shark hammerhead = new("Hammerhead", 4, "Fish", "Tropical Waters");
        Shark tiger = new("Tiger", 4, "Turtles", "Coral Reefs");
        Shark mako = new("Mako", 3, "Fish", "Open Ocean");
        Shark bull = new("Bull", 3, "Fish", "Rivers");
        Shark whale = new("Whale", 12, "Plankton", "Open Ocean");
        Shark leopard = new("Leopard", 1, "Crabs", "Shallow Waters");
        Shark goblin = new("Goblin", 4, "Deep-sea Creatures", "Deep Ocean");
        Shark thresher = new("Thresher", 6, "Fish", "Open Ocean");
        Shark blacktipReef = new("Blacktip Reef", 2, "Fish", "Coral Reefs");
        Shark oceanicWhitetip = new("Oceanic Whitetip", 3, "Fish", "Open Ocean");

        //Add sharks to the repository
        classifier.AddShark(greatWhite);
        classifier.AddShark(hammerhead);
        classifier.AddShark(tiger);
        classifier.AddShark(mako);
        classifier.AddShark(bull);
        classifier.AddShark(whale);
        classifier.AddShark(leopard);
        classifier.AddShark(goblin);
        classifier.AddShark(thresher);
        classifier.AddShark(blacktipReef);

        //Check collection count
        Console.WriteLine(classifier.GetCount);
        //10

        //Attempt to add a shark that will exceed the capacity of the Classifier
        classifier.AddShark(oceanicWhitetip);

        //Check collection count
        Console.WriteLine(classifier.GetCount);
        //10

        //Remove existing shark
        classifier.RemoveShark("Blacktip Reef"); //Returns True

        //Check collection count
        Console.WriteLine(classifier.GetCount);
        //9

        //Try to remove not existing shark
        classifier.RemoveShark("Blue"); //Returns False

        //Check collection count
        Console.WriteLine(classifier.GetCount);
        //9

        //Try to add once again a shark, if there is enough capacity already
        classifier.AddShark(oceanicWhitetip);

        //Check collection count
        Console.WriteLine(classifier.GetCount);
        //10

        //Get the shark that has the greatest body length
        Console.WriteLine(classifier.GetLargestShark());
        //Whale shark: 12m long.
        //Could be spotted in the Open Ocean, typical menu: Plankton

        //Get the average Length of all sharks added to the collection
        Console.WriteLine(classifier.GetAverageLength());
        //4.5

        //Print Sharks Report
        Console.WriteLine(classifier.Report());

        //10 sharks classified:
        //Great White shark: 5m long.
        //Could be spotted in the Open Ocean, typical menu: Seals
        //Hammerhead shark: 4m long.
        //Could be spotted in the Tropical Waters, typical menu: Fish
        //Tiger shark: 4m long.
        //Could be spotted in the Coral Reefs, typical menu: Turtles
        //Mako shark: 3m long.
        //Could be spotted in the Open Ocean, typical menu: Fish
        //Bull shark: 3m long.
        //Could be spotted in the Rivers, typical menu: Fish
        //Whale shark: 12m long.
        //Could be spotted in the Open Ocean, typical menu: Plankton
        //Leopard shark: 1m long.
        //Could be spotted in the Shallow Waters, typical menu: Crabs
        //Goblin shark: 4m long.
        //Could be spotted in the Deep Ocean, typical menu: Deep - sea Creatures
        //Thresher shark: 6m long.
        //Could be spotted in the Open Ocean, typical menu: Fish
        //Oceanic Whitetip shark: 3m long.
        //Could be spotted in the Open Ocean, typical menu: Fish

    }
}

