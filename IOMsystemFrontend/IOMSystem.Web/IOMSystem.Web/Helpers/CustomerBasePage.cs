using System;

namespace IOMSystem.Web.Helpers
{
    public class CustomerBasePage : BasePage
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            
            // Check if user is customer or admin (admin can access customer pages)
            if (!IsInRole("Customer") && !IsInRole("Admin"))
            {
                RedirectToAccessDenied();
            }
        }
    }
}
