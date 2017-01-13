using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;

namespace Price_Configurator.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<EquipmentGroup> EquipmentGroups { get; set; }
        public DbSet<EquipmentType> EquipmentTypes { get; set; }
        public DbSet<Equipment> Equipments { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }
        public DbSet<ProductModel> ProductModels { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductEquipment> ProductsEquipment { get; set; }
        public DbSet<EquipmentRule> EquipmentRules { get; set; }
        public DbSet<ProductEquipmentRule> ProductEquipmentRules { get; set; }
        public DbSet<CheckModel> CheckModels { get; set; }
        public DbSet<PriceQuote> PriceQuotes { get; set; }

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

            modelBuilder.Entity<ProductEquipment>()
                .HasRequired(e => e.Equipment)
                .WithMany()
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ProductEquipmentRule>()
                .HasRequired(p => p.Product)
                .WithMany()
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ProductEquipmentRule>()
                .HasRequired(e => e.EquipmentRule)
                .WithMany()
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<EquipmentRule>()
                .HasRequired(p => p.ParentEquipment)
                .WithMany()
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<EquipmentRule>()
                .HasRequired(c => c.ChildEquipment)
                .WithMany()
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Product>()
                .HasRequired(m => m.ProductModel)
                .WithMany()
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ProductModel>()
                .HasRequired(c => c.ProductCategory)
                .WithMany()
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Equipment>()
                .HasRequired(t => t.EquipmentType)
                .WithMany()
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<EquipmentType>()
                .HasOptional(g => g.EquipmentGroup)
                .WithMany()
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<PriceQuote>()
                .HasRequired(x => x.Product)
                .WithMany()
                .WillCascadeOnDelete(false);

            base.OnModelCreating(modelBuilder);
        }
    }
}