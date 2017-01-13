using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Price_Configurator.Models
{
    public class PriceQuote
    {
        public int Id { get; set; }

        [Required]
        public string ApplicationUserId { get; set; }

        public List<CheckModel> SelectedEquipment { get; set; }

        [Required]
        public int ProductId { get; set; }
        public Product Product { get; set; }

        public decimal TotalPrice { get; set; }

        [Required]
        public DateTime DateCreated { get; set; }
    }
}