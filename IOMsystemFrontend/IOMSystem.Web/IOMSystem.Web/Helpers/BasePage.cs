using System;
using System.Web.UI;
using InventoryManagementSystem.BL;
using InventoryManagementSystem.Models;

namespace InventoryManagementSystem.Helpers
{
    public class BasePage : Page
    {
        protected AuthenticationService AuthService;
        protected User CurrentUser;
        protected string CurrentRole;

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            AuthService = new AuthenticationService();
            
            // Check if user is authenticated
            if (!Request.IsAuthenticated)
            {
                RedirectToLogin();
            }
            else
            {
                LoadCurrentUser();
            }
        }

        protected void LoadCurrentUser()
        {
            CurrentUser = AuthService.GetCurrentUser();
            if (CurrentUser == null)
            {
                RedirectToLogin();
            }
            else
            {
                CurrentRole = CurrentUser.Role.RoleName;
            }
        }

        protected void RedirectToLogin()
        {
            Response.Redirect("~/UI/Guest/Login.aspx", true);
        }

        protected void RedirectToAccessDenied()
        {
            Response.Redirect("~/UI/Guest/AccessDenied.aspx", true);
        }

        protected bool IsInRole(string roleName)
        {
            return CurrentRole != null && CurrentRole.Equals(roleName, StringComparison.OrdinalIgnoreCase);
        }

        protected void ShowMessage(string message, string cssClass = "alert-info")
        {
            // This can be overridden in derived classes to show messages
            // For now, we'll use Session to pass messages
            Session["Message"] = message;
            Session["MessageClass"] = cssClass;
        }
    }
}
