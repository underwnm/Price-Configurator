using Price_Configurator.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Price_Configurator.ViewModels
{
    public class EquipmentGroupViewModel
    {
        public IEnumerable<EquipmentGroup> EquipmentGroups { get; set; }

        [Required]
        [Display(Name = "Description*")]
        public string Description { get; set; }
    }
}