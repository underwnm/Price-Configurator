using Price_Configurator.Models;
using System.Collections.Generic;

namespace Price_Configurator.ViewModels
{
    public class PriceQuoteViewModel
    {
        public decimal TotalPrice { get; set; }

        public Product Product { get; set; }

        public List<CheckModel> SelectedEquipment { get; set; }
    }
}