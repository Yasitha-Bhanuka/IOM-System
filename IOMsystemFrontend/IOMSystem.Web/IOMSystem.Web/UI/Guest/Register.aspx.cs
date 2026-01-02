using System;
using System.Web.UI.WebControls;
using InventoryManagementSystem.BL;

namespace InventoryManagementSystem.UI.Guest
{
    public partial class Register : System.Web.UI.Page
    {
        private RegistrationService registrationService;
        private BranchService branchService;
        private UserService userService;

        protected void Page_Load(object sender, EventArgs e)
        {
            registrationService = new RegistrationService();
            branchService = new BranchService();
            userService = new UserService();

            if (!IsPostBack)
            {
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

        protected void btnRegister_Click(object sender, EventArgs e)
        {
            try
            {
                string email = txtEmail.Text.Trim();
                string password = txtPassword.Text;
                string branchName = ddlBranch.SelectedValue;
                string fullName = txtFullName.Text.Trim();
                string phoneNumber = txtPhoneNumber.Text.Trim();

                // Validate password length
                if (password.Length < 6)
                {
                    ShowMessage("Password must be at least 6 characters long.", "alert-danger");
                    return;
                }

                // Check if user already exists
                if (userService.EmailExists(email))
                {
                    ShowMessage("An account with this email already exists. Please login instead.", "alert-warning");
                    return;
                }

                // Check if pending request exists
                if (registrationService.HasPendingRequest(email))
                {
                    ShowMessage("A registration request with this email is already pending approval.", "alert-info");
                    return;
                }

                // Create registration request
                bool success = registrationService.CreateRegistrationRequest(
                    email, 
                    password, 
                    branchName, 
                    string.IsNullOrEmpty(fullName) ? null : fullName,
                    string.IsNullOrEmpty(phoneNumber) ? null : phoneNumber
                );

                if (success)
                {
                    ShowMessage("Registration request submitted successfully! Please wait for admin approval before logging in.", "alert-success");
                    ClearForm();
                }
                else
                {
                    ShowMessage("Failed to submit registration request. Please try again.", "alert-danger");
                }
            }
            catch (Exception ex)
            {
                ShowMessage("Registration error: " + ex.Message, "alert-danger");
            }
        }

        private void ClearForm()
        {
            txtEmail.Text = "";
            txtPassword.Text = "";
            txtConfirmPassword.Text = "";
            txtFullName.Text = "";
            txtPhoneNumber.Text = "";
            ddlBranch.SelectedIndex = 0;
        }

        private void ShowMessage(string message, string cssClass)
        {
            lblMessage.Text = message;
            pnlMessage.CssClass = "alert " + cssClass;
            pnlMessage.Visible = true;
        }
    }
}
