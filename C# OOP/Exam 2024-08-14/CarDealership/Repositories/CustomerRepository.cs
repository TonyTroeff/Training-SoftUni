using CarDealership.Models.Contracts;

namespace CarDealership.Repositories;

public class CustomerRepository : BaseRepository<ICustomer>
{
    public override void Add(ICustomer customer)
        => this.ModelsByUniqueId[customer.Name] = customer;
}
