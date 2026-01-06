<%@ Page Title="Orders" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="List.aspx.cs"
    Inherits="Pages_Orders_List" %>

    <asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
        <div class="card">
            <div class="card-header">
                <h3 class="card-title">Order History</h3>
                <button type="button" class="btn btn-primary" onclick="alert('Create Order Coming Soon')">
                    <i class="fa-solid fa-plus" style="margin-right: 5px;"></i> Create Order
                </button>
            </div>
            <div class="card-body" style="padding: 0;">
                <div class="table-responsive">
                    <table class="table" id="ordersTable">
                        <thead>
                            <tr>
                                <th>Order ID</th>
                                <th>Customer</th>
                                <th>Date</th>
                                <th class="text-right">Total Amount</th>
                                <th>Status</th>
                                <th class="text-right">Actions</th>
                            </tr>
                        </thead>
                        <tbody>
                            <tr>
                                <td colspan="6" class="text-center">Loading...</td>
                            </tr>
                        </tbody>
                    </table>
                </div>
            </div>
        </div>

        <script>
            document.addEventListener('DOMContentLoaded', loadOrders);

            async function loadOrders() {
                const tbody = document.querySelector('#ordersTable tbody');
                try {
                    const orders = await ApiService.get('orders');
                    if (!orders || orders.length === 0) {
                        tbody.innerHTML = '<tr><td colspan="6" style="text-align:center;">No orders found.</td></tr>';
                        return;
                    }

                    tbody.innerHTML = orders.map(o => `
                    <tr>
                        <td>#${o.orderId}</td>
                        <td style="font-weight:500">User ID: ${o.userId}</td>
                        <td>${new Date(o.orderDate).toLocaleDateString()}</td>
                        <td class="text-right">$${o.totalAmount.toFixed(2)}</td>
                        <td><span class="badge badge-success">Completed</span></td>
                        <td class="text-right">
                             <button class="btn btn-secondary btn-sm" onclick="viewOrder(${o.orderId})"><i class="fa-solid fa-eye"></i></button>
                        </td>
                    </tr>
                `).join('');
                } catch (err) {
                    tbody.innerHTML = `<tr><td colspan="6" style="color:var(--danger)">Error loading orders.</td></tr>`;
                }
            }

            function viewOrder(id) {
                alert('View details for Order #' + id);
            }
        </script>
    </asp:Content>