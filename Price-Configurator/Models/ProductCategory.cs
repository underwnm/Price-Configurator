using System.ComponentModel.DataAnnotations;

namespace Price_Configurator.Models
{
    public class ProductCategory
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
    }
}