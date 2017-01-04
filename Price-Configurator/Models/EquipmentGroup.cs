using System.ComponentModel.DataAnnotations;

namespace Price_Configurator.Models
{
    public class EquipmentGroup
    {
        public int Id { get; set; }

        [Required]
        public string Description { get; set; }
    }
}