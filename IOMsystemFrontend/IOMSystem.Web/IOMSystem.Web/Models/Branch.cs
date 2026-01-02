using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IOMSystem.Web.Models
{
    [Table("Branches")]
    public class Branch
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BranchId { get; set; }

        [Required]
        [StringLength(100)]
        public string BranchName { get; set; }

        [Required]
        [StringLength(20)]
        public string BranchCode { get; set; }

        [StringLength(200)]
        public string Address { get; set; }

        [StringLength(50)]
        public string City { get; set; }

        [StringLength(50)]
        public string State { get; set; }

        [StringLength(20)]
        public string PhoneNumber { get; set; }

        public bool IsActive { get; set; }

        public DateTime CreatedDate { get; set; }

        public Branch()
        {
            IsActive = true;
            CreatedDate = DateTime.Now;
        }
    }
}
