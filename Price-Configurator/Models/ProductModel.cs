using System.ComponentModel.DataAnnotations;

namespace Price_Configurator.Models
{
    public class ProductModel
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public int ProductCategoryId { get; set; }
        public ProductCategory ProductCategory { get; set; }
    }
}