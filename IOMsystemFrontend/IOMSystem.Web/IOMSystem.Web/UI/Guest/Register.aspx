<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Register.aspx.cs"
    Inherits="InventoryManagementSystem.UI.Guest.Register" %>

    <!DOCTYPE html>
    <html xmlns="http://www.w3.org/1999/xhtml">

    <head runat="server">
        <title>Register - Inventory Management System</title>
        <link href="../../Content/bootstrap.min.css" rel="stylesheet" />
        <style>
            body {
                background: linear-gradient(135deg, #667eea 0%, #764ba2 100%);
                min-height: 100vh;
                display: flex;
                align-items: center;
                justify-content: center;
                font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
                padding: 20px 0;
            }

            .register-container {
                background: white;
                border-radius: 15px;
                box-shadow: 0 10px 40px rgba(0, 0, 0, 0.2);
                padding: 40px;
                width: 100%;
                max-width: 750px;
            }

            .register-header {
                text-align: center;
                margin-bottom: 30px;
            }

            .register-header h2 {
                color: #667eea;
                font-weight: 600;
                margin-bottom: 10px;
            }

            .form-control {
                border-radius: 8px;
                padding: 12px;
                border: 1px solid #ddd;
            }

            .form-control:focus {
                border-color: #667eea;
                box-shadow: 0 0 0 0.2rem rgba(102, 126, 234, 0.25);
            }

            .btn-register {
                background: linear-gradient(135deg, #667eea 0%, #764ba2 100%);
                border: none;
                border-radius: 8px;
                padding: 12px;
                font-weight: 600;
                width: 100%;
                color: white;
                transition: transform 0.2s;
            }

            .btn-register:hover {
                transform: translateY(-2px);
                box-shadow: 0 5px 15px rgba(102, 126, 234, 0.4);
            }

            .login-link {
                text-align: center;
                margin-top: 20px;
                color: #666;
            }

            .login-link a {
                color: #667eea;
                text-decoration: none;
                font-weight: 600;
            }

            .login-link a:hover {
                text-decoration: underline;
            }
        </style>
    </head>

    <body>
        <form id="form1" runat="server">
            <div class="register-container">
                <div class="register-header">
                    <h2>Create Account</h2>
                    <p class="text-muted">Request access to the system</p>
                </div>

                <asp:Panel ID="pnlMessage" runat="server" Visible="false" CssClass="alert" role="alert">
                    <asp:Label ID="lblMessage" runat="server"></asp:Label>
                </asp:Panel>

                <div class="row">
                    <div class="col-md-6 mb-3">
                        <label for="txtFullName" class="form-label">Full Name</label>
                        <asp:TextBox ID="txtFullName" runat="server" CssClass="form-control"
                            placeholder="Enter your full name"></asp:TextBox>
                    </div>

                    <div class="col-md-6 mb-3">
                        <label for="txtEmail" class="form-label">Email Address *</label>
                        <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" TextMode="Email"
                            placeholder="Enter your email" required="required"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvEmail" runat="server" ControlToValidate="txtEmail"
                            ErrorMessage="Email is required" CssClass="text-danger" Display="Dynamic">
                        </asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="revEmail" runat="server" ControlToValidate="txtEmail"
                            ErrorMessage="Invalid email format" CssClass="text-danger" Display="Dynamic"
                            ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">
                        </asp:RegularExpressionValidator>
                    </div>
                </div>

                <div class="row">
                    <div class="col-md-6 mb-3">
                        <label for="txtPhoneNumber" class="form-label">Phone Number</label>
                        <asp:TextBox ID="txtPhoneNumber" runat="server" CssClass="form-control"
                            placeholder="Enter your phone number"></asp:TextBox>
                    </div>

                    <div class="col-md-6 mb-3">
                        <label for="ddlBranch" class="form-label">Branch *</label>
                        <asp:DropDownList ID="ddlBranch" runat="server" CssClass="form-control">
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="rfvBranch" runat="server" ControlToValidate="ddlBranch"
                            InitialValue="" ErrorMessage="Please select a branch" CssClass="text-danger"
                            Display="Dynamic">
                        </asp:RequiredFieldValidator>
                    </div>
                </div>

                <div class="row">
                    <div class="col-md-6 mb-3">
                        <label for="txtPassword" class="form-label">Password *</label>
                        <asp:TextBox ID="txtPassword" runat="server" CssClass="form-control" TextMode="Password"
                            placeholder="Enter password" required="required"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvPassword" runat="server" ControlToValidate="txtPassword"
                            ErrorMessage="Password is required" CssClass="text-danger" Display="Dynamic">
                        </asp:RequiredFieldValidator>
                        <small class="text-muted">Minimum 6 characters</small>
                    </div>

                    <div class="col-md-6 mb-3">
                        <label for="txtConfirmPassword" class="form-label">Confirm Password *</label>
                        <asp:TextBox ID="txtConfirmPassword" runat="server" CssClass="form-control" TextMode="Password"
                            placeholder="Confirm password" required="required"></asp:TextBox>
                        <asp:CompareValidator ID="cvPassword" runat="server" ControlToValidate="txtConfirmPassword"
                            ControlToCompare="txtPassword" ErrorMessage="Passwords do not match" CssClass="text-danger"
                            Display="Dynamic"></asp:CompareValidator>
                    </div>
                </div>

                <div class="mb-3">
                    <asp:Button ID="btnRegister" runat="server" Text="Submit Registration Request"
                        CssClass="btn btn-register" OnClick="btnRegister_Click" />
                </div>

                <div class="login-link">
                    Already have an account? <a href="Login.aspx">Login here</a>
                </div>
            </div>
        </form>
    </body>

    </html>