using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IOMSystem.Web.Models
{
    [Table("UserRegistrationRequests")]
    public class UserRegistrationRequest
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RequestId { get; set; }

        [Required]
        [StringLength(100)]
        [EmailAddress]
        public string UserEmail { get; set; }

        [Required]
        [StringLength(256)]
        public string PasswordHash { get; set; }

        [Required]
        [StringLength(256)]
        public string PasswordSalt { get; set; }

        [Required]
        [StringLength(100)]
        public string BranchName { get; set; }

        [StringLength(100)]
        public string FullName { get; set; }

        [StringLength(20)]
        public string PhoneNumber { get; set; }

        public DateTime RequestDate { get; set; }

        [Required]
        [StringLength(20)]
        public string Status { get; set; } // Pending, Approved, Rejected

        public int? ApprovedByUserId { get; set; }

        public DateTime? ApprovedDate { get; set; }

        [StringLength(500)]
        public string RejectionReason { get; set; }

        [StringLength(500)]
        public string Comments { get; set; }

        // Navigation property
        [ForeignKey("ApprovedByUserId")]
        public virtual User ApprovedByUser { get; set; }

        public UserRegistrationRequest()
        {
            RequestDate = DateTime.Now;
            Status = "Pending";
        }
    }
}
