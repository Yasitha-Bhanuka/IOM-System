<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MyOrders.aspx.cs"
    Inherits="InventoryManagementSystem.UI.Customer.MyOrders" %>

    <!DOCTYPE html>
    <html xmlns="http://www.w3.org/1999/xhtml">

    <head runat="server">
        <title>My Orders</title>
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

            .btn-back {
                background: #6c757d;
                color: white;
                border: none;
                padding: 10px 20px;
                border-radius: 5px;
            }

            .btn-view-details {
                background: #667eea;
                color: white;
                border: none;
                padding: 6px 15px;
                border-radius: 5px;
                font-size: 14px;
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

            .order-details-panel {
                background: #f8f9fa;
                border-left: 5px solid #667eea;
                padding: 20px;
                border-radius: 10px;
                margin-top: 20px;
            }

            /* Table Improvements */
            .table-responsive-custom {
                overflow-x: auto;
                -webkit-overflow-scrolling: touch;
            }

            .table th {
                white-space: nowrap;
                vertical-align: middle;
            }

            .table td {
                vertical-align: middle;
            }

            @media (max-width: 1200px) {
                .container-custom {
                    padding: 20px;
                }

                .table {
                    font-size: 13px;
                }
            }

            @media (max-width: 768px) {
                .container-custom {
                    padding: 15px;
                }

                .table {
                    font-size: 12px;
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
                            <h2 class="mb-0">My Orders</h2>
                            <p class="mb-0">View your order history and status</p>
                        </div>
                        <div class="col-auto">
                            <asp:Button ID="btnBackToDashboard" runat="server" Text="Back to Dashboard"
                                CssClass="btn-back" OnClick="btnBackToDashboard_Click" CausesValidation="false" />
                        </div>
                    </div>
                </div>

                <div class="content-card mb-3">
                    <h5 class="mb-3">Filter Orders</h5>
                    <div class="row">
                        <div class="col-md-4">
                            <label>Status</label>
                            <asp:DropDownList ID="ddlStatusFilter" runat="server" CssClass="form-control">
                                <asp:ListItem Value="">All Statuses</asp:ListItem>
                                <asp:ListItem Value="Pending">Pending</asp:ListItem>
                                <asp:ListItem Value="Approved">Approved</asp:ListItem>
                                <asp:ListItem Value="Shipped">Shipped</asp:ListItem>
                                <asp:ListItem Value="Cancelled">Cancelled</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <div class="col-md-3">
                            <label>&nbsp;</label>
                            <asp:Button ID="btnFilter" runat="server" Text="Apply Filter"
                                CssClass="btn btn-primary w-100" OnClick="btnFilter_Click" />
                        </div>
                        <div class="col-md-3">
                            <label>&nbsp;</label>
                            <asp:Button ID="btnClearFilter" runat="server" Text="Clear Filter"
                                CssClass="btn btn-secondary w-100" OnClick="btnClearFilter_Click" />
                        </div>
                    </div>
                </div>

                <div class="content-card">
                    <h4 class="mb-3">Order History</h4>
                    <div class="table-responsive-custom">
                        <asp:GridView ID="gvOrders" runat="server" CssClass="table table-striped table-hover"
                            AutoGenerateColumns="False" OnRowCommand="gvOrders_RowCommand">
                            <Columns>
                                <asp:BoundField DataField="OrderId" HeaderText="Order ID" />
                                <asp:TemplateField HeaderText="Order Date">
                                    <ItemTemplate>
                                        <%# Eval("OrderDate", "{0:MMM dd, yyyy}" ) %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Total Amount">
                                    <ItemTemplate>
                                        Rs.<%# Eval("TotalAmount", "{0:F2}" ) %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Status">
                                    <ItemTemplate>
                                        <%# GetStatusBadge(Eval("Status").ToString()) %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Items">
                                    <ItemTemplate>
                                        <%# Eval("OrderItems.Count") %> item(s)
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Action">
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
                                    <p>You haven't placed any orders yet</p>
                                    <asp:HyperLink ID="lnkViewProducts" runat="server" NavigateUrl="ViewProducts.aspx"
                                        CssClass="btn btn-primary">Browse Products</asp:HyperLink>
                                </div>
                            </EmptyDataTemplate>
                        </asp:GridView>
                    </div>
                </div>

                <asp:Panel ID="pnlOrderDetails" runat="server" CssClass="order-details-panel" Visible="false">
                    <h4 class="mb-3">Order Details - Order #<asp:Label ID="lblOrderId" runat="server"></asp:Label>
                    </h4>
                    <div class="row mb-3">
                        <div class="col-md-6">
                            <strong>Order Date:</strong>
                            <asp:Label ID="lblOrderDate" runat="server"></asp:Label>
                        </div>
                        <div class="col-md-6">
                            <strong>Status:</strong>
                            <asp:Label ID="lblOrderStatus" runat="server"></asp:Label>
                        </div>
                    </div>
                    <div class="mb-3">
                        <strong>Notes:</strong>
                        <asp:Label ID="lblOrderNotes" runat="server" Text="None"></asp:Label>
                    </div>

                    <h5 class="mt-4 mb-3">Order Items</h5>
                    <div class="table-responsive-custom">
                        <asp:GridView ID="gvOrderItems" runat="server" CssClass="table table-bordered"
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

                    <div class="text-end mt-3">
                        <h4>Total: Rs.<asp:Label ID="lblOrderTotal" runat="server"></asp:Label>
                        </h4>
                    </div>

                    <div class="mt-3">
                        <asp:Button ID="btnCloseDetails" runat="server" Text="Close" CssClass="btn btn-secondary"
                            OnClick="btnCloseDetails_Click" />
                    </div>
                </asp:Panel>
            </div>
        </form>
    </body>

    </html>