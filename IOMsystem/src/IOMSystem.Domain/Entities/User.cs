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

    [Required]
    [StringLength(100)]
    [EmailAddress]
    public required string UserEmail { get; set; }

    [Required]
    [StringLength(256)]
    public required string PasswordHash { get; set; }

    [Required]
    [StringLength(256)]
    public required string PasswordSalt { get; set; }

    [Required]
    [StringLength(100)]
    public required string BranchName { get; set; }

    [Required]
    public int RoleId { get; set; }

    public bool IsActive { get; set; } = true;

    public DateTime CreatedDate { get; set; } = DateTime.Now;

    public DateTime? LastLoginDate { get; set; }

    [StringLength(100)]
    public string? FullName { get; set; }

    [StringLength(20)]
    public string? PhoneNumber { get; set; }

    // Navigation property
    [ForeignKey("RoleId")]
    public virtual Role? Role { get; set; }
}
