<%@ Page Title="Register" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeFile="Register.aspx.cs" Inherits="Account_Register" %>

    <asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
        <div style="max-width: 500px; margin: 0 auto;">
            <div class="card">
                <div class="card-header">
                    <h3 class="card-title">
                        <%: Title %>
                    </h3>
                </div>
                <div class="card-body">
                    <p class="text-muted" style="margin-bottom: 1.5rem;">Create a new account.</p>
                    <p style="color: var(--danger);">
                        <asp:Literal runat="server" ID="ErrorMessage" />
                    </p>

                    <asp:ValidationSummary runat="server" style="color: var(--danger); margin-bottom: 1rem;" />

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
                        <asp:Label runat="server" AssociatedControlID="ConfirmPassword" CssClass="form-label">Confirm
                            password</asp:Label>
                        <asp:TextBox runat="server" ID="ConfirmPassword" TextMode="Password" CssClass="form-control" />
                        <asp:RequiredFieldValidator runat="server" ControlToValidate="ConfirmPassword"
                            style="color: var(--danger); font-size: 0.875rem;" Display="Dynamic"
                            ErrorMessage="The confirm password field is required." />
                        <asp:CompareValidator runat="server" ControlToCompare="Password"
                            ControlToValidate="ConfirmPassword" style="color: var(--danger); font-size: 0.875rem;"
                            Display="Dynamic" ErrorMessage="The password and confirmation password do not match." />
                    </div>

                    <div class="form-group" style="margin-top: 1.5rem;">
                        <asp:Button runat="server" OnClick="CreateUser_Click" Text="Register" CssClass="btn btn-primary"
                            style="width: 100%;" />
                    </div>
                </div>
            </div>
        </div>
    </asp:Content>