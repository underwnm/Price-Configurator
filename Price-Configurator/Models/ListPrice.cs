using System.ComponentModel.DataAnnotations;

namespace Price_Configurator.Models
{
    public class ListPrice
    {
        public int Id { get; set; }

        [Required]
        public decimal Price { get; set; }
    }
}