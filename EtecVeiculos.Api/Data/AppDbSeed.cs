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


        #region Brands
        List<Brand> brands = [
            new() {
                Id = 1,
                Name = "Lamborghini"
            },
            new() {
                Id = 2,
                Name = "Ferrari"
            },
            new() {
                Id = 3,
                Name = "Subaru"
            },
        ];
        modelBuilder.Entity<Brand>().HasData(brands);
        #endregion


        #region VehicleModel
        List<VehicleModel> vehicleModels = [
            new() {
                Id = 1,
                Name = "Lamborghini Huracan",
                BrandId = 1
            },
            new() {
                Id = 2,
                Name = "Lamborghini Urus",
                BrandId = 1
            },
            new() {
                Id = 3,
                Name = "Ferrari 296 GTB",
                BrandId = 2
            },
             new() {
                Id = 4,
                Name = "Ferrari 296 GTS",
                BrandId = 2
            },
             new() {
                Id = 5,
                Name = "Subaru Impreza",
                BrandId = 3
            },
             new() {
                Id = 6,
                Name = "Subaru Outback",
                BrandId = 3
            },
        ];
        modelBuilder.Entity<VehicleModel>().HasData(vehicleModels);
        #endregion
    }
}
