<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Dashboard.aspx.cs"
    Inherits="InventoryManagementSystem.UI.Customer.Dashboard" %>

    <!DOCTYPE html>
    <html xmlns="http://www.w3.org/1999/xhtml">

    <head runat="server">
        <title>Customer Dashboard - Inventory System</title>
        <link href="../../Content/bootstrap.min.css" rel="stylesheet" />
        <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.4.0/css/all.min.css" />
        <style>
            :root {
                --primary-gradient: linear-gradient(135deg, #667eea 0%, #764ba2 100%);
                --secondary-gradient: linear-gradient(135deg, #4facfe 0%, #00f2fe 100%);
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
                background-image: url('data:image/svg+xml,<svg width="200" height="200" viewBox="0 0 200 200" xmlns="http://www.w3.org/2000/svg"><path fill="%234facfe" fill-opacity="0.05" d="M44.7,-76.4C58.9,-69.2,71.8,-59.1,81.6,-46.6C91.4,-34.1,98.1,-19.2,95.8,-4.9C93.5,9.4,82.2,23.1,70.6,34.8C59,46.5,47.1,56.2,34.3,64.2C21.5,72.2,7.8,78.5,-5,87.2C-17.8,95.9,-29.7,107,-40.4,105.7C-51.1,104.4,-60.7,90.7,-68.8,77.2C-76.9,63.7,-83.5,50.4,-86.3,36.5C-89.1,22.6,-88.1,8.1,-84.3,-5.3C-80.5,-18.7,-73.9,-31,-65.4,-42.1C-56.9,-53.2,-46.5,-63.1,-34.8,-71.9C-23.1,-80.7,-10.1,-88.4,1.9,-91.7C13.9,-95,25.8,-93.8,30.5,-83.6L44.7,-76.4Z" transform="translate(100 100)" /></svg>');
                background-repeat: no-repeat;
                background-position: right bottom;
            }

            .header-title h1 {
                font-weight: 800;
                font-size: 2.2rem;
                margin-bottom: 5px;
                background: var(--secondary-gradient);
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

            /* Navigation Grid */
            .nav-section-title {
                margin-bottom: 30px;
                font-size: 1.6rem;
                font-weight: 700;
                color: #2d3748;
                text-align: center;
            }

            .nav-grid {
                display: grid;
                grid-template-columns: repeat(auto-fill, minmax(300px, 1fr));
                gap: 30px;
                max-width: 1000px;
                margin: 0 auto;
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
                transform: translateY(-10px);
                box-shadow: 0 20px 40px rgba(0, 0, 0, 0.15);
                text-decoration: none;
            }

            .nav-card-body {
                padding: 40px;
                text-align: center;
            }

            .nav-icon-wrapper {
                width: 90px;
                height: 90px;
                border-radius: 50%;
                margin: 0 auto 25px;
                display: flex;
                align-items: center;
                justify-content: center;
                font-size: 36px;
                transition: transform 0.3s ease;
            }

            .nav-card:hover .nav-icon-wrapper {
                transform: scale(1.15) rotate(5deg);
            }

            .nav-card-title {
                font-size: 1.4rem;
                font-weight: 700;
                color: #2d3748;
                margin-bottom: 12px;
            }

            .nav-card-desc {
                color: #718096;
                font-size: 1rem;
                line-height: 1.6;
            }

            /* Card Colors */
            .nav-products .nav-icon-wrapper {
                background: linear-gradient(135deg, #4facfe 0%, #00f2fe 100%);
                color: white;
            }

            .nav-orders .nav-icon-wrapper {
                background: linear-gradient(135deg, #667eea 0%, #764ba2 100%);
                color: white;
            }

            .nav-profile .nav-icon-wrapper {
                background: linear-gradient(135deg, #43e97b 0%, #38f9d7 100%);
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
                            <div class="header-subtitle">Welcome back, <asp:Label ID="lblCustomerName" runat="server">
                                    Customer</asp:Label>!</div>
                        </div>
                        <asp:Button ID="btnLogout" runat="server" Text="Log Out" CssClass="btn-logout"
                            OnClick="btnLogout_Click" />
                    </div>

                    <!-- Quick Actions -->
                    <h2 class="nav-section-title">What would you like to do?</h2>
                    <div class="nav-grid">

                        <!-- View Products -->
                        <a href="ViewProducts.aspx" class="nav-card nav-products">
                            <div class="nav-card-body">
                                <div class="nav-icon-wrapper">
                                    <i class="fas fa-box-open"></i>
                                </div>
                                <h3 class="nav-card-title">Browse Products</h3>
                                <p class="nav-card-desc">View available products and place orders.</p>
                            </div>
                        </a>

                        <!-- My Orders -->
                        <a href="MyOrders.aspx" class="nav-card nav-orders">
                            <div class="nav-card-body">
                                <div class="nav-icon-wrapper">
                                    <i class="fas fa-clipboard-list"></i>
                                </div>
                                <h3 class="nav-card-title">My Orders</h3>
                                <p class="nav-card-desc">Track your order history and status.</p>
                            </div>
                        </a>

                        <!-- Edit Profile -->
                        <a href="Profile.aspx" class="nav-card nav-profile">
                            <div class="nav-card-body">
                                <div class="nav-icon-wrapper">
                                    <i class="fas fa-user-cog"></i>
                                </div>
                                <h3 class="nav-card-title">My Profile</h3>
                                <p class="nav-card-desc">View and update your account information.</p>
                            </div>
                        </a>

                    </div>

                </div>
            </div>
        </form>
    </body>

    </html>