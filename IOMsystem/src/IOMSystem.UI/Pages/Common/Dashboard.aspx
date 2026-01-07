<%@ Page Title="Dashboard" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeFile="Dashboard.aspx.cs" Inherits="Pages_Common_Dashboard" %>

    <asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
        <div class="row">
            <div class="col-md-12">
                <h1>Dashboard</h1>
                <p class="lead">Welcome back,
                    <asp:Literal ID="LitUserName" runat="server" />!
                </p>
            </div>
        </div>

        <div class="dashboard-grid">
            <div class="stat-card">
                <h3>Pending Requests</h3>
                <div class="value">12</div>
            </div>
            <div class="stat-card">
                <h3>Active Branches</h3>
                <div class="value">5</div>
            </div>
            <div class="stat-card">
                <h3>Total Products</h3>
                <div class="value">124</div>
            </div>
            <div class="stat-card">
                <h3>Recent Orders</h3>
                <div class="value">8</div>
            </div>
        </div>
    </asp:Content>