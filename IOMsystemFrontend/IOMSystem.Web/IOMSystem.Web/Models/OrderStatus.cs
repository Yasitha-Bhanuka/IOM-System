using System.Collections.Generic;
using System.Linq;

namespace IOMSystem.Web.Models
{
    public static class OrderStatus
    {
        public const string Pending = "Pending";
        public const string Approved = "Approved";
        public const string Shipped = "Shipped";
        public const string Cancelled = "Cancelled";

        public static List<string> GetAllStatuses()
        {
            return new List<string> { Pending, Approved, Shipped, Cancelled };
        }

        public static bool IsValidStatus(string status)
        {
            return GetAllStatuses().Contains(status);
        }
    }
}
