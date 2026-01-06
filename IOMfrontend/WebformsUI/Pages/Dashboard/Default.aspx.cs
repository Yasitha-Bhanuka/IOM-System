using System;
using System.Web.UI;

namespace WebformsUI.Pages.Dashboard
{
    public partial class Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!User.Identity.IsAuthenticated)
            {
               Response.Redirect("~/Pages/Auth/Login");
            }
        }
    }
}
