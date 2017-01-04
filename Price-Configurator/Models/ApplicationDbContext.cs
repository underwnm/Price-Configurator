using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;

namespace Price_Configurator.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<EquipmentGroup> EquipmentGroups { get; set; }
        public DbSet<EquipmentType> EquipmentTypes { get; set; }
        public DbSet<ListPrice> ListPrices { get; set; }
        public DbSet<Equipment> Equipments { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }
        public DbSet<ProductModel> ProductModels { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductEquipment> ProductsEquipment { get; set; }

        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProductEquipment>()
                .HasRequired(p => p.Product)
                .WithMany()
                .WillCascadeOnDelete(false);

            base.OnModelCreating(modelBuilder);
        }
    }
}