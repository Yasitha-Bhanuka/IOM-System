using System;
using System.Web.UI.WebControls;
using InventoryManagementSystem.BL;

namespace InventoryManagementSystem.UI.Guest
{
    public partial class Login : System.Web.UI.Page
    {
        private AuthenticationService authService;
        private BranchService branchService;

        protected void Page_Load(object sender, EventArgs e)
        {
            authService = new AuthenticationService();
            branchService = new BranchService();

            if (!IsPostBack)
            {
                // Check if user is already logged in
                if (Request.IsAuthenticated)
                {
                    RedirectToDashboard();
                }

                LoadBranches();
            }
        }

        private void LoadBranches()
        {
            try
            {
                var branches = branchService.GetActiveBranches();
                ddlBranch.Items.Clear();
                ddlBranch.Items.Add(new ListItem("-- Select Branch --", ""));

                foreach (var branch in branches)
                {
                    ddlBranch.Items.Add(new ListItem(branch.BranchName, branch.BranchName));
                }
            }
            catch (Exception ex)
            {
                ShowMessage("Error loading branches: " + ex.Message, "alert-danger");
            }
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                string email = txtEmail.Text.Trim();
                string branchName = ddlBranch.SelectedValue;
                string password = txtPassword.Text;

                // Validate login
                var user = authService.ValidateLogin(email, branchName, password);

                if (user != null)
                {
                    // Create authentication ticket
                    authService.CreateAuthenticationTicket(user, false);

                    // Redirect to appropriate dashboard
                    RedirectToDashboard();
                }
                else
                {
                    ShowMessage("Invalid email, branch, or password. Please try again.", "alert-danger");
                }
            }
            catch (Exception ex)
            {
                ShowMessage("Login error: " + ex.Message, "alert-danger");
            }
        }

        private void RedirectToDashboard()
        {
            var user = authService.GetCurrentUser();
            if (user != null)
            {
                if (user.Role.RoleName == "Admin")
                {
                    Response.Redirect("~/UI/Admin/Dashboard.aspx", false);
                }
                else if (user.Role.RoleName == "Customer")
                {
                    Response.Redirect("~/UI/Customer/Dashboard.aspx", false);
                }
                else
                {
                    Response.Redirect("~/Default.aspx", false);
                }
            }
            else
            {
                Response.Redirect("~/Default.aspx", false);
            }
        }

        private void ShowMessage(string message, string cssClass)
        {
            lblMessage.Text = message;
            pnlMessage.CssClass = "alert " + cssClass;
            pnlMessage.Visible = true;
        }
    }
}
