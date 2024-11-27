using CarDealership.Models.Contracts;

namespace CarDealership.Repositories;

public class VehicleRepository : BaseRepository<IVehicle>
{
    public override void Add(IVehicle vehicle)
        => this.ModelsByUniqueId[vehicle.Model] = vehicle;
}
