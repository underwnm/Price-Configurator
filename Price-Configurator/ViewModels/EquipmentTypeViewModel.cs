using Price_Configurator.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Price_Configurator.ViewModels
{
    public class EquipmentTypeViewModel
    {
        public IEnumerable<EquipmentType> EquipmentTypes { get; set; }

        [Required]
        [Display(Name = "Name*")]
        public string Name { get; set; }

        [Display(Name = "Group")]
        public int? EquipmentGroupId { get; set; }
        public IEnumerable<EquipmentGroup> EquipmentGroups { get; set; }
    }
}