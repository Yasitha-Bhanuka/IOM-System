using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI;
using System.Web.Script.Serialization; // Ideally use NewtonSoft, but using System.Web.Script.Serialization for built-in support

public partial class Login : Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserToken"] != null)
        {
            // Redirect if already logged in (Dashboard to be created)
            // Response.Redirect("~/Dashboard.aspx");
        }
    }

    protected async void btnLogin_Click(object sender, EventArgs e)
    {
        string email = txtEmail.Text.Trim();
        string password = txtPassword.Text;

        if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
        {
            lblMessage.Text = "Please enter email and password.";
            lblMessage.Visible = true;
            return;
        }

        try
        {
            // Ideally read from Web.config
            string apiUrl = "https://localhost:7123/api/users/login"; 

            using (var client = new HttpClient())
            {
                var loginData = new { Email = email, Password = password };
                var json = new JavaScriptSerializer().Serialize(loginData);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await client.PostAsync(apiUrl, content);

                if (response.IsSuccessStatusCode)
                {
                    var responseString = await response.Content.ReadAsStringAsync();
                    // Assuming response returns user object or token. 
                    // Deserialize if needed to get Role/Token.
                    
                    Session["UserEmail"] = email;
                    Session["UserToken"] = responseString; // Store token/user object
                    lblMessage.Text = "Login successful!";
                    lblMessage.ForeColor = System.Drawing.Color.Green;
                    lblMessage.Visible = true;
                    
                    // Redirect to Dashboard
                    // Response.Redirect("~/Dashboard.aspx");
                }
                else
                {
                    lblMessage.Text = "Invalid login credentials.";
                    lblMessage.Visible = true;
                }
            }
        }
        catch (Exception ex)
        {
            lblMessage.Text = "Error: " + ex.Message;
            lblMessage.Visible = true;
        }
    }
}
