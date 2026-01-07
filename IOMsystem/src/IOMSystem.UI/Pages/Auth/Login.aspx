<%@ Page Title="Log in" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeFile="Login.aspx.cs"
    Inherits="Pages_Auth_Login" Async="true" %>

    <asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
        <div class="auth-container">
            <h2>
                <%: Title %>
            </h2>
            <p>Use your account to log in.</p>
            <asp:Label ID="lblMessage" runat="server" CssClass="text-danger"></asp:Label>

            <div class="form-group">
                <label>Email</label>
                <asp:TextBox runat="server" ID="Email" CssClass="form-control" TextMode="Email" />
                <asp:RequiredFieldValidator runat="server" ControlToValidate="Email" CssClass="text-danger"
                    ErrorMessage="The email field is required." />
            </div>

            <div class="form-group">
                <label>Password</label>
                <asp:TextBox runat="server" ID="Password" CssClass="form-control" TextMode="Password" />
                <asp:RequiredFieldValidator runat="server" ControlToValidate="Password" CssClass="text-danger"
                    ErrorMessage="The password field is required." />
            </div>

            <div class="form-group">
                <asp:Button runat="server" OnClick="LogIn" Text="Log in" CssClass="btn btn-primary" />
            </div>

            <p>
                <a href="Register.aspx">Register as a new user</a>
            </p>
        </div>
    </asp:Content>