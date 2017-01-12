using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Price_Configurator.ViewModels
{
    public class RoleViewModel
    {
        public IEnumerable<IdentityRole> Roles { get; set; }

        [Required]
        [Display(Name = "Name*")]
        public string Name { get; set; }
    }
}