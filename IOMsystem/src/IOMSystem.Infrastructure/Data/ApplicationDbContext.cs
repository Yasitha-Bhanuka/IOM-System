using Microsoft.EntityFrameworkCore;
using IOMSystem.Domain.Entities;

namespace IOMSystem.Infrastructure.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<Stationary> Stationaries { get; set; }
    public DbSet<Product> Products { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configure unique index on BranchCode (Placeholder if migrated later)
        // modelBuilder.Entity<Branch>().HasIndex(b => b.BranchCode).IsUnique();

        // Products relationship
        modelBuilder.Entity<Product>()
            .HasOne(p => p.Stationary)
            .WithMany()
            .HasForeignKey(p => p.LocationCode)
            .OnDelete(DeleteBehavior.Restrict);

        // Ensure decimal precision for Price
        modelBuilder.Entity<Product>()
            .Property(p => p.Price)
            .HasPrecision(18, 2);
    }
}
