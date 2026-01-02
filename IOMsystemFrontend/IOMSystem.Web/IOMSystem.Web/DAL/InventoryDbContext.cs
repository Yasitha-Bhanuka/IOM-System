using System.Data.Entity;
using InventoryManagementSystem.Models;

namespace InventoryManagementSystem.DAL
{
    public class InventoryDbContext : DbContext
    {
        public InventoryDbContext() : base("name=InventoryDbConnection")
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Branch> Branches { get; set; }
        public DbSet<UserRegistrationRequest> UserRegistrationRequests { get; set; }
        public DbSet<Stationary> Stationaries { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure User entity
            modelBuilder.Entity<User>()
                .HasRequired(u => u.Role)
                .WithMany(r => r.Users)
                .HasForeignKey(u => u.RoleId)
                .WillCascadeOnDelete(false);

            // Configure UserRegistrationRequest entity
            modelBuilder.Entity<UserRegistrationRequest>()
                .HasOptional(r => r.ApprovedByUser)
                .WithMany()
                .HasForeignKey(r => r.ApprovedByUserId)
                .WillCascadeOnDelete(false);

            // Configure unique index on UserEmail
            modelBuilder.Entity<User>()
                .HasIndex(u => u.UserEmail)
                .IsUnique();

            // Configure unique index on BranchCode
            modelBuilder.Entity<Branch>()
                .HasIndex(b => b.BranchCode)
                .IsUnique();
        }
    }
}
