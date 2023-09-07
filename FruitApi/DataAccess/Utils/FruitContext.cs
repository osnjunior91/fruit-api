using FruitApi.DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

namespace FruitApi.DataAccess.Utils
{
    public class FruitContext : DbContext
    {
        public FruitContext(DbContextOptions<FruitContext> options) : base(options)
        {
        }
        protected override void OnConfiguring
       (DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase(databaseName: "FruitDb");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Fruit>()
                .HasOne(f => f.FruitType)
                .WithMany(ft => ft.Fruits)
                .HasForeignKey(f => f.FruitTypeId);

            base.OnModelCreating(modelBuilder);
        }

        public void SeedData()
        {
            if (!FruitTypes.Any())
            {
                FruitTypes.AddRange(
                    new FruitType(1, "Citrus Fruits", "Citrus fruits are known for their tangy flavor and high vitamin C content."),
                    new FruitType(2, "Berry Fruits", "Berries are small, often colorful, and packed with antioxidants."),
                    new FruitType(3, "Tropical Fruits", "Tropical fruits typically grow in warm climates and have unique flavors.")
                );
                SaveChanges();
            }
        }

        public DbSet<Fruit> Fruits { get; set; }
        public DbSet<FruitType> FruitTypes { get; set; }
    }
}
