using System;
using System.Web.UI;
using IOMSystem.Contracts.DTOs;

namespace WebformsUI.Pages.Dashboard
{
    public partial class DashboardDefault : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Verification of Contracts reference
            var _ = new BranchResponseDto();
        }
    }
}
