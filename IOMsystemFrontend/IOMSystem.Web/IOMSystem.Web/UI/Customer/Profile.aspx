<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Profile.aspx.cs"
    Inherits="InventoryManagementSystem.UI.Customer.Profile" %>

    <!DOCTYPE html>
    <html xmlns="http://www.w3.org/1999/xhtml">

    <head runat="server">
        <title>My Profile</title>
        <link href="../../Content/bootstrap.min.css" rel="stylesheet" />
        <style>
            body {
                background: #f5f7fa;
                font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
            }

            .profile-container {
                padding: 30px;
                max-width: 800px;
                margin: 0 auto;
            }

            .page-header {
                background: linear-gradient(135deg, #667eea 0%, #764ba2 100%);
                color: white;
                padding: 25px;
                border-radius: 10px;
                margin-bottom: 30px;
            }

            .content-card {
                background: white;
                border-radius: 10px;
                padding: 25px;
                box-shadow: 0 2px 10px rgba(0, 0, 0, 0.1);
                margin-bottom: 20px;
            }

            .btn-save {
                background: #28a745;
                color: white;
                border: none;
                padding: 10px 20px;
                border-radius: 5px;
            }

            .btn-back {
                background: #6c757d;
                color: white;
                border: none;
                padding: 10px 20px;
                border-radius: 5px;
            }
        </style>
    </head>

    <body>
        <form id="form1" runat="server">
            <div class="profile-container">
                <div class="page-header">
                    <div class="row align-items-center">
                        <div class="col">
                            <h2 class="mb-0">My Profile</h2>
                        </div>
                        <div class="col-auto">
                            <asp:Button ID="btnBackToDashboard" runat="server" Text="Back to Dashboard"
                                CssClass="btn-back" OnClick="btnBackToDashboard_Click" CausesValidation="false" />
                        </div>
                    </div>
                </div>

                <div class="content-card">
                    <h4 class="mb-3">Update Profile Information</h4>
                    <asp:Panel ID="pnlMessage" runat="server" Visible="false" CssClass="alert mb-3" role="alert">
                        <asp:Label ID="lblMessage" runat="server"></asp:Label>
                    </asp:Panel>

                    <div class="mb-3">
                        <label class="form-label">Email (Cannot be changed)</label>
                        <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" ReadOnly="true"
                            Enabled="false"></asp:TextBox>
                    </div>

                    <div class="mb-3">
                        <label class="form-label">Full Name</label>
                        <asp:TextBox ID="txtFullName" runat="server" CssClass="form-control"
                            placeholder="Enter your full name"></asp:TextBox>
                    </div>

                    <div class="mb-3">
                        <label class="form-label">Phone Number</label>
                        <asp:TextBox ID="txtPhoneNumber" runat="server" CssClass="form-control"
                            placeholder="Enter your phone number"></asp:TextBox>
                    </div>

                    <div class="mb-3">
                        <label class="form-label">Branch (Cannot be changed)</label>
                        <asp:TextBox ID="txtBranch" runat="server" CssClass="form-control" ReadOnly="true"
                            Enabled="false"></asp:TextBox>
                    </div>

                    <asp:Button ID="btnUpdateProfile" runat="server" Text="Update Profile" CssClass="btn-save"
                        OnClick="btnUpdateProfile_Click" />
                </div>

                <div class="content-card">
                    <h4 class="mb-3">Change Password</h4>
                    <div class="mb-3">
                        <label class="form-label">New Password</label>
                        <asp:TextBox ID="txtNewPassword" runat="server" CssClass="form-control" TextMode="Password"
                            placeholder="Enter new password"></asp:TextBox>
                        <small class="text-muted">Minimum 6 characters</small>
                    </div>

                    <div class="mb-3">
                        <label class="form-label">Confirm New Password</label>
                        <asp:TextBox ID="txtConfirmPassword" runat="server" CssClass="form-control" TextMode="Password"
                            placeholder="Confirm new password"></asp:TextBox>
                        <asp:CompareValidator ID="cvPassword" runat="server" ControlToValidate="txtConfirmPassword"
                            ControlToCompare="txtNewPassword" ErrorMessage="Passwords do not match"
                            CssClass="text-danger" Display="Dynamic"></asp:CompareValidator>
                    </div>

                    <asp:Button ID="btnChangePassword" runat="server" Text="Change Password" CssClass="btn-save"
                        OnClick="btnChangePassword_Click" />
                </div>
            </div>
        </form>
    </body>

    </html>