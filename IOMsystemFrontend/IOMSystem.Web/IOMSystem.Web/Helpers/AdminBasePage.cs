using System;

namespace InventoryManagementSystem.Helpers
{
    public class AdminBasePage : BasePage
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            
            // Check if user is admin
            if (!IsInRole("Admin"))
            {
                RedirectToAccessDenied();
            }
        }
    }
}
