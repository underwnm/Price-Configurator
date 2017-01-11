using Price_Configurator.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Price_Configurator.ViewModels
{
    public class EquipmentGroupViewModel
    {
        public IEnumerable<EquipmentGroup> CurrentEquipmentGroups { get; set; }

        [Required]
        [Display(Name = "Equipment Group Description")]
        public string Description { get; set; }
    }
}