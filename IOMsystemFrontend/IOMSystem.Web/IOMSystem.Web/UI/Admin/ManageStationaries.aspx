<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ManageStationaries.aspx.cs"
    Inherits="InventoryManagementSystem.UI.Admin.ManageStationaries" %>

    <!DOCTYPE html>
    <html xmlns="http://www.w3.org/1999/xhtml">

    <head runat="server">
        <title>Manage Stationaries</title>
        <link href="../../Content/bootstrap.min.css" rel="stylesheet" />
        <style>
            body {
                background: #f5f7fa;
                font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
            }

            .container-custom {
                padding: 30px;
                max-width: 1400px;
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

            .btn-add {
                background: #28a745;
                color: white;
                border: none;
                padding: 10px 20px;
                border-radius: 5px;
            }

            .btn-activate {
                background: #28a745;
                color: white;
                border: none;
                padding: 6px 12px;
                border-radius: 5px;
                margin-right: 5px;
                font-size: 14px;
            }

            .btn-deactivate {
                background: #dc3545;
                color: white;
                border: none;
                padding: 6px 12px;
                border-radius: 5px;
                font-size: 14px;
            }

            .btn-back {
                background: #6c757d;
                color: white;
                border: none;
                padding: 10px 20px;
                border-radius: 5px;
            }

            .badge-active {
                background: #28a745;
                color: white;
                padding: 5px 10px;
                border-radius: 5px;
            }

            .badge-inactive {
                background: #dc3545;
                color: white;
                padding: 5px 10px;
                border-radius: 5px;
            }

            /* Table Improvements */
            .table-responsive-custom {
                overflow-x: auto;
                -webkit-overflow-scrolling: touch;
            }

            .table th {
                white-space: nowrap;
                vertical-align: middle;
                font-weight: 600;
                background-color: #f8f9fa;
                border-bottom: 2px solid #dee2e6;
            }

            .table td {
                vertical-align: middle;
                padding: 12px 8px;
            }

            /* Action buttons styling */
            .action-buttons {
                display: flex;
                gap: 5px;
                flex-wrap: wrap;
                justify-content: flex-start;
            }

            .btn-activate,
            .btn-deactivate {
                min-width: 85px;
                font-size: 13px;
                padding: 6px 12px;
                white-space: nowrap;
            }

            /* Status badge improvements */
            .badge-active,
            .badge-inactive {
                display: inline-block;
                min-width: 70px;
                text-align: center;
                font-size: 12px;
                font-weight: 600;
            }

            /* Responsive adjustments */
            @media (max-width: 1200px) {
                .container-custom {
                    padding: 20px;
                }

                .table {
                    font-size: 13px;
                }

                .table th,
                .table td {
                    padding: 8px 5px;
                }
            }

            @media (max-width: 768px) {
                .container-custom {
                    padding: 15px;
                }

                .table {
                    font-size: 12px;
                }

                .btn-activate,
                .btn-deactivate {
                    font-size: 11px;
                    padding: 5px 8px;
                    min-width: 70px;
                }

                .action-buttons {
                    flex-direction: column;
                }
            }
        </style>
    </head>

    <body>
        <form id="form1" runat="server">
            <div class="container-custom">
                <div class="page-header">
                    <div class="row align-items-center">
                        <div class="col">
                            <h2 class="mb-0">Manage Stationaries</h2>
                        </div>
                        <div class="col-auto">
                            <asp:Button ID="btnAddNewStationary" runat="server" Text="+ Add New Stationary"
                                CssClass="btn-add me-2" OnClick="btnAddNewStationary_Click" CausesValidation="false" />
                            <asp:Button ID="btnBackToDashboard" runat="server" Text="Back to Dashboard"
                                CssClass="btn-back" OnClick="btnBackToDashboard_Click" CausesValidation="false" />
                        </div>
                    </div>
                </div>

                <asp:Panel ID="pnlMessage" runat="server" Visible="false" CssClass="alert mb-3" role="alert">
                    <asp:Label ID="lblMessage" runat="server"></asp:Label>
                </asp:Panel>

                <div class="content-card mb-3">
                    <h5 class="mb-3">Search Stationaries</h5>
                    <div class="row">
                        <div class="col-md-10">
                            <asp:TextBox ID="txtSearch" runat="server" CssClass="form-control"
                                placeholder="Search by Location Code or Description..."></asp:TextBox>
                        </div>
                        <div class="col-md-2">
                            <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn btn-primary w-100"
                                OnClick="btnSearch_Click" CausesValidation="false" />
                        </div>
                    </div>
                    <div class="row mt-2">
                        <div class="col-md-12">
                            <asp:Button ID="btnClearSearch" runat="server" Text="Clear Search"
                                CssClass="btn btn-secondary btn-sm" OnClick="btnClearSearch_Click"
                                CausesValidation="false" />
                            <asp:Label ID="lblSearchResults" runat="server" CssClass="ms-3 text-muted"></asp:Label>
                        </div>
                    </div>
                </div>

                <div class="content-card">
                    <h4 class="mb-3">All Stationaries</h4>
                    <div class="table-responsive-custom">
                        <asp:GridView ID="gvStationaries" runat="server" CssClass="table table-striped table-hover"
                            AutoGenerateColumns="False" OnRowCommand="gvStationaries_RowCommand"
                            OnRowEditing="gvStationaries_RowEditing" OnRowUpdating="gvStationaries_RowUpdating"
                            OnRowCancelingEdit="gvStationaries_RowCancelingEdit" DataKeyNames="LocationCode">
                            <Columns>
                                <asp:BoundField DataField="LocationCode" HeaderText="Location Code" ReadOnly="true" />
                                <asp:TemplateField HeaderText="Description">
                                    <ItemTemplate>
                                        <%# Eval("Description") %>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtEditDescription" runat="server"
                                            Text='<%# Bind("Description") %>' CssClass="form-control"
                                            TextMode="MultiLine" Rows="2" MaxLength="500"></asp:TextBox>
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="CreatedDate" HeaderText="Created Date"
                                    DataFormatString="{0:yyyy-MM-dd}" ReadOnly="true" />
                                <asp:TemplateField HeaderText="Status">
                                    <ItemTemplate>
                                        <%# (bool)Eval("IsActive") ? "<span class='badge-active'>Active</span>"
                                            : "<span class='badge-inactive'>Inactive</span>" %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Actions">
                                    <ItemTemplate>
                                        <div class="action-buttons">
                                            <asp:Button ID="btnEdit" runat="server" Text="Edit" CssClass="btn-activate"
                                                CommandName="Edit" CausesValidation="false" />
                                            <asp:Button ID="btnActivate" runat="server" Text="Activate"
                                                CssClass="btn-activate" CommandName="ActivateStationary"
                                                CommandArgument='<%# Eval("LocationCode") %>'
                                                Visible='<%# !(bool)Eval("IsActive") %>' CausesValidation="false" />
                                            <asp:Button ID="btnDeactivate" runat="server" Text="Deactivate"
                                                CssClass="btn-deactivate" CommandName="DeactivateStationary"
                                                CommandArgument='<%# Eval("LocationCode") %>'
                                                Visible='<%# (bool)Eval("IsActive") %>'
                                                OnClientClick="return confirm('Are you sure you want to deactivate this stationary?');"
                                                CausesValidation="false" />
                                        </div>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <div class="action-buttons">
                                            <asp:Button ID="btnUpdate" runat="server" Text="Update"
                                                CssClass="btn-activate" CommandName="Update" />
                                            <asp:Button ID="btnCancel" runat="server" Text="Cancel"
                                                CssClass="btn-deactivate" CommandName="Cancel"
                                                CausesValidation="false" />
                                        </div>
                                    </EditItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <EmptyDataTemplate>
                                <div class="text-center text-muted py-4">
                                    <h5>No stationaries found</h5>
                                </div>
                            </EmptyDataTemplate>
                        </asp:GridView>
                    </div>
                </div>
            </div>
        </form>
    </body>

    </html>