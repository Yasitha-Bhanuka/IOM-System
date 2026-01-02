<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ViewProducts.aspx.cs"
    Inherits="InventoryManagementSystem.UI.Customer.ViewProducts" %>

    <!DOCTYPE html>
    <html xmlns="http://www.w3.org/1999/xhtml">

    <head runat="server">
        <title>View Products</title>
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

            .search-box {
                background: white;
                border-radius: 10px;
                padding: 20px;
                box-shadow: 0 2px 10px rgba(0, 0, 0, 0.1);
                margin-bottom: 20px;
            }

            .btn-search {
                background: #667eea;
                color: white;
                border: none;
                padding: 10px 30px;
                border-radius: 5px;
            }

            .btn-back {
                background: #6c757d;
                color: white;
                border: none;
                padding: 10px 20px;
                border-radius: 5px;
            }

            .badge-available {
                background: #28a745;
                color: white;
                padding: 5px 10px;
                border-radius: 5px;
            }

            .badge-low-stock {
                background: #ffc107;
                color: #000;
                padding: 5px 10px;
                border-radius: 5px;
            }

            .badge-out-of-stock {
                background: #dc3545;
                color: white;
                padding: 5px 10px;
                border-radius: 5px;
            }

            .product-row {
                transition: background-color 0.2s;
            }

            .product-row:hover {
                background-color: #f8f9fa;
            }

            .btn-add-to-cart {
                background: #28a745;
                color: white;
                border: none;
                padding: 6px 15px;
                border-radius: 5px;
                font-size: 14px;
            }

            .btn-place-order {
                background: #667eea;
                color: white;
                border: none;
                padding: 12px 30px;
                border-radius: 5px;
                font-size: 16px;
                font-weight: bold;
            }

            .cart-panel {
                background: #fff3cd;
                border-left: 5px solid #ffc107;
                padding: 20px;
                border-radius: 10px;
                margin-bottom: 20px;
            }

            .cart-total {
                font-size: 24px;
                font-weight: bold;
                color: #667eea;
                text-align: right;
                margin-top: 15px;
            }

            .quantity-input {
                width: 80px;
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
                            <h2 class="mb-0">Available Products</h2>
                            <p class="mb-0">Browse our product catalog</p>
                        </div>
                        <div class="col-auto">
                            <asp:Button ID="btnBackToDashboard" runat="server" Text="Back to Dashboard"
                                CssClass="btn-back" OnClick="btnBackToDashboard_Click" CausesValidation="false" />
                        </div>
                    </div>
                </div>

                <div class="search-box">
                    <h5 class="mb-3">Search Products</h5>
                    <asp:Panel ID="pnlSearch" runat="server" DefaultButton="btnSearch">
                        <div class="row">
                            <div class="col-md-10">
                                <asp:TextBox ID="txtSearch" runat="server" CssClass="form-control"
                                    placeholder="Search by SKU or Product Name..."></asp:TextBox>
                            </div>
                            <div class="col-md-2">
                                <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn-search w-100"
                                    OnClick="btnSearch_Click" />
                            </div>
                        </div>
                        <div class="row mt-3">
                            <div class="col-md-12">
                                <label class="me-2"><strong>Sort by Name:</strong></label>
                                <asp:Button ID="btnSortAZ" runat="server" Text="A to Z"
                                    CssClass="btn btn-outline-primary btn-sm me-2" OnClick="btnSortAZ_Click"
                                    CausesValidation="false" />
                                <asp:Button ID="btnSortZA" runat="server" Text="Z to A"
                                    CssClass="btn btn-outline-primary btn-sm" OnClick="btnSortZA_Click"
                                    CausesValidation="false" />
                            </div>
                        </div>
                    </asp:Panel>
                </div>

                <div class="content-card">
                    <h4 class="mb-3">Product List</h4>
                    <div class="table-responsive-custom">
                        <asp:GridView ID="gvProducts" runat="server" CssClass="table table-striped table-hover"
                            AutoGenerateColumns="False">
                            <Columns>
                                <asp:BoundField DataField="SKU" HeaderText="SKU" />
                                <asp:BoundField DataField="ProductName" HeaderText="Product Name" />
                                <asp:BoundField DataField="LocationCode" HeaderText="Location" />
                                <asp:TemplateField HeaderText="Price">
                                    <ItemTemplate>
                                        Rs.<%# Eval("Price", "{0:F2}" ) %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Stock">
                                    <ItemTemplate>
                                        <%# Eval("StockQuantity") %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Availability">
                                    <ItemTemplate>
                                        <%# GetAvailabilityBadge((int)Eval("StockQuantity"),
                                            (int)Eval("MinStockThreshold")) %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Quantity">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtQuantity" runat="server"
                                            CssClass="form-control quantity-input" TextMode="Number" Text="1" min="1"
                                            max='<%# Eval("StockQuantity") %>'>
                                        </asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Action">
                                    <ItemTemplate>
                                        <asp:Button ID="btnAddToCart" runat="server" Text="Add to Cart"
                                            CssClass="btn-add-to-cart" CommandName="AddToCart"
                                            CommandArgument='<%# Eval("SKU") %>' OnCommand="btnAddToCart_Command"
                                            Enabled='<%# (int)Eval("StockQuantity") > 0 %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <EmptyDataTemplate>
                                <div class="text-center text-muted py-4">
                                    <h5>No products found</h5>
                                    <p>Try adjusting your search criteria</p>
                                </div>
                            </EmptyDataTemplate>
                        </asp:GridView>
                    </div>
                </div>

                <asp:Panel ID="pnlCart" runat="server" CssClass="cart-panel" Visible="false">
                    <h4 class="mb-3">Your Shopping Cart</h4>
                    <div class="table-responsive-custom">
                        <asp:GridView ID="gvCart" runat="server" CssClass="table table-bordered"
                            AutoGenerateColumns="False" OnRowCommand="gvCart_RowCommand">
                            <Columns>
                                <asp:BoundField DataField="ProductName" HeaderText="Product" />
                                <asp:BoundField DataField="SKU" HeaderText="SKU" />
                                <asp:TemplateField HeaderText="Price">
                                    <ItemTemplate>
                                        Rs.<%# Eval("UnitPrice", "{0:F2}" ) %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="Quantity" HeaderText="Quantity" />
                                <asp:TemplateField HeaderText="Subtotal">
                                    <ItemTemplate>
                                        Rs.<%# ((int)Eval("Quantity") * (decimal)Eval("UnitPrice")).ToString("F2") %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Action">
                                    <ItemTemplate>
                                        <asp:Button ID="btnRemove" runat="server" Text="Remove"
                                            CssClass="btn btn-danger btn-sm" CommandName="RemoveItem"
                                            CommandArgument='<%# Eval("SKU") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                    <div class="cart-total">
                        Total: Rs.<asp:Label ID="lblCartTotal" runat="server">0.00</asp:Label>
                    </div>
                    <div class="text-end mt-3">
                        <asp:Button ID="btnClearCart" runat="server" Text="Clear Cart" CssClass="btn btn-secondary me-2"
                            OnClick="btnClearCart_Click" />
                        <asp:Button ID="btnPlaceOrder" runat="server" Text="Place Order" CssClass="btn-place-order"
                            OnClick="btnPlaceOrder_Click" />
                    </div>
                </asp:Panel>

                <asp:Panel ID="pnlMessage" runat="server" Visible="false" CssClass="alert mt-3" role="alert">
                    <asp:Label ID="lblMessage" runat="server"></asp:Label>
                </asp:Panel>
            </div>
        </form>
    </body>

    </html>