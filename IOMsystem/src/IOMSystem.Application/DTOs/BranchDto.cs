using System;
using System.Collections.Generic;
using System.Text;

namespace IOMSystem.Application.DTOs
{
    public class BranchResponseDto
    {
        public string BranchCode { get; set; }
        public string BranchName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string PhoneNumber { get; set; }
        public bool IsActive { get; set; }
    }

    public class CreateBranchDto
    {
        public string BranchName { get; set; }
        public string BranchCode { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string PhoneNumber { get; set; }
        public bool IsActive { get; set; } = true;
    }

    public class UpdateBranchDto
    {
        public string BranchName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string PhoneNumber { get; set; }
        public bool IsActive { get; set; }
    }

    public class BranchStatusDto
    {
        public bool IsActive { get; set; }
    }

}
