using System;
using InventoryManagementSystem.BL;
using InventoryManagementSystem.Helpers;

namespace InventoryManagementSystem.UI.Customer
{
    public partial class Profile : CustomerBasePage
    {
        private UserService userService;

        protected void Page_Load(object sender, EventArgs e)
        {
            userService = new UserService();

            if (!IsPostBack)
            {
                LoadProfile();
            }
        }

        private void LoadProfile()
        {
            try
            {
                txtEmail.Text = CurrentUser.UserEmail;
                txtFullName.Text = CurrentUser.FullName;
                txtPhoneNumber.Text = CurrentUser.PhoneNumber;
                txtBranch.Text = CurrentUser.BranchName;
            }
            catch (Exception ex)
            {
                ShowMessage("Error loading profile: " + ex.Message, "alert-danger");
            }
        }

        protected void btnUpdateProfile_Click(object sender, EventArgs e)
        {
            try
            {
                CurrentUser.FullName = txtFullName.Text.Trim();
                CurrentUser.PhoneNumber = txtPhoneNumber.Text.Trim();

                bool success = userService.UpdateUser(CurrentUser);
                if (success)
                {
                    ShowMessage("Profile updated successfully!", "alert-success");
                }
                else
                {
                    ShowMessage("Failed to update profile. Please try again.", "alert-danger");
                }
            }
            catch (Exception ex)
            {
                ShowMessage("Error updating profile: " + ex.Message, "alert-danger");
            }
        }

        protected void btnChangePassword_Click(object sender, EventArgs e)
        {
            try
            {
                string newPassword = txtNewPassword.Text;

                if (string.IsNullOrEmpty(newPassword))
                {
                    ShowMessage("Please enter a new password.", "alert-warning");
                    return;
                }

                if (newPassword.Length < 6)
                {
                    ShowMessage("Password must be at least 6 characters long.", "alert-warning");
                    return;
                }

                bool success = userService.ChangePassword(CurrentUser.UserId, newPassword);
                if (success)
                {
                    ShowMessage("Password changed successfully!", "alert-success");
                    txtNewPassword.Text = "";
                    txtConfirmPassword.Text = "";
                }
                else
                {
                    ShowMessage("Failed to change password. Please try again.", "alert-danger");
                }
            }
            catch (Exception ex)
            {
                ShowMessage("Error changing password: " + ex.Message, "alert-danger");
            }
        }

        protected void btnBackToDashboard_Click(object sender, EventArgs e)
        {
            Response.Redirect("Dashboard.aspx", false);
        }

        private void ShowMessage(string message, string cssClass)
        {
            lblMessage.Text = message;
            pnlMessage.CssClass = "alert " + cssClass;
            pnlMessage.Visible = true;
        }
    }
}
