using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Price_Configurator.Models
{
    public class EquipmentRule
    {

        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [ForeignKey("ParentEquipment")]
        public int ParentEquipmentId { get; set; }
        public Equipment ParentEquipment { get; set; }

        [Required]
        [ForeignKey("ChildEquipment")]
        public int ChildEquipmentId { get; set; }
        public Equipment ChildEquipment { get; set; }
    }
}