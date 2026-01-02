<%@ Page Title="Products" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeFile="Products.aspx.cs" Inherits="Products" %>

    <asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

        <div class="card">
            <div class="card-header">
                <h3 class="card-title">Product Inventory</h3>
                <button type="button" class="btn btn-primary" onclick="window.location.href='#'">
                    <i class="fa-solid fa-plus" style="margin-right: 5px;"></i> Add Product
                </button>
            </div>
            <div class="card-body" style="padding: 0;">
                <div class="table-responsive">
                    <table class="table" id="productsTable">
                        <thead>
                            <tr>
                                <th>SKU</th>
                                <th>Product Name</th>
                                <th>Location</th>
                                <th class="text-right">Price</th>
                                <th class="text-right">Stock</th>
                                <th>Status</th>
                                <th>Actions</th>
                            </tr>
                        </thead>
                        <tbody>
                            <tr>
                                <td colspan="7" style="text-align:center; padding: 2rem; color: var(--text-muted);">
                                    Loading products...
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </div>
            </div>
        </div>

        <script>
            document.addEventListener('DOMContentLoaded', loadProducts);

            async function loadProducts() {
                const tableBody = document.querySelector('#productsTable tbody');
                const apiUrl = 'http://localhost:5102/api/products';

                try {
                    const response = await fetch(apiUrl);
                    if (response.ok) {
                        const products = await response.json();
                        renderTable(products);
                    } else {
                        tableBody.innerHTML = `<tr><td colspan="7" style="text-align:center; color: var(--danger);">Failed to load products. Status: ${response.status}</td></tr>`;
                    }
                } catch (error) {
                    console.error(error);
                    tableBody.innerHTML = `<tr><td colspan="7" style="text-align:center; color: var(--danger);">Connection refused. Ensure backend is running.</td></tr>`;
                }
            }

            function renderTable(products) {
                const tableBody = document.querySelector('#productsTable tbody');

                if (products.length === 0) {
                    tableBody.innerHTML = `<tr><td colspan="7" style="text-align:center;">No products found.</td></tr>`;
                    return;
                }

                let html = '';
                products.forEach(p => {
                    const statusBadge = p.isActive
                        ? '<span class="badge badge-success">Active</span>'
                        : '<span class="badge badge-danger">Inactive</span>';

                    html += `
                    <tr>
                        <td style="font-family: monospace; font-weight: 500;">${p.sku || p.ProductID}</td>
                        <td style="font-weight: 500;">${p.productName}</td>
                        <td>${p.locationCode}</td>
                        <td class="text-right">$${p.price.toFixed(2)}</td>
                        <td class="text-right">${p.stockQuantity}</td>
                        <td>${statusBadge}</td>
                        <td>
                            <button class="btn btn-secondary btn-sm" style="padding: 0.25rem 0.5rem;" title="Edit">
                                <i class="fa-solid fa-pen"></i>
                            </button>
                            <button class="btn btn-danger btn-sm" style="padding: 0.25rem 0.5rem;" title="Delete">
                                <i class="fa-solid fa-trash"></i>
                            </button>
                        </td>
                    </tr>
                `;
                });

                tableBody.innerHTML = html;
            }
        </script>
    </asp:Content>