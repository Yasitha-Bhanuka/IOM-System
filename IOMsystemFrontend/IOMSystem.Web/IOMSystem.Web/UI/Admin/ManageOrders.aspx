<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ManageOrders.aspx.cs"
    Inherits="InventoryManagementSystem.UI.Admin.ManageOrders" %>

    <!DOCTYPE html>
    <html xmlns="http://www.w3.org/1999/xhtml">

    <head runat="server">
        <title>Manage Orders</title>
        <link href="../../Content/bootstrap.min.css" rel="stylesheet" />
        <style>
            body {
                background: #f5f7fa;
                font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
            }

            .container-custom {
                padding: 30px;
                max-width: 1600px;
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

            .filter-box {
                background: white;
                border-radius: 10px;
                padding: 20px;
                box-shadow: 0 2px 10px rgba(0, 0, 0, 0.1);
                margin-bottom: 20px;
            }

            .btn-back {
                background: #6c757d;
                color: white;
                border: none;
                padding: 10px 20px;
                border-radius: 5px;
            }

            .btn-filter {
                background: #667eea;
                color: white;
                border: none;
                padding: 10px 30px;
                border-radius: 5px;
            }

            .badge-pending {
                background: #ffc107;
                color: #000;
                padding: 5px 12px;
                border-radius: 5px;
                font-weight: bold;
            }

            .badge-approved {
                background: #28a745;
                color: white;
                padding: 5px 12px;
                border-radius: 5px;
                font-weight: bold;
            }

            .badge-shipped {
                background: #17a2b8;
                color: white;
                padding: 5px 12px;
                border-radius: 5px;
                font-weight: bold;
            }

            .badge-cancelled {
                background: #dc3545;
                color: white;
                padding: 5px 12px;
                border-radius: 5px;
                font-weight: bold;
            }

            .stock-ok {
                color: #28a745;
                font-weight: bold;
            }

            .stock-warning {
                color: #ffc107;
                font-weight: bold;
            }

            .stock-error {
                color: #dc3545;
                font-weight: bold;
            }

            .modal-overlay {
                position: fixed;
                top: 0;
                left: 0;
                width: 100%;
                height: 100%;
                background-color: rgba(0, 0, 0, 0.6);
                z-index: 1050;
                display: flex;
                justify-content: center;
                align-items: center;
                backdrop-filter: blur(2px);
            }

            .modal-content-custom {
                background: white;
                border: none;
                border-radius: 12px;
                padding: 30px;
                width: 70%;
                max-width: 900px;
                max-height: 90vh;
                overflow-y: auto;
                box-shadow: 0 10px 40px rgba(0, 0, 0, 0.3);
                position: relative;
                animation: slideDown 0.3s ease-out;
            }

            @keyframes slideDown {
                from {
                    transform: translateY(-50px);
                    opacity: 0;
                }

                to {
                    transform: translateY(0);
                    opacity: 1;
                }
            }

            .btn-close-custom {
                background: transparent;
                border: none;
                color: #6c757d;
                font-size: 1.5rem;
                padding: 0;
                line-height: 1;
                transition: color 0.2s;
            }

            .btn-close-custom:hover {
                color: #343a40;
            }

            .update-status-section {
                background-color: #f8f9fa;
                border: 1px solid #e9ecef;
                border-radius: 8px;
                padding: 20px;
                margin-top: 25px;
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

            .btn-view-details {
                background: #667eea;
                color: white;
                border: none;
                padding: 6px 12px;
                border-radius: 5px;
                font-size: 13px;
                min-width: 100px;
                white-space: nowrap;
            }

            .btn-view-details:hover {
                background: #5568d3;
            }

            /* Status badge improvements */
            .badge-pending,
            .badge-approved,
            .badge-shipped,
            .badge-cancelled {
                display: inline-block;
                min-width: 80px;
                text-align: center;
                font-size: 12px;
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

                .btn-view-details {
                    font-size: 11px;
                    padding: 5px 8px;
                    min-width: 80px;
                }

                .modal-content-custom {
                    width: 95%;
                    padding: 20px;
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
                            <h2 class="mb-0">Manage Orders</h2>
                            <p class="mb-0">View and manage customer orders</p>
                        </div>
                        <div class="col-auto">
                            <asp:Button ID="btnBackToDashboard" runat="server" Text="Back to Dashboard"
                                CssClass="btn-back" OnClick="btnBackToDashboard_Click" CausesValidation="false" />
                        </div>
                    </div>
                </div>

                <div class="filter-box">
                    <h5 class="mb-3">Filter Orders</h5>
                    <div class="row">
                        <div class="col-md-3">
                            <label>Branch</label>
                            <asp:DropDownList ID="ddlBranchFilter" runat="server" CssClass="form-control">
                                <asp:ListItem Value="">All Branches</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <div class="col-md-3">
                            <label>Status</label>
                            <asp:DropDownList ID="ddlStatusFilter" runat="server" CssClass="form-control">
                                <asp:ListItem Value="">All Statuses</asp:ListItem>
                                <asp:ListItem Value="Pending">Pending</asp:ListItem>
                                <asp:ListItem Value="Approved">Approved</asp:ListItem>
                                <asp:ListItem Value="Shipped">Shipped</asp:ListItem>
                                <asp:ListItem Value="Cancelled">Cancelled</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <div class="col-md-2">
                            <label>&nbsp;</label>
                            <asp:Button ID="btnFilter" runat="server" Text="Apply Filter" CssClass="btn-filter w-100"
                                OnClick="btnFilter_Click" />
                        </div>
                        <div class="col-md-2">
                            <label>&nbsp;</label>
                            <asp:Button ID="btnClearFilter" runat="server" Text="Clear"
                                CssClass="btn btn-secondary w-100" OnClick="btnClearFilter_Click" />
                        </div>
                    </div>
                </div>

                <div class="content-card">
                    <h4 class="mb-3">Orders</h4>
                    <div class="table-responsive-custom">
                        <asp:GridView ID="gvOrders" runat="server" CssClass="table table-striped table-hover"
                            AutoGenerateColumns="False" OnRowCommand="gvOrders_RowCommand"
                            OnRowDataBound="gvOrders_RowDataBound">
                            <Columns>
                                <asp:BoundField DataField="OrderId" HeaderText="Order ID" />
                                <asp:TemplateField HeaderText="Customer">
                                    <ItemTemplate>
                                        <%# Eval("User.FullName") %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Branch">
                                    <ItemTemplate>
                                        <%# Eval("User.BranchName") %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Order Date">
                                    <ItemTemplate>
                                        <%# Eval("OrderDate", "{0:MMM dd, yyyy}" ) %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Total">
                                    <ItemTemplate>
                                        Rs.<%# Eval("TotalAmount", "{0:F2}" ) %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Status">
                                    <ItemTemplate>
                                        <%# GetStatusBadge(Eval("Status").ToString()) %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Stock Status">
                                    <ItemTemplate>
                                        <asp:Label ID="lblStockStatus" runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Actions">
                                    <ItemTemplate>
                                        <asp:Button ID="btnViewDetails" runat="server" Text="View Details"
                                            CssClass="btn-view-details" CommandName="ViewDetails"
                                            CommandArgument='<%# Eval("OrderId") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <EmptyDataTemplate>
                                <div class="text-center text-muted py-4">
                                    <h5>No orders found</h5>
                                    <p>No orders match your filter criteria</p>
                                </div>
                            </EmptyDataTemplate>
                        </asp:GridView>
                    </div>
                </div>

                <!-- Modal Overlay Panel -->
                <asp:Panel ID="pnlOrderDetails" runat="server" CssClass="modal-overlay" Visible="false">
                    <div class="modal-content-custom">
                        <div class="d-flex justify-content-between align-items-center mb-3">
                            <h4 class="mb-0">Order #<asp:Label ID="lblOrderId" runat="server"></asp:Label> - Details
                            </h4>
                            <asp:Button ID="btnCloseDetails" runat="server" Text="X" CssClass="btn-close-custom"
                                OnClick="btnCloseDetails_Click" CausesValidation="false" Font-Bold="true"
                                Font-Size="Large" />
                        </div>

                        <div class="row mb-3">
                            <div class="col-md-3">
                                <strong>Customer:</strong><br />
                                <asp:Label ID="lblCustomerName" runat="server"></asp:Label>
                            </div>
                            <div class="col-md-3">
                                <strong>Branch:</strong><br />
                                <asp:Label ID="lblBranchName" runat="server"></asp:Label>
                            </div>
                            <div class="col-md-3">
                                <strong>Order Date:</strong><br />
                                <asp:Label ID="lblOrderDate" runat="server"></asp:Label>
                            </div>
                            <div class="col-md-3">
                                <strong>Current Status:</strong><br />
                                <asp:Label ID="lblOrderStatus" runat="server"></asp:Label>
                            </div>
                        </div>

                        <div class="row mb-3">
                            <div class="col-md-6">
                                <strong>Total Amount:</strong> Rs.<asp:Label ID="lblTotalAmount" runat="server">
                                </asp:Label>
                            </div>
                            <div class="col-md-6">
                                <strong>Notes:</strong>
                                <asp:Label ID="lblNotes" runat="server"></asp:Label>
                            </div>
                        </div>

                        <h6 class="mt-3 mb-2">Order Items:</h6>
                        <div class="table-responsive" style="max-height: 250px; overflow-y: auto;">
                            <asp:GridView ID="gvOrderItems" runat="server" CssClass="table table-bordered table-sm mb-0"
                                AutoGenerateColumns="False">
                                <Columns>
                                    <asp:BoundField DataField="SKU" HeaderText="SKU" />
                                    <asp:BoundField DataField="ProductName" HeaderText="Product" />
                                    <asp:BoundField DataField="Quantity" HeaderText="Quantity" />
                                    <asp:TemplateField HeaderText="Unit Price">
                                        <ItemTemplate>
                                            Rs.<%# Eval("UnitPrice", "{0:F2}" ) %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Subtotal">
                                        <ItemTemplate>
                                            Rs.<%# Eval("Subtotal", "{0:F2}" ) %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>

                        <!-- Update Status Section -->
                        <asp:Panel ID="pnlUpdateStatusSection" runat="server" CssClass="update-status-section">
                            <h6 class="mb-3">Update Order Status</h6>

                            <asp:Panel ID="pnlStockWarning" runat="server" CssClass="alert alert-danger mb-3"
                                Visible="false">
                                <strong>Stock Validation Failed!</strong><br />
                                <asp:Label ID="lblStockWarning" runat="server"></asp:Label>
                            </asp:Panel>

                            <div class="row">
                                <div class="col-md-6">
                                    <label>New Status</label>
                                    <asp:DropDownList ID="ddlNewStatus" runat="server" CssClass="form-control">
                                        <asp:ListItem Value="">Select Status</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="col-md-6 d-flex align-items-end">
                                    <asp:Button ID="btnUpdateStatus" runat="server" Text="Update Status"
                                        CssClass="btn btn-success w-100" OnClick="btnUpdateStatus_Click" />
                                </div>
                            </div>
                        </asp:Panel>
                    </div>
                </asp:Panel>

                <asp:Panel ID="pnlMessage" runat="server" Visible="false" CssClass="alert mt-3" role="alert">
                    <asp:Label ID="lblMessage" runat="server"></asp:Label>
                </asp:Panel>
            </div>
        </form>
    </body>

    </html>