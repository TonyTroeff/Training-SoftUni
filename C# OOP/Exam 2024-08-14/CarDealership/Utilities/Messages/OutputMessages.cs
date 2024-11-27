namespace CarDealership.Utilities.Messages
{
    public class OutputMessages
    {
        //Common
        public const string InvalidType = "{0} is not a valid type.";

        //AddCustomer
        public const string CustomerAlreadyAdded = "{0} already exists as a profile in the dealership.";
        public const string CustomerAddedSuccessfully = "{0} created a profile in the dealership.";

        //AddVehicle
        public const string VehicleAlreadyAdded = "{0} already exists as an offer in the dealership.";
        public const string VehicleAddedSuccessfully = "{0}: {1} is listed in the dealership. Price: {2:f2}";

        //PurchaseVehicle
        public const string CustomerNotFound = "{0} has no profile in the dealership.";
        public const string VehicleTypeNotFound = "{0} is not listed for sale in the dealership.";
        public const string CustomerNotEligibleToPurchaseVehicle = "{0} is not eligible to purchase a {1}.";
        public const string BudgetIsNotEnough = "{0} does not have enough budget to purchase {1}.";
        public const string VehiclePurchasedSuccessfully = "{0} purchased a {1}.";
    }
}
