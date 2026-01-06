<%@ Page Title="Dashboard" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="Default.aspx.cs" Inherits="_Default" %>

    <asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

        <div class="stats-grid">
            <div class="stat-card">
                <div class="stat-label">Total Revenue</div>
                <div class="stat-value">$24,500</div>
                <div class="stat-trend up"><i class="fa-solid fa-arrow-up"></i> 12% from last month</div>
            </div>
            <div class="stat-card">
                <div class="stat-label">Active Orders</div>
                <div class="stat-value">45</div>
                <div class="stat-trend nav"><i class="fa-solid fa-minus"></i> 0% change</div>
            </div>
            <div class="stat-card">
                <div class="stat-label">Low Stock Items</div>
                <div class="stat-value">8</div>
                <div class="stat-trend down"><i class="fa-solid fa-arrow-down"></i> Requires attention</div>
            </div>
            <div class="stat-card">
                <div class="stat-label">Total Products</div>
                <div class="stat-value">1,204</div>
                <div class="stat-trend up"><i class="fa-solid fa-arrow-up"></i> +5 new items</div>
            </div>
        </div>

        <div class="card">
            <div class="card-header">
                <h3 class="card-title">System Status</h3>
                <button type="button" class="btn btn-secondary btn-sm" onclick="testConnection()">
                    <i class="fa-solid fa-arrows-rotate" style="margin-right: 5px;"></i> Test Connection
                </button>
            </div>
            <div class="card-body">
                <div id="connectionStatus">
                    <span style="color: var(--text-muted);"><i class="fa-solid fa-circle-info"></i> Ready to check
                        backend connection (Localhost:5102)...</span>
                </div>
            </div>
        </div>

        <script>
            async function testConnection() {
                const resultDiv = document.getElementById('connectionStatus');
                resultDiv.innerHTML = "<span><i class='fa-solid fa-circle-notch fa-spin'></i> Pinging backend...</span>";
                const apiUrl = 'http://localhost:5102/api/ping';
                try {
                    const response = await fetch(apiUrl);
                    if (response.ok) {
                        const text = await response.text();
                        resultDiv.innerHTML = '<span style="color:var(--success); font-weight:500;"><i class="fa-solid fa-check-circle"></i> Connected: ' + text + '</span>';
                    } else {
                        resultDiv.innerHTML = '<span style="color:var(--danger); font-weight:500;"><i class="fa-solid fa-triangle-exclamation"></i> Error: ' + response.status + '</span>';
                    }
                } catch (error) {
                    console.error(error);
                    resultDiv.innerHTML = '<span style="color:var(--danger); font-weight:500;"><i class="fa-solid fa-triangle-exclamation"></i> Failed to connect. Is backend running on port 5102?</span>';
                }
            }
        </script>

    </asp:Content>