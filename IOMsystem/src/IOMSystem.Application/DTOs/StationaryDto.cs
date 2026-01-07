using System;
using System.Collections.Generic;
using System.Text;

namespace IOMSystem.Application.DTOs
{
    public class StationaryDto
    {
        public string LocationCode { get; set; }
        public string BranchCode { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
