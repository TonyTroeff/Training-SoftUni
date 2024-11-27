using CarDealership.Repositories.Contracts;

namespace CarDealership.Models.Contracts
{
    public interface IDealership
    {
        IRepository<IVehicle> Vehicles { get; }

        IRepository<ICustomer> Customers { get; }
    }
}
