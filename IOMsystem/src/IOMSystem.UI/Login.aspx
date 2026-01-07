<%@ Page Title="Login" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeFile="Login.aspx.cs"
    Inherits="Login" Async="true" %>

    <asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
        <div style="max-width: 400px; margin: 50px auto; padding: 20px; border: 1px solid #ccc; border-radius: 5px;">
            <h2>Login</h2>
            <asp:Label ID="lblMessage" runat="server" ForeColor="Red" Visible="false"></asp:Label>

            <div style="margin-bottom: 15px;">
                <label>Email</label>
                <asp:TextBox ID="txtEmail" runat="server" TextMode="Email" style="width: 100%; padding: 8px;">
                </asp:TextBox>
            </div>

            <div style="margin-bottom: 15px;">
                <label>Password</label>
                <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" style="width: 100%; padding: 8px;">
                </asp:TextBox>
            </div>

            <asp:Button ID="btnLogin" runat="server" Text="Login" OnClick="btnLogin_Click"
                style="width: 100%; padding: 10px; background-color: #007bff; color: white; border: none; cursor: pointer;" />
        </div>
    </asp:Content>