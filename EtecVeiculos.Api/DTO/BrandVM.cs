using System.ComponentModel.DataAnnotations;

namespace EtecVeiculos.Api.DTO;

    public class BrandVM
    {
        [Required]
        [StringLength(30)]
        [Display (Name = "Nome")]
        public string Name { get; set; }
    }
