<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddBranch.aspx.cs"
    Inherits="InventoryManagementSystem.UI.Admin.AddBranch" %>

    <!DOCTYPE html>
    <html xmlns="http://www.w3.org/1999/xhtml">

    <head runat="server">
        <title>Add Branch - Inventory Management System</title>
        <link href="../../Content/bootstrap.min.css" rel="stylesheet" />
        <style>
            body {
                background: #f5f7fa;
                font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
            }

            .container-custom {
                padding: 30px;
                max-width: 900px;
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
                padding: 30px;
                box-shadow: 0 2px 10px rgba(0, 0, 0, 0.1);
            }

            .btn-add {
                background: #28a745;
                color: white;
                border: none;
                padding: 12px 30px;
                border-radius: 5px;
                font-weight: 600;
            }

            .btn-add:hover {
                background: #218838;
            }

            .btn-cancel {
                background: #6c757d;
                color: white;
                border: none;
                padding: 12px 30px;
                border-radius: 5px;
                margin-right: 10px;
            }

            .btn-cancel:hover {
                background: #5a6268;
            }
        </style>
    </head>

    <body>
        <form id="form1" runat="server">
            <div class="container-custom">
                <div class="page-header">
                    <h2 class="mb-0">Add New Branch</h2>
                    <p class="mb-0">Create a new branch location</p>
                </div>

                <div class="content-card">
                    <asp:Panel ID="pnlMessage" runat="server" Visible="false" CssClass="alert mb-3" role="alert">
                        <asp:Label ID="lblMessage" runat="server"></asp:Label>
                    </asp:Panel>

                    <div class="row">
                        <div class="col-md-6 mb-3">
                            <label class="form-label">Branch Name *</label>
                            <asp:TextBox ID="txtBranchName" runat="server" CssClass="form-control"
                                placeholder="Enter branch name"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvBranchName" runat="server"
                                ControlToValidate="txtBranchName" ErrorMessage="Branch name is required"
                                CssClass="text-danger" Display="Dynamic"></asp:RequiredFieldValidator>
                        </div>
                        <div class="col-md-6 mb-3">
                            <label class="form-label">Branch Code *</label>
                            <asp:TextBox ID="txtBranchCode" runat="server" CssClass="form-control"
                                placeholder="Enter branch code"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvBranchCode" runat="server"
                                ControlToValidate="txtBranchCode" ErrorMessage="Branch code is required"
                                CssClass="text-danger" Display="Dynamic"></asp:RequiredFieldValidator>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-md-6 mb-3">
                            <label class="form-label">City</label>
                            <asp:TextBox ID="txtCity" runat="server" CssClass="form-control" placeholder="Enter city">
                            </asp:TextBox>
                        </div>
                        <div class="col-md-6 mb-3">
                            <label class="form-label">Phone Number</label>
                            <asp:TextBox ID="txtPhoneNumber" runat="server" CssClass="form-control"
                                placeholder="Enter phone number"></asp:TextBox>
                        </div>
                    </div>

                    <div class="mb-3">
                        <label class="form-label">Address</label>
                        <asp:TextBox ID="txtAddress" runat="server" CssClass="form-control" TextMode="MultiLine"
                            Rows="3" placeholder="Enter address"></asp:TextBox>
                    </div>

                    <div class="mt-4">
                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn-cancel"
                            OnClick="btnCancel_Click" CausesValidation="false" />
                        <asp:Button ID="btnAddBranch" runat="server" Text="Add Branch" CssClass="btn-add"
                            OnClick="btnAddBranch_Click" />
                    </div>
                </div>
            </div>
        </form>
    </body>

    </html>