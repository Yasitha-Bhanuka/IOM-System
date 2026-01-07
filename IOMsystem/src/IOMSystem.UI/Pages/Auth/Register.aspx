<%@ Page Title="Register" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeFile="Register.aspx.cs" Inherits="Pages_Auth_Register" Async="true" %>

    <asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
        <div class="auth-container">
            <h2>
                <%: Title %>
            </h2>
            <p>Create a new account.</p>
            <asp:Label ID="lblMessage" runat="server" CssClass="text-danger"></asp:Label>

            <div class="form-group">
                <label>Full Name</label>
                <asp:TextBox runat="server" ID="FullName" CssClass="form-control" />
                <asp:RequiredFieldValidator runat="server" ControlToValidate="FullName" CssClass="text-danger"
                    ErrorMessage="Full Name is required." />
            </div>

            <div class="form-group">
                <label>Email</label>
                <asp:TextBox runat="server" ID="Email" CssClass="form-control" TextMode="Email" />
                <asp:RequiredFieldValidator runat="server" ControlToValidate="Email" CssClass="text-danger"
                    ErrorMessage="Email is required." />
            </div>

            <div class="form-group">
                <label>Password</label>
                <asp:TextBox runat="server" ID="Password" CssClass="form-control" TextMode="Password" />
                <asp:RequiredFieldValidator runat="server" ControlToValidate="Password" CssClass="text-danger"
                    ErrorMessage="Password is required." />
            </div>

            <div class="form-group">
                <label>Confirm Password</label>
                <asp:TextBox runat="server" ID="ConfirmPassword" CssClass="form-control" TextMode="Password" />
                <asp:CompareValidator runat="server" ControlToCompare="Password" ControlToValidate="ConfirmPassword"
                    CssClass="text-danger" ErrorMessage="Passwords do not match." />
            </div>

            <div class="form-group">
                <asp:Button runat="server" OnClick="RegisterUser" Text="Register" CssClass="btn btn-primary" />
            </div>

            <p>
                <a href="Login.aspx">Already have an account? Log in</a>
            </p>
        </div>
    </asp:Content>