using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;

namespace Price_Configurator.ViewModels
{
    public class RoleViewModel
    {
        public IEnumerable<IdentityRole> CurrentRoles { get; set; }
    }
}