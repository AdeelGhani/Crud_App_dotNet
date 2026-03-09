using Microsoft.EntityFrameworkCore;
using Crud_App_dotNetCore.Entities;

namespace Crud_App_dotNetInfrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<JournalDetail> JournalDetails { get; set; }
        public DbSet<Journal> Journals { get; set; }
        public DbSet<User> Users { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Journal>(entity =>
            {
                entity.Property(b => b.VoucherDate)
                      .IsRequired();
                entity.Property(b => b.VoucherType)
                      .IsRequired()
                      .HasMaxLength(2);
                entity.Property(b => b.VoucherRef)
                      .HasMaxLength(8);
                entity.Property(b => b.Narration)
                      .HasMaxLength(100);
                entity.Property(b => b.StoreCode)
                      .HasMaxLength(4);

            });
            modelBuilder.Entity<JournalDetail>()
                    .HasOne(p => p.Journal)
                    .WithMany(b => b.JournalDetails)
                    .HasForeignKey(p => p.JId)
                    .OnDelete(DeleteBehavior.Restrict);

            // Category Configuration
            modelBuilder.Entity<JournalDetail>(entity =>
            {
                // Configure properties
                entity.Property(c => c.Part)
                    .IsRequired();

                entity.Property(c => c.AccCode)
                    .IsRequired()
                    .HasMaxLength(7);
                entity.Property(c => c.Debit)
                    .HasColumnType("decimal(14,2)");
                entity.Property(c => c.Credit)
                    .HasColumnType("decimal(14,2)");
                entity.Property(c => c.LineNarration)
                    .IsRequired()   
                    .HasMaxLength(150);

                // Configure one-to-many relationship with Product
                entity.HasOne(c => c.Journal)
                    .WithMany(p => p.JournalDetails)
                    .HasForeignKey(p => p.JId)
                    .OnDelete(DeleteBehavior.Restrict); // Prevent cascade delete
            });

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