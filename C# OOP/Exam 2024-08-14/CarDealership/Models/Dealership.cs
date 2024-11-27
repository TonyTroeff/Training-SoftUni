using CarDealership.Models.Contracts;
using CarDealership.Repositories;
using CarDealership.Repositories.Contracts;

namespace CarDealership.Models;

public class Dealership : IDealership
{
    public IRepository<IVehicle> Vehicles { get; } = new VehicleRepository();
    public IRepository<ICustomer> Customers { get; } = new CustomerRepository();
}
