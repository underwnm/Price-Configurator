using System.ComponentModel.DataAnnotations;

namespace Price_Configurator.Models
{
    public class Product
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public int ProductModelId { get; set; }
        public ProductModel ProductModel { get; set; }

        [Required]
        public decimal ListPrice { get; set; }

        public string PictureUrl { get; set; }
    }
}