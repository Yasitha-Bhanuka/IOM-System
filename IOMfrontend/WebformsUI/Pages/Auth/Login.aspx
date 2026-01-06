<%@ Page Title="Log in" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeFile="Login.aspx.cs"
    Inherits="WebformsUI.Pages.Auth.Login" Async="true" %>

    <asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
        <h2>
            <%: Title %>.
        </h2>
        <div class="row">
            <div class="col-md-8">
                <section id="loginForm">
                    <div class="form-horizontal">
                        <h4>Use a local account to log in.</h4>
                        <hr />
                        <asp:Label ID="lblMessage" runat="server" Visible="false"></asp:Label>
                        <div class="form-group">
                            <asp:Label runat="server" AssociatedControlID="txtEmail" CssClass="col-md-2 control-label">
                                Email</asp:Label>
                            <div class="col-md-10">
                                <asp:TextBox runat="server" ID="txtEmail" CssClass="form-control" TextMode="Email" />
                                <asp:RequiredFieldValidator runat="server" ControlToValidate="txtEmail"
                                    CssClass="text-danger" ErrorMessage="The email field is required." />
                            </div>
                        </div>
                        <div class="form-group">
                            <asp:Label runat="server" AssociatedControlID="txtPassword"
                                CssClass="col-md-2 control-label">Password</asp:Label>
                            <div class="col-md-10">
                                <asp:TextBox runat="server" ID="txtPassword" CssClass="form-control"
                                    TextMode="Password" />
                                <asp:RequiredFieldValidator runat="server" ControlToValidate="txtPassword"
                                    CssClass="text-danger" ErrorMessage="The password field is required." />
                            </div>
                        </div>
                        <div class="form-group">
                            <div class="col-md-offset-2 col-md-10">
                                <asp:Button runat="server" ID="btnLogin" OnClick="btnLogin_Click" Text="Log in"
                                    CssClass="btn btn-default" />
                            </div>
                        </div>
                        <p>
                            <a runat="server" href="~/Pages/Auth/Register">Register as a new user</a>
                        </p>
                    </div>
                </section>
            </div>
        </div>
    </asp:Content>