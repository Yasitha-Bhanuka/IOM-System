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
    public DbSet<User> Users { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<Branch> Branches { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderItem> OrderItems { get; set; }
    public DbSet<UserRegistrationRequest> UserRegistrationRequests { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configure unique index on BranchCode (Placeholder if migrated later)
        // Configure unique index on BranchCode
        modelBuilder.Entity<Branch>().HasIndex(b => b.BranchCode).IsUnique();

        // User-Role relationship
        modelBuilder.Entity<User>()
            .HasOne(u => u.Role)
            .WithMany(r => r.Users)
            .HasForeignKey(u => u.RoleId)
            .OnDelete(DeleteBehavior.Restrict);

        // Products relationship

        // Products relationship
        // Branch-Stationary relationship (1:N)
        modelBuilder.Entity<Stationary>()
            .HasOne(s => s.Branch)
            .WithMany(b => b.Stationaries)
            .HasForeignKey(s => s.BranchCode)
            .OnDelete(DeleteBehavior.Restrict);

        // Stationary-Product relationship (1:1)
        modelBuilder.Entity<Product>()
            .HasOne(p => p.Stationary)
            .WithOne(s => s.Product)
            .HasForeignKey<Product>(p => p.LocationCode)
            .OnDelete(DeleteBehavior.Restrict);

        // Ensure decimal precision for Price
        modelBuilder.Entity<Product>()
            .Property(p => p.Price)
            .HasPrecision(18, 2);

        // Seed Roles
        modelBuilder.Entity<Role>().HasData(
            new Role { RoleId = 1, RoleName = "Admin", Description = "Administrator", CreatedDate = new DateTime(2024, 1, 1) },
            new Role { RoleId = 2, RoleName = "User", Description = "Standard User", CreatedDate = new DateTime(2024, 1, 1) },
            new Role { RoleId = 3, RoleName = "Manager", Description = "Branch Manager", CreatedDate = new DateTime(2024, 1, 1) }
        );

        // Order precision
        modelBuilder.Entity<Order>()
            .Property(o => o.TotalAmount)
            .HasPrecision(18, 2);

        // OrderItem precision
        modelBuilder.Entity<OrderItem>()
            .Property(oi => oi.Subtotal)
            .HasPrecision(18, 2);

        modelBuilder.Entity<OrderItem>()
            .Property(oi => oi.UnitPrice)
            .HasPrecision(18, 2);
    }
}
