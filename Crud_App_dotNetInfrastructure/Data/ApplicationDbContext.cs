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
        public DbSet<Brand> Brands { get; set; }
        public DbSet<User> Users { get; set; }


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
                entity.Property(p => p.ProductName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(p => p.ProductDescription)
                    .HasMaxLength(500);

                entity.HasIndex(p => p.CategoryId);
                entity.HasIndex(p => p.BrandId);

                // Configure soft delete query filter
                //entity.HasQueryFilter(p => !p.IsDeleted);
            });
            modelBuilder.Entity<Brand>(entity =>
            {
                entity.Property(b => b.BrandName)
                      .IsRequired()
                      .HasMaxLength(100);

                entity.Property(b => b.Discription)
                      .HasMaxLength(500);

            });
            modelBuilder.Entity<Product>()
                    .HasOne(p => p.Brand)
                    .WithMany(b => b.Products)
                    .HasForeignKey(p => p.BrandId)
                    .OnDelete(DeleteBehavior.Restrict);

            // User configuration
            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(u => u.Username)
                      .IsRequired()
                      .HasMaxLength(100);
                entity.HasIndex(u => u.Username)
                      .IsUnique();

                entity.Property(u => u.Role)
                      .IsRequired()
                      .HasMaxLength(50);

                entity.Property(u => u.Email)
                      .HasMaxLength(150);
            });
        }
    }
}