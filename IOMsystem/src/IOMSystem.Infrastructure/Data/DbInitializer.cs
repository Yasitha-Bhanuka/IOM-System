using System.Security.Cryptography;
using System.Text;
using IOMSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace IOMSystem.Infrastructure.Data;

public static class DbInitializer
{
    public static async Task SeedAsync(ApplicationDbContext context)
    {
        await context.Database.MigrateAsync();


        // Seed Branch if not exists (Admin needs a branch)
        var headOffice = await context.Branches.FirstOrDefaultAsync(b => b.BranchCode == "HO001");
        if (headOffice == null)
        {
            headOffice = new Branch
            {
                BranchCode = "HO001",
                BranchName = "Head Office",
                Address = "123 Main St, Colombo",
                City = "Colombo",
                State = "Western",
                PhoneNumber = "0110000000",
                IsActive = true
            };
            await context.Branches.AddAsync(headOffice);
            await context.SaveChangesAsync();
        }

        if (!await context.Users.AnyAsync(u => u.UserEmail == "admin@iomsystem.com"))
        {
            var adminRole = await context.Roles.FirstOrDefaultAsync(r => r.RoleName == "Admin");
            if (adminRole == null) return;

            CreatePasswordHash("Admin@123", out string passwordHash, out string passwordSalt);

            var adminUser = new User
            {
                UserEmail = "admin@iomsystem.com",
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                FullName = "System Admin",
                BranchCode = headOffice.BranchCode,
                RoleId = adminRole.RoleId,
                IsActive = true,
                CreatedDate = DateTime.Now
            };

            await context.Users.AddAsync(adminUser);
            await context.SaveChangesAsync();
        }
    }

    private static void CreatePasswordHash(string password, out string passwordHash, out string passwordSalt)
    {
        using (var hmac = new HMACSHA256())
        {
            passwordSalt = Convert.ToBase64String(hmac.Key);
            passwordHash = Convert.ToBase64String(hmac.ComputeHash(Encoding.UTF8.GetBytes(password)));
        }
    }
}
