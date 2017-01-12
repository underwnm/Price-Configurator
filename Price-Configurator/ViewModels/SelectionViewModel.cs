using Price_Configurator.Models;
using System.Collections.Generic;

namespace Price_Configurator.ViewModels
{
    public class SelectionViewModel
    {
        public Product Product { get; set; }
        public List<CheckModel> CheckModels { get; set; }
        public List<CheckModel> RadioList { get; set; }
    }
}