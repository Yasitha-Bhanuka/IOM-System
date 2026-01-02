<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Dashboard.aspx.cs"
    Inherits="InventoryManagementSystem.UI.Admin.Dashboard" %>

    <!DOCTYPE html>
    <html xmlns="http://www.w3.org/1999/xhtml">

    <head runat="server">
        <title>Admin Dashboard - Inventory System</title>
        <link href="../../Content/bootstrap.min.css" rel="stylesheet" />
        <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.4.0/css/all.min.css" />
        <style>
            :root {
                --primary-gradient: linear-gradient(135deg, #667eea 0%, #764ba2 100%);
                --card-hover-shadow: 0 15px 30px rgba(0, 0, 0, 0.1);
                --text-color: #2d3748;
                --bg-color: #f7fafc;
            }

            body {
                background: var(--bg-color);
                font-family: 'Segoe UI', 'Roboto', Helvetica, Arial, sans-serif;
                color: var(--text-color);
            }

            .dashboard-wrapper {
                min-height: 100vh;
                display: flex;
                flex-direction: column;
            }

            .main-content {
                flex: 1;
                padding: 40px;
                max-width: 1400px;
                margin: 0 auto;
                width: 100%;
            }

            /* Header Section */
            .dashboard-header {
                background: white;
                padding: 30px 40px;
                border-radius: 16px;
                box-shadow: 0 4px 6px rgba(0, 0, 0, 0.05);
                margin-bottom: 40px;
                display: flex;
                justify-content: space-between;
                align-items: center;
                background-image: url('data:image/svg+xml,<svg width="200" height="200" viewBox="0 0 200 200" xmlns="http://www.w3.org/2000/svg"><path fill="%23667eea" fill-opacity="0.05" d="M44.7,-76.4C58.9,-69.2,71.8,-59.1,81.6,-46.6C91.4,-34.1,98.1,-19.2,95.8,-4.9C93.5,9.4,82.2,23.1,70.6,34.8C59,46.5,47.1,56.2,34.3,64.2C21.5,72.2,7.8,78.5,-5,87.2C-17.8,95.9,-29.7,107,-40.4,105.7C-51.1,104.4,-60.7,90.7,-68.8,77.2C-76.9,63.7,-83.5,50.4,-86.3,36.5C-89.1,22.6,-88.1,8.1,-84.3,-5.3C-80.5,-18.7,-73.9,-31,-65.4,-42.1C-56.9,-53.2,-46.5,-63.1,-34.8,-71.9C-23.1,-80.7,-10.1,-88.4,1.9,-91.7C13.9,-95,25.8,-93.8,30.5,-83.6L44.7,-76.4Z" transform="translate(100 100)" /></svg>');
                background-repeat: no-repeat;
                background-position: right bottom;
            }

            .header-title h1 {
                font-weight: 800;
                font-size: 2.2rem;
                margin-bottom: 5px;
                background: var(--primary-gradient);
                -webkit-background-clip: text;
                background-clip: text;
                -webkit-text-fill-color: transparent;
            }

            .header-subtitle {
                color: #718096;
                font-size: 1.1rem;
            }

            .btn-logout {
                background: linear-gradient(135deg, #ff6b6b 0%, #ee5253 100%);
                color: white;
                border: none;
                padding: 12px 30px;
                border-radius: 50px;
                font-weight: 600;
                box-shadow: 0 4px 15px rgba(238, 82, 83, 0.3);
                transition: all 0.3s ease;
            }

            .btn-logout:hover {
                transform: translateY(-2px);
                box-shadow: 0 8px 20px rgba(238, 82, 83, 0.4);
            }

            /* Stats Grid */
            .stats-grid {
                display: grid;
                grid-template-columns: repeat(auto-fit, minmax(240px, 1fr));
                gap: 25px;
                margin-bottom: 40px;
            }

            .stat-card {
                background: white;
                border-radius: 16px;
                padding: 25px;
                display: flex;
                align-items: center;
                box-shadow: 0 4px 6px rgba(0, 0, 0, 0.02);
                transition: transform 0.3s ease, box-shadow 0.3s ease;
                border: 1px solid rgba(0, 0, 0, 0.03);
                text-decoration: none;
                color: inherit;
                cursor: pointer;
            }

            .stat-card:hover {
                transform: translateY(-5px);
                box-shadow: var(--card-hover-shadow);
                text-decoration: none;
            }

            .stat-icon {
                width: 60px;
                height: 60px;
                border-radius: 12px;
                display: flex;
                align-items: center;
                justify-content: center;
                font-size: 24px;
                margin-right: 20px;
            }

            .stat-info h3 {
                font-size: 28px;
                font-weight: 700;
                margin: 0;
                color: #2d3748;
            }

            .stat-info p {
                color: #718096;
                margin: 0;
                font-size: 14px;
                font-weight: 500;
                text-transform: uppercase;
                letter-spacing: 0.5px;
            }

            .bg-blue-light {
                background: #ebf8ff;
                color: #4299e1;
            }

            .bg-purple-light {
                background: #faf5ff;
                color: #9f7aea;
            }

            .bg-green-light {
                background: #f0fff4;
                color: #48bb78;
            }

            .bg-red-light {
                background: #fff5f5;
                color: #f56565;
            }

            .bg-orange-light {
                background: #fffaf0;
                color: #ed8936;
            }

            /* Navigation Grid */
            .nav-section-title {
                margin-bottom: 25px;
                font-size: 1.4rem;
                font-weight: 700;
                color: #2d3748;
                padding-left: 10px;
                border-left: 5px solid #667eea;
            }

            .nav-grid {
                display: grid;
                grid-template-columns: repeat(auto-fill, minmax(300px, 1fr));
                gap: 30px;
            }

            .nav-card {
                background: white;
                border-radius: 20px;
                overflow: hidden;
                box-shadow: 0 4px 6px rgba(0, 0, 0, 0.02);
                transition: all 0.3s cubic-bezier(0.4, 0, 0.2, 1);
                border: 1px solid rgba(0, 0, 0, 0.03);
                display: block;
                text-decoration: none;
                height: 100%;
            }

            .nav-card:hover {
                transform: translateY(-8px);
                box-shadow: 0 20px 40px rgba(0, 0, 0, 0.1);
                text-decoration: none;
            }

            .nav-card-body {
                padding: 30px;
                text-align: center;
            }

            .nav-icon-wrapper {
                width: 80px;
                height: 80px;
                border-radius: 50%;
                margin: 0 auto 20px;
                display: flex;
                align-items: center;
                justify-content: center;
                font-size: 32px;
                transition: transform 0.3s ease;
            }

            .nav-card:hover .nav-icon-wrapper {
                transform: scale(1.1) rotate(5deg);
            }

            .nav-card-title {
                font-size: 1.25rem;
                font-weight: 700;
                color: #2d3748;
                margin-bottom: 10px;
            }

            .nav-card-desc {
                color: #718096;
                font-size: 0.95rem;
                line-height: 1.5;
            }

            /* Card Colors */
            .nav-orders .nav-icon-wrapper {
                background: linear-gradient(135deg, #667eea 0%, #764ba2 100%);
                color: white;
            }

            .nav-products .nav-icon-wrapper {
                background: linear-gradient(135deg, #4facfe 0%, #00f2fe 100%);
                color: white;
            }

            .nav-users .nav-icon-wrapper {
                background: linear-gradient(135deg, #43e97b 0%, #38f9d7 100%);
                color: white;
            }

            .nav-branches .nav-icon-wrapper {
                background: linear-gradient(135deg, #fa709a 0%, #fee140 100%);
                color: white;
            }

            .nav-stationary .nav-icon-wrapper {
                background: linear-gradient(135deg, #8fd3f4 0%, #84fab0 100%);
                color: white;
            }

            .nav-reports .nav-icon-wrapper {
                background: linear-gradient(135deg, #a18cd1 0%, #fbc2eb 100%);
                color: white;
            }
        </style>
    </head>

    <body>
        <form id="form1" runat="server">
            <div class="dashboard-wrapper">
                <div class="main-content">

                    <!-- Header -->
                    <div class="dashboard-header">
                        <div class="header-title">
                            <h1>Dashboard</h1>
                            <div class="header-subtitle">Welcome back, <asp:Label ID="lblAdminName" runat="server">Admin
                                </asp:Label>
                            </div>
                        </div>
                        <asp:Button ID="btnLogout" runat="server" Text="Log Out" CssClass="btn-logout"
                            OnClick="btnLogout_Click" />
                    </div>

                    <!-- Statistics -->
                    <div class="stats-grid">
                        <!-- Pending Orders -->
                        <a href="ManageOrders.aspx" class="stat-card">
                            <div class="stat-icon bg-orange-light">
                                <i class="fas fa-shopping-cart"></i>
                            </div>
                            <div class="stat-info">
                                <h3>
                                    <asp:Label ID="lblPendingOrders" runat="server">0</asp:Label>
                                </h3>
                                <p>Pending Orders</p>
                            </div>
                        </a>

                        <!-- Low Stock -->
                        <a href="ManageProducts.aspx" class="stat-card">
                            <div class="stat-icon bg-red-light">
                                <i class="fas fa-exclamation-triangle"></i>
                            </div>
                            <div class="stat-info">
                                <h3>
                                    <asp:Label ID="lblLowStockCount" runat="server">0</asp:Label>
                                </h3>
                                <p>Low Stock Items</p>
                            </div>
                        </a>

                        <!-- Pending Requests -->
                        <a href="PendingRegistrations.aspx" class="stat-card">
                            <div class="stat-icon bg-blue-light">
                                <i class="fas fa-user-plus"></i>
                            </div>
                            <div class="stat-info">
                                <h3>
                                    <asp:Label ID="lblPendingRequests" runat="server">0</asp:Label>
                                </h3>
                                <p>User Requests</p>
                            </div>
                        </a>

                        <!-- Active Users -->
                        <a href="ManageUsers.aspx" class="stat-card">
                            <div class="stat-icon bg-green-light">
                                <i class="fas fa-users"></i>
                            </div>
                            <div class="stat-info">
                                <h3>
                                    <asp:Label ID="lblActiveUsers" runat="server">0</asp:Label>
                                </h3>
                                <p>Active Users</p>
                            </div>
                        </a>

                        <!-- Branches -->
                        <a href="ManageBranches.aspx" class="stat-card">
                            <div class="stat-icon bg-purple-light">
                                <i class="fas fa-building"></i>
                            </div>
                            <div class="stat-info">
                                <h3>
                                    <asp:Label ID="lblTotalBranches" runat="server">0</asp:Label>
                                </h3>
                                <p>Branches</p>
                            </div>
                        </a>
                    </div>

                    <!-- Management Section -->
                    <h2 class="nav-section-title">Management Modules</h2>
                    <div class="nav-grid">

                        <!-- Order Management -->
                        <a href="ManageOrders.aspx" class="nav-card nav-orders">
                            <div class="nav-card-body">
                                <div class="nav-icon-wrapper">
                                    <i class="fas fa-clipboard-list"></i>
                                </div>
                                <h3 class="nav-card-title">Order Management</h3>
                                <p class="nav-card-desc">Review pending orders, update statuses, and track order
                                    history.</p>
                            </div>
                        </a>

                        <!-- Product Management -->
                        <a href="ManageProducts.aspx" class="nav-card nav-products">
                            <div class="nav-card-body">
                                <div class="nav-icon-wrapper">
                                    <i class="fas fa-box-open"></i>
                                </div>
                                <h3 class="nav-card-title">Product Inventory</h3>
                                <p class="nav-card-desc">Add new products, update stock levels, and manage inventory.
                                </p>
                            </div>
                        </a>

                        <!-- Stationary Management -->
                        <a href="ManageStationaries.aspx" class="nav-card nav-stationary">
                            <div class="nav-card-body">
                                <div class="nav-icon-wrapper">
                                    <i class="fas fa-pencil-ruler"></i>
                                </div>
                                <h3 class="nav-card-title">Stationary Items</h3>
                                <p class="nav-card-desc">Manage stationary categories and specific item types.</p>
                            </div>
                        </a>

                        <!-- User Management -->
                        <a href="ManageUsers.aspx" class="nav-card nav-users">
                            <div class="nav-card-body">
                                <div class="nav-icon-wrapper">
                                    <i class="fas fa-users-cog"></i>
                                </div>
                                <h3 class="nav-card-title">User Management</h3>
                                <p class="nav-card-desc">Manage system users, permissions, and roles.</p>
                            </div>
                        </a>

                        <!-- Branch Management -->
                        <a href="ManageBranches.aspx" class="nav-card nav-branches">
                            <div class="nav-card-body">
                                <div class="nav-icon-wrapper">
                                    <i class="fas fa-store-alt"></i>
                                </div>
                                <h3 class="nav-card-title">Branch Locations</h3>
                                <p class="nav-card-desc">Add and manage company branches and their details.</p>
                            </div>
                        </a>

                        <!-- Registration Requests -->
                        <a href="PendingRegistrations.aspx" class="nav-card nav-reports">
                            <div class="nav-card-body">
                                <div class="nav-icon-wrapper">
                                    <i class="fas fa-user-clock"></i>
                                </div>
                                <h3 class="nav-card-title">Pending Requests</h3>
                                <p class="nav-card-desc">Review and approve new user account registration requests.</p>
                            </div>
                        </a>

                    </div>

                </div>
            </div>
        </form>
    </body>

    </html>