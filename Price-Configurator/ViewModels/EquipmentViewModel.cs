using Price_Configurator.Models;
using System.Collections.Generic;

namespace Price_Configurator.ViewModels
{
    public class EquipmentViewModel
    {
        public IEnumerable<Equipment> CurrentEquipment { get; set; }
    }
}