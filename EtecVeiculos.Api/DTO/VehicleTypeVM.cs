using System.ComponentModel.DataAnnotations;

namespace EtecVeiculos.Api.DTO;

    public class VehicleTypeVM
    {
        [Required]
        [StringLength(30)]
        [Display (Name = "Nome")]
        public string Name { get; set; }
    }
