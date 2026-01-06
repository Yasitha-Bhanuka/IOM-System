<%@ Page Title="Log in" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs"
    Inherits="Account_Login" Async="true" %>

    <%@ Register Src="~/Account/OpenAuthProviders.ascx" TagPrefix="uc" TagName="OpenAuthProviders" %>

        <asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
            <div style="max-width: 500px; margin: 0 auto;">
                <div class="card">
                    <div class="card-header">
                        <h3 class="card-title">
                            <%: Title %>
                        </h3>
                    </div>
                    <div class="card-body">
                        <section id="loginForm">
                            <p class="text-muted" style="margin-bottom: 1.5rem;">Use a local account to log in.</p>

                            <asp:PlaceHolder runat="server" ID="ErrorMessage" Visible="false">
                                <p style="color: var(--danger);">
                                    <asp:Literal runat="server" ID="FailureText" />
                                </p>
                            </asp:PlaceHolder>

                            <div class="form-group">
                                <asp:Label runat="server" AssociatedControlID="UserName" CssClass="form-label">User name
                                </asp:Label>
                                <asp:TextBox runat="server" ID="UserName" CssClass="form-control" />
                                <asp:RequiredFieldValidator runat="server" ControlToValidate="UserName"
                                    style="color: var(--danger); font-size: 0.875rem;"
                                    ErrorMessage="The user name field is required." />
                            </div>

                            <div class="form-group">
                                <asp:Label runat="server" AssociatedControlID="Password" CssClass="form-label">Password
                                </asp:Label>
                                <asp:TextBox runat="server" ID="Password" TextMode="Password" CssClass="form-control" />
                                <asp:RequiredFieldValidator runat="server" ControlToValidate="Password"
                                    style="color: var(--danger); font-size: 0.875rem;"
                                    ErrorMessage="The password field is required." />
                            </div>

                            <div class="form-group">
                                <div style="display: flex; align-items: center; gap: 0.5rem;">
                                    <asp:CheckBox runat="server" ID="RememberMe" />
                                    <asp:Label runat="server" AssociatedControlID="RememberMe"
                                        style="font-size: 0.875rem;">Remember me?</asp:Label>
                                </div>
                            </div>

                            <div class="form-group" style="margin-top: 1.5rem;">
                                <asp:Button runat="server" OnClick="LogIn" Text="Log in" CssClass="btn btn-primary"
                                    style="width: 100%;" />
                            </div>

                            <div style="text-align: center; margin-top: 1rem; font-size: 0.875rem;">
                                <asp:HyperLink runat="server" ID="RegisterHyperLink" ViewStateMode="Disabled">Register
                                </asp:HyperLink>
                                if you don't have a local account.
                            </div>
                        </section>
                    </div>
                </div>

                <!-- Social Login: Hidden or Styled if needed, keeping it minimal for now -->
                <div style="margin-top: 2rem;">
                    <section id="socialLoginForm">
                        <uc:OpenAuthProviders runat="server" ID="OpenAuthLogin" />
                    </section>
                </div>
            </div>
        </asp:Content>