using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace IOMSystem.Domain.Entities;

[Table("Users")]
[Index(nameof(UserEmail), IsUnique = true)]
public class User
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int UserId { get; set; }

    [Required, StringLength(100), EmailAddress]
    public string UserEmail { get; set; } = default!;

    [Required, StringLength(256)]
    public string PasswordHash { get; set; } = default!;

    [Required, StringLength(256)]
    public string PasswordSalt { get; set; } = default!;

    [Required, StringLength(20)]
    public string BranchCode { get; set; } = default!;

    public int RoleId { get; set; }

    public bool IsActive { get; set; } = true;

    public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

    [StringLength(100)]
    public string? FullName { get; set; }

    [StringLength(20)]
    public string? PhoneNumber { get; set; }

    public Role Role { get; set; } = default!;

    public Branch Branch { get; set; } = default!;
}
