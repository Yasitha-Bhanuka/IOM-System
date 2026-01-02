using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IOMSystem.Web.Models
{
    [Table("Roles")]
    public class Role
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RoleId { get; set; }

        [Required]
        [StringLength(50)]
        public string RoleName { get; set; }

        [StringLength(200)]
        public string Description { get; set; }

        public DateTime CreatedDate { get; set; }

        // Navigation property
        public virtual System.Collections.Generic.ICollection<User> Users { get; set; }

        public Role()
        {
            CreatedDate = DateTime.Now;
            Users = new System.Collections.Generic.HashSet<User>();
        }
    }
}
