<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PendingRegistrations.aspx.cs"
    Inherits="InventoryManagementSystem.UI.Admin.PendingRegistrations" %>

    <!DOCTYPE html>
    <html xmlns="http://www.w3.org/1999/xhtml">

    <head runat="server">
        <title>Pending Registrations</title>
        <link href="../../Content/bootstrap.min.css" rel="stylesheet" />
        <style>
            body {
                background: #f5f7fa;
                font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
            }

            .container-custom {
                padding: 30px;
                max-width: 1200px;
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
            }

            .btn-approve {
                background: #28a745;
                color: white;
                border: none;
                padding: 8px 15px;
                border-radius: 5px;
                margin-right: 5px;
            }

            .btn-reject {
                background: #dc3545;
                color: white;
                border: none;
                padding: 8px 15px;
                border-radius: 5px;
            }

            .btn-delete {
                background: #6c757d;
                color: white;
                border: none;
                padding: 8px 15px;
                border-radius: 5px;
                margin-left: 5px;
            }

            .btn-back {
                background: #6c757d;
                color: white;
                border: none;
                padding: 10px 20px;
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

            .action-buttons {
                display: flex;
                gap: 5px;
                flex-wrap: wrap;
            }

            .btn-approve,
            .btn-reject,
            .btn-delete {
                min-width: 75px;
                font-size: 13px;
                white-space: nowrap;
            }

            @media (max-width: 1200px) {
                .table {
                    font-size: 13px;
                }

                .table th,
                .table td {
                    padding: 8px 5px;
                }
            }

            @media (max-width: 768px) {
                .table {
                    font-size: 12px;
                }

                .btn-approve,
                .btn-reject,
                .btn-delete {
                    font-size: 11px;
                    padding: 6px 10px;
                    min-width: 65px;
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
                            <h2 class="mb-0">Pending Registration Requests</h2>
                        </div>
                        <div class="col-auto">
                            <asp:Button ID="btnBackToDashboard" runat="server" Text="Back to Dashboard"
                                CssClass="btn-back" OnClick="btnBackToDashboard_Click" CausesValidation="false" />
                        </div>
                    </div>
                </div>

                <div class="content-card">
                    <asp:Panel ID="pnlMessage" runat="server" Visible="false" CssClass="alert mb-3" role="alert">
                        <asp:Label ID="lblMessage" runat="server"></asp:Label>
                    </asp:Panel>

                    <div class="table-responsive-custom">
                        <asp:GridView ID="gvPendingRequests" runat="server" CssClass="table table-striped table-hover"
                            AutoGenerateColumns="False" OnRowCommand="gvPendingRequests_RowCommand"
                            DataKeyNames="RequestId">
                            <Columns>
                                <asp:BoundField DataField="UserEmail" HeaderText="Email" />
                                <asp:BoundField DataField="FullName" HeaderText="Full Name" />
                                <asp:BoundField DataField="BranchName" HeaderText="Branch" />
                                <asp:BoundField DataField="PhoneNumber" HeaderText="Phone" />
                                <asp:BoundField DataField="RequestDate" HeaderText="Request Date"
                                    DataFormatString="{0:yyyy-MM-dd HH:mm}" />
                                <asp:TemplateField HeaderText="Actions">
                                    <ItemTemplate>
                                        <div class="action-buttons">
                                            <asp:Button ID="btnApprove" runat="server" Text="Approve"
                                                CssClass="btn-approve" CommandName="ApproveRequest"
                                                CommandArgument='<%# Eval("RequestId") %>' />
                                            <asp:Button ID="btnReject" runat="server" Text="Reject"
                                                CssClass="btn-reject" CommandName="RejectRequest"
                                                CommandArgument='<%# Eval("RequestId") %>'
                                                OnClientClick="return confirm('Are you sure you want to reject this request?');" />
                                            <asp:Button ID="btnDelete" runat="server" Text="Delete"
                                                CssClass="btn-delete" CommandName="DeleteRequest"
                                                CommandArgument='<%# Eval("RequestId") %>'
                                                OnClientClick="return confirm('Are you sure you want to permanently delete this registration request? This action cannot be undone.');" />
                                        </div>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <EmptyDataTemplate>
                                <div class="text-center text-muted py-4">
                                    <h5>No pending registration requests</h5>
                                </div>
                            </EmptyDataTemplate>
                        </asp:GridView>
                    </div>
                </div>
            </div>
        </form>
    </body>

    </html>