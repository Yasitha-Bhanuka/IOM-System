using System;
using System.Web;
using System.Web.UI;

namespace WebformsUI.Pages.Admin
{
    public partial class Users : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                gvUsers.DataSource = GetUsers(); // your data source
                gvUsers.DataBind();
            }
        }

        protected string GetEditUserScript(
            object userId,
            object email,
            object fullName,
            object branch,
            object role,
            object isActive)
        {
            return string.Format(
                "openEditModal({0}, '{1}', '{2}', '{3}', '{4}', {5});",
                userId,
                JsEncode(email),
                JsEncode(fullName),
                JsEncode(branch),
                JsEncode(role),
                isActive.ToString().ToLower()
            );
        }

        protected string GetDeleteUserScript(object userId)
        {
            return $"deleteUser({userId});";
        }

        private static string JsEncode(object value)
        {
            return HttpUtility.JavaScriptStringEncode(value?.ToString() ?? "");
        }

        private object GetUsers()
        {
            // Replace with real data access
            return new[]
            {
                new {
                    UserId = 1,
                    UserEmail = "test@test.com",
                    FullName = "Test User",
                    BranchName = "HQ",
                    RoleName = "Admin",
                    IsActive = true,
                    CreatedDate = DateTime.Now
                }
            };
        }
    }
}
