using Microsoft.EntityFrameworkCore;
using Crud_App_dotNetCore.Entities;

namespace Crud_App_dotNetInfrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Category Configuration
            modelBuilder.Entity<Category>(entity =>
            {
                // Configure properties
                entity.Property(c => c.CategoryName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(c => c.CategoryDescription)
                    .HasMaxLength(500);

                // Configure one-to-many relationship with Product
                entity.HasMany(c => c.Products)
                    .WithOne(p => p.Category)
                    .HasForeignKey(p => p.CategoryId)
                    .OnDelete(DeleteBehavior.Restrict); // Prevent cascade delete
            });

            // Product Configuration
            modelBuilder.Entity<Product>(entity =>
            {
                // Configure properties
                entity.Property(p => p.ProductName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(p => p.ProductDescription)
                    .HasMaxLength(500);

                // Configure index for CategoryId for better query performance
                entity.HasIndex(p => p.CategoryId);

                // Configure soft delete query filter
                //entity.HasQueryFilter(p => !p.IsDeleted);
            });

         
        }
    }
}