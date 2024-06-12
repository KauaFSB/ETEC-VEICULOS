
using System.ComponentModel.DataAnnotations;

namespace EtecVeiculos.Api.Models;

    public class VehicleType
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(30)]
        [Display (Name = "Nome")]
        public string Name { get; set; }
    }
