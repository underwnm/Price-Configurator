using Price_Configurator.Models;
using System.Collections.Generic;

namespace Price_Configurator.ViewModels.Manage
{
    public class NavBarViewModel
    {
        public IEnumerable<ProductCategory> ProductCategories { get; set; }
        public IEnumerable<ProductModel> ProductModels { get; set; }
    }
}