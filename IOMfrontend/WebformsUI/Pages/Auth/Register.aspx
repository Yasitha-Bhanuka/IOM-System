<%@ Page Title="Register" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeFile="Register.aspx.cs" Inherits="WebformsUI.Pages.Auth.Register" Async="true" %>

    <asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
        <h2>
            <%: Title %>.
        </h2>
        <p class="text-danger">
            <asp:Literal runat="server" ID="ErrorMessage" />
        </p>

        <div class="form-horizontal">
            <h4>Create a new account.</h4>
            <hr />
            <asp:Label ID="lblError" runat="server" CssClass="text-danger" Visible="false"></asp:Label>
            <div class="form-group">
                <asp:Label runat="server" AssociatedControlID="txtEmail" CssClass="col-md-2 control-label">Email
                </asp:Label>
                <div class="col-md-10">
                    <asp:TextBox runat="server" ID="txtEmail" CssClass="form-control" TextMode="Email" />
                    <asp:RequiredFieldValidator runat="server" ControlToValidate="txtEmail" CssClass="text-danger"
                        ErrorMessage="The email field is required." />
                </div>
            </div>
            <div class="form-group">
                <asp:Label runat="server" AssociatedControlID="txtFullName" CssClass="col-md-2 control-label">Full Name
                </asp:Label>
                <div class="col-md-10">
                    <asp:TextBox runat="server" ID="txtFullName" CssClass="form-control" />
                    <asp:RequiredFieldValidator runat="server" ControlToValidate="txtFullName" CssClass="text-danger"
                        ErrorMessage="The full name field is required." />
                </div>
            </div>
            <div class="form-group">
                <asp:Label runat="server" AssociatedControlID="txtBranch" CssClass="col-md-2 control-label">Branch
                </asp:Label>
                <div class="col-md-10">
                    <asp:TextBox runat="server" ID="txtBranch" CssClass="form-control" />
                    <asp:RequiredFieldValidator runat="server" ControlToValidate="txtBranch" CssClass="text-danger"
                        ErrorMessage="The branch field is required." />
                </div>
            </div>
            <div class="form-group">
                <asp:Label runat="server" AssociatedControlID="txtPassword" CssClass="col-md-2 control-label">Password
                </asp:Label>
                <div class="col-md-10">
                    <asp:TextBox runat="server" ID="txtPassword" CssClass="form-control" TextMode="Password" />
                    <asp:RequiredFieldValidator runat="server" ControlToValidate="txtPassword" CssClass="text-danger"
                        ErrorMessage="The password field is required." />
                </div>
            </div>
            <div class="form-group">
                <div class="col-md-offset-2 col-md-10">
                    <asp:Button runat="server" OnClick="btnRegister_Click" Text="Register" CssClass="btn btn-default" />
                </div>
            </div>
        </div>
    </asp:Content>