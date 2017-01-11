using Price_Configurator.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Price_Configurator.ViewModels
{
    public class ProductCategoryViewModel
    {
        [Required]
        [Display(Name = "Category Name*")]
        public string Name { get; set; }

        public IEnumerable<ProductCategory> ProductCategories { get; set; }
    }
}