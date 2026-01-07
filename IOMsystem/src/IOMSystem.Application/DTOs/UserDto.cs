using System;
using System.Collections.Generic;
using System.Text;

namespace IOMSystem.Application.DTOs
{
    public class UserDto
    {
        public int UserId { get; set; }
        public string UserEmail { get; set; }
        public string FullName { get; set; }
        public string BranchName { get; set; }
        public string BranchCode { get; set; }
        public string RoleName { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Token { get; set; }
    }

    public class LoginDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

}
