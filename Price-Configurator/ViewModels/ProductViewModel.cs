﻿using Price_Configurator.Models;
using System.Collections.Generic;

namespace Price_Configurator.ViewModels
{
    public class ProductViewModel
    {
        public IEnumerable<Product> CurrentProducts { get; set; }
    }
}