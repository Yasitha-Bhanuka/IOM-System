<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddStationary.aspx.cs"
    Inherits="InventoryManagementSystem.UI.Admin.AddStationary" %>

    <!DOCTYPE html>
    <html xmlns="http://www.w3.org/1999/xhtml">

    <head runat="server">
        <title>Add Stationary - Inventory Management System</title>
        <link href="../../Content/bootstrap.min.css" rel="stylesheet" />
        <style>
            body {
                background: #f5f7fa;
                font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
            }

            .container-custom {
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
                    <h2 class="mb-0">Add New Stationary</h2>
                    <p class="mb-0">Create a new stationary location</p>
                </div>

                <div class="content-card">
                    <asp:Panel ID="pnlMessage" runat="server" Visible="false" CssClass="alert mb-3" role="alert">
                        <asp:Label ID="lblMessage" runat="server"></asp:Label>
                    </asp:Panel>

                    <div class="row">
                        <div class="col-md-12 mb-3">
                            <label class="form-label">Location Code * (e.g., STA1-A)</label>
                            <asp:TextBox ID="txtLocationCode" runat="server" CssClass="form-control"
                                placeholder="STA1-A" MaxLength="20"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvLocationCode" runat="server"
                                ControlToValidate="txtLocationCode" ErrorMessage="Location code is required"
                                CssClass="text-danger" Display="Dynamic" ValidationGroup="AddGroup">
                            </asp:RequiredFieldValidator>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-md-12 mb-3">
                            <label class="form-label">Description</label>
                            <asp:TextBox ID="txtDescription" runat="server" CssClass="form-control" TextMode="MultiLine"
                                Rows="3" placeholder="Enter description" MaxLength="500"></asp:TextBox>
                        </div>
                    </div>

                    <div class="mt-4">
                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn-cancel"
                            OnClick="btnCancel_Click" CausesValidation="false" />
                        <asp:Button ID="btnAddStationary" runat="server" Text="Add Stationary" CssClass="btn-add"
                            OnClick="btnAddStationary_Click" ValidationGroup="AddGroup" />
                    </div>
                </div>
            </div>
        </form>
    </body>

    </html>