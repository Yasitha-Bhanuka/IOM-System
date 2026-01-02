<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="jumbotron">
        <h1>ASP.NET</h1>
        <p class="lead">ASP.NET is a free web framework for building great Web sites and Web applications using HTML, CSS, and JavaScript.</p>
        <p><a href="http://www.asp.net" class="btn btn-primary btn-lg">Learn more &raquo;</a></p>
    </div>

    <div class="row">
        <div class="col-md-12">
             <div class="panel panel-default">
                <div class="panel-heading">Backend Connection</div>
                <div class="panel-body">
                    <button type="button" class="btn btn-success" onclick="testConnection()">Ping Backend (Localhost:5102)</button>
                    <span id="result" style="margin-left:10px; font-weight:bold;">Waiting...</span>
                </div>
            </div>
        </div>
    </div>
    <script>
        async function testConnection() {
            const resultDiv = document.getElementById('result');
            resultDiv.innerText = "Pinging...";
            const apiUrl = 'http://localhost:5102/api/ping'; 
            try {
                const response = await fetch(apiUrl);
                if (response.ok) {
                    const text = await response.text();
                    resultDiv.innerHTML = '<span style="color:green">' + text + '</span>';
                } else {
                    resultDiv.innerHTML = '<span style="color:red">Error: ' + response.status + '</span>';
                }
            } catch (error) {
                console.error(error);
                resultDiv.innerHTML = '<span style="color:red">Failed (Is Backend running on port 5102?)</span>';
            }
        }
    </script>

    <div class="row">
        <div class="col-md-4">
            <h2>Getting started</h2>
            <p>
                ASP.NET Web Forms lets you build dynamic websites using a familiar drag-and-drop, event-driven model.
            A design surface and hundreds of controls and components let you rapidly build sophisticated, powerful UI-driven sites with data access.
            </p>
            <p>
                <a class="btn btn-default" href="https://go.microsoft.com/fwlink/?LinkId=301948">Learn more &raquo;</a>
            </p>
        </div>
        <div class="col-md-4">
            <h2>Get more libraries</h2>
            <p>
                NuGet is a free Visual Studio extension that makes it easy to add, remove, and update libraries and tools in Visual Studio projects.
            </p>
            <p>
                <a class="btn btn-default" href="https://go.microsoft.com/fwlink/?LinkId=301949">Learn more &raquo;</a>
            </p>
        </div>
        <div class="col-md-4">
            <h2>Web Hosting</h2>
            <p>
                You can easily find a web hosting company that offers the right mix of features and price for your applications.
            </p>
            <p>
                <a class="btn btn-default" href="https://go.microsoft.com/fwlink/?LinkId=301950">Learn more &raquo;</a>
            </p>
        </div>
    </div>
</asp:Content>
