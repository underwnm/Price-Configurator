using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Price_Configurator.Models
{
    public class ProductEquipmentRule
    {
        [Key]
        [Column(Order = 1)]
        public int ProductId { get; set; }
        public Product Product { get; set; }

        [Key]
        [Column(Order = 2)]
        public int EquipmentRuleId { get; set; }
        public EquipmentRule EquipmentRule { get; set; }
    }
}