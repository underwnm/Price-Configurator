using Price_Configurator.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Price_Configurator.ViewModels
{
    public class ProductViewModel
    {
        public IEnumerable<Product> CurrentProducts { get; set; }

        [Required]
        [Display(Name = "Product Name*")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Product Model")]
        public int ProductModelId { get; set; }
        public IEnumerable<ProductModel> ProductModels { get; set; }
        
    }
}