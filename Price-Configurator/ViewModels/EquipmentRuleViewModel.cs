using Price_Configurator.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Price_Configurator.ViewModels
{
    public class EquipmentRuleViewModel
    {
        public IEnumerable<EquipmentRule> CurrentEquipmentRules { get; set; }

        [Required]
        [Display(Name = "Rule Name*")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Parent Equipment*")]
        public int ParentEquipmentId { get; set; }
        public IEnumerable<Equipment> ParentEquipment { get; set; }

        [Required]
        [Display(Name = "Child Equipment*")]
        public int ChildEquipmentId { get; set; }
        public IEnumerable<Equipment> ChildEquipment { get; set; }
    }
}