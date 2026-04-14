using Microsoft.EntityFrameworkCore;
using Crud_App_dotNetCore.Entities;
using Crud_App_dotNetCore.Interfaces.IServices;
//using Crud_App_dotNetApplication.Interfaces.IServices;

namespace Crud_App_dotNetInfrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        private readonly ITenantProvider tenant;


        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, ITenantProvider _tenant) : base(options)
        {
            tenant = _tenant;
        }

        public DbSet<JournalDetail> JournalDetails { get; set; }
        public DbSet<Journal> Journals { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //modelBuilder.Entity<Journal>()
            //    .HasQueryFilter(e => e.AppUserID)
            modelBuilder.Entity<Journal>(entity => entity.HasQueryFilter(e => e.AppUserID == tenant.GetTenantId()));
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

            modelBuilder.Entity<JournalDetail>(entity => entity.HasQueryFilter(e => e.AppUserID == tenant.GetTenantId()));
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

            modelBuilder.Entity<Marrige>(entity => entity.HasQueryFilter(e => e.AppUserID == tenant.GetTenantId()));
            modelBuilder.Entity<Marrige>(entity =>
            {
                entity.Property(c => c.Status).IsRequired();
                entity.Property(c => c.Description).HasMaxLength(500);
                entity.HasMany(c => c.Employees);
            });

            modelBuilder.Entity<EmployeeType>(entity => entity.HasQueryFilter(e => e.AppUserID == tenant.GetTenantId()));
            modelBuilder.Entity<EmployeeType>(entity =>
            {
                entity.Property(c => c.Type).IsRequired();
                entity.Property(c => c.Description).HasMaxLength(500);
                entity.HasMany(c => c.Employees);
            });

            modelBuilder.Entity<Designation>(entity => entity.HasQueryFilter(e => e.AppUserID == tenant.GetTenantId()));
            modelBuilder.Entity<Designation>(entity =>
            {
                entity.Property(c => c.DesignationName).IsRequired();
                entity.Property(c => c.Description).HasMaxLength(500);
                entity.HasMany(c => c.Employees);
            });

            modelBuilder.Entity<Employee>(entity => entity.HasQueryFilter(e => e.AppUserID == tenant.GetTenantId()));
            modelBuilder.Entity<Employee>(entity =>
            {
                entity.Property(c => c.FName).IsRequired();
                entity.Property(c => c.LName).IsRequired();
                entity.Property(c => c.Roll).IsRequired();
                entity.Property(c => c.BasicSalary).IsRequired();
                entity.Property(c => c.AllowanceSalary).IsRequired();
                entity.Property(c => c.Description).HasMaxLength(500);
                entity.HasOne(c => c.Marrige).WithMany(c => c.Employees).HasForeignKey(p => p.MarrigeID).OnDelete(DeleteBehavior.Restrict);
                entity.HasOne(c => c.Designation).WithMany(c => c.Employees).HasForeignKey(p => p.DesignationID).OnDelete(DeleteBehavior.Restrict);
                entity.HasOne(c => c.EmployeeType).WithMany(c => c.Employees).HasForeignKey(p => p.EmployeeTypeID).OnDelete(DeleteBehavior.Restrict);
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
        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach (var entry in ChangeTracker.Entries<BaseEntity>())
            {
                // INSERT case
                if (entry.State == EntityState.Added)
                {
                    entry.Entity.AppUserID = tenant.GetTenantId();
                    entry.Entity.CreatedAt = DateTime.UtcNow;
                }

                // UPDATE case (security check)
                if (entry.State == EntityState.Modified)
                {
                    if (entry.Entity.AppUserID != tenant.GetTenantId())
                    {
                        throw new UnauthorizedAccessException("Cross-tenant update not allowed");
                    }

                    entry.Entity.UpdatedAt = DateTime.UtcNow;
                }

                // DELETE case = soft delete
                if (entry.State == EntityState.Deleted)
                {
                    if (entry.Entity.AppUserID != tenant.GetTenantId())
                    {
                        throw new UnauthorizedAccessException("Cross-tenant delete not allowed");
                    }

                    entry.State = EntityState.Modified;
                    entry.Entity.IsDeleted = true;
                }
            }

            return await base.SaveChangesAsync(cancellationToken);
        }
    }
}