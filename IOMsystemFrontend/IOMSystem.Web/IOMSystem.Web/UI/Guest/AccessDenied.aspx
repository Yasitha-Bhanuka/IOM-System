<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AccessDenied.aspx.cs"
    Inherits="InventoryManagementSystem.UI.Guest.AccessDenied" %>

    <!DOCTYPE html>
    <html xmlns="http://www.w3.org/1999/xhtml">

    <head runat="server">
        <title>Access Denied</title>
        <link href="../../Content/bootstrap.min.css" rel="stylesheet" />
        <style>
            body {
                background: linear-gradient(135deg, #667eea 0%, #764ba2 100%);
                min-height: 100vh;
                display: flex;
                align-items: center;
                justify-content: center;
                font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
            }

            .access-denied-container {
                background: white;
                border-radius: 15px;
                box-shadow: 0 10px 40px rgba(0, 0, 0, 0.2);
                padding: 60px 40px;
                text-align: center;
                max-width: 500px;
            }

            .access-denied-icon {
                font-size: 80px;
                color: #dc3545;
                margin-bottom: 20px;
            }

            h1 {
                color: #333;
                font-weight: 600;
                margin-bottom: 15px;
            }

            p {
                color: #666;
                margin-bottom: 30px;
            }

            .btn-primary {
                background: linear-gradient(135deg, #667eea 0%, #764ba2 100%);
                border: none;
                border-radius: 8px;
                padding: 12px 30px;
                font-weight: 600;
            }
        </style>
    </head>

    <body>
        <form id="form1" runat="server">
            <div class="access-denied-container">
                <div class="access-denied-icon">!</div>
                <h1>Access Denied</h1>
                <p>You do not have permission to access this page. Please contact your administrator if you believe this
                    is an error.</p>
                <asp:Button ID="btnBackToLogin" runat="server" Text="Back to Login" CssClass="btn btn-primary"
                    OnClick="btnBackToLogin_Click" CausesValidation="false" />
            </div>
        </form>
    </body>

    </html>