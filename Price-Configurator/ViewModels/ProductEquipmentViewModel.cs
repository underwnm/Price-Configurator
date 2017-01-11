using Price_Configurator.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Price_Configurator.ViewModels
{
    public class ProductEquipmentViewModel
    {
        public IEnumerable<ProductEquipment> CurrentProductEquipments { get; set; }

        [Required]
        [Display(Name = "Product*")]
        public int ProductId { get; set; }
        public IEnumerable<Product> Products { get; set; }

        [Required]
        [Display(Name = "Equipment*")]
        public int EquipmentId { get; set; }
        public IEnumerable<Equipment> Equipments { get; set; }
    }
}