using Price_Configurator.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Price_Configurator.ViewModels
{
    public class EquipmentTypeViewModel
    {
        public IEnumerable<EquipmentType> CurrentEquipmentTypes { get; set; }

        [Required]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Display(Name = "Equipment Group")]
        public int? EquipmentGroupId { get; set; }
        public IEnumerable<EquipmentGroup> EquipmentGroups { get; set; }
    }
}