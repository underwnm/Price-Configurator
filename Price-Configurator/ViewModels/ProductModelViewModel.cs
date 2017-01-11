using Price_Configurator.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Price_Configurator.ViewModels
{
    public class ProductModelViewModel
    {
        public IEnumerable<ProductModel> ProductModels { get; set; }

        [Required]
        [Display(Name = "Model*")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Category*")]
        public int ProductCategoryId { get; set; }
        public IEnumerable<ProductCategory> ProductCategories { get; set; }
    }
}