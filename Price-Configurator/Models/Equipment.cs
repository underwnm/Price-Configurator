﻿using System.ComponentModel.DataAnnotations;

namespace Price_Configurator.Models
{
    public class Equipment
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        [Required]
        public decimal ListPrice { get; set; }

        [Required]
        public int EquipmentTypeId { get; set; }
        public EquipmentType EquipmentType { get; set; }

        public string PictureUrl { get; set; }
    }
}