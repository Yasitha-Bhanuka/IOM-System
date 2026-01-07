<%@ Page Language="C#" AutoEventWireup="true" %>
    <%@ Import Namespace="System" %>

        <!DOCTYPE html>
        <html xmlns="http://www.w3.org/1999/xhtml">

        <head runat="server">
            <title>Redirecting...</title>
            <script runat="server">
        protected void Page_Load(object sender, EventArgs e)
                {
                    if (Session["UserToken"] != null) {
                        Response.Redirect("~/Pages/Common/Dashboard.aspx");
                    } else {
                        Response.Redirect("~/Pages/Auth/Login.aspx");
                    }
                }
            </script>
        </head>

        <body>
            <form id="form1" runat="server">
            </form>
        </body>

        </html>