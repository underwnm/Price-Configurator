using Price_Configurator.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Price_Configurator.ViewModels
{
    public class EquipmentViewModel
    {
        public IEnumerable<Equipment> CurrentEquipment { get; set; }

        [Required]
        [Display(Name = "Name*")]
        public string Name { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }

        [Required]
        [Display(Name = "List Price*")]
        public decimal ListPrice { get; set; }

        [Required]
        [Display(Name = "Equipment Type*")]
        public int EquipmentTypeId { get; set; }
        public IEnumerable<EquipmentType> EquipmentTypes { get; set; }

        [Display(Name = "Picture Url")]
        public string PictureUrl { get; set; }
    }
}