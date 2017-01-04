using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;

namespace Price_Configurator.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<EquipmentGroup> EquipmentGroups { get; set; }
        public DbSet<EquipmentType> EquipmentTypes { get; set; }

        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}