using System.ComponentModel.DataAnnotations;

namespace Price_Configurator.Models
{
    public class EquipmentType
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public int EquipmentGroupId { get; set; }
        public EquipmentGroup EquipmentGroup { get; set; }
    }
}