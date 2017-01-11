using Price_Configurator.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Price_Configurator.ViewModels
{
    public class ProductEquipmentRuleViewModel
    {
        public IEnumerable<ProductEquipmentRule> CurrentProductEquipmentRules { get; set; }

        [Required]
        [Display(Name = "Product*")]
        public int ProductId { get; set; }
        public IEnumerable<Product> Products { get; set; }

        [Required]
        [Display(Name = "Equipment Rule*")]
        public int EquipmentRuleId { get; set; }
        public IEnumerable<EquipmentRule> EquipmentRules { get; set; }
    }
}