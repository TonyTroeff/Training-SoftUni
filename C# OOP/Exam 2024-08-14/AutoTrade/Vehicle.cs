namespace AutoTrade
{
    public class Vehicle
    {
        public Vehicle(string make, string model, int year)
        {
            Make = make;
            Model = model;
            Year = year;
        }

        public string Make { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }

        public override string ToString()
        {
            return $"{Year} {Make} {Model}";
        }
    }
}
