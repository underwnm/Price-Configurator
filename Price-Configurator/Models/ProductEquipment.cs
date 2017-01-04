using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Price_Configurator.Models
{
    public class ProductEquipment
    {
        [Key]
        [Column(Order = 1)]
        public int ProductId { get; set; }
        public Product Product { get; set; }

        [Key]
        [Column(Order = 2)]
        public int EquipmentId { get; set; }
        public Equipment Equipment { get; set; }
    }
}