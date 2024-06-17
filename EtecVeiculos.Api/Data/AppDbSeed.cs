using EtecVeiculos.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace EtecVeiculos.Api.Data;

public class AppDbSeed
{
    public AppDbSeed(ModelBuilder modelBuilder)
    {   
        #region VehicleType
        List<VehicleType> vehicleTypes = [
            new() {
                Id = 1,
                Name = "Motorcycle"
            },
            new() {
                Id = 2,
                Name = "Car"
            },
            new() {
                Id = 3,
                Name = "Truck"
            },
        ];
        modelBuilder.Entity<VehicleType>().HasData(vehicleTypes);
        #endregion
    }
}
