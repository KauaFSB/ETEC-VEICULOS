
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EtecVeiculos.Api.Models;

    public class VehicleModel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(30)]
        [Display (Name = "Nome")]
        public string Name { get; set; }

        [Required]
       public int BrandId { get; set; }
       [ForeignKey("BrandId")]
       public Brand Brand { get; set; }
    }
