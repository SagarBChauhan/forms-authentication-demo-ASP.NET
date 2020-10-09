using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using System.Configuration;
using System.Data.SqlClient;

namespace SecurityApp
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            if(AuthenticateUser(txtUserName.Text,txtPassword.Text))
            {
                FormsAuthentication.
                    RedirectFromLoginPage(txtUserName.Text,true);
            }
            else
            {
                lblMessage.Text = "Invalid Username and password";
            }
        }

        private bool AuthenticateUser(string UserName, string Password)
        {
            string ConnectionString = ConfigurationManager.ConnectionStrings["UserConnectionString"].ConnectionString;

            SqlConnection objConnection = new SqlConnection(ConnectionString);
            objConnection.Open();

            SqlCommand objCommand = new SqlCommand();
            objCommand.Connection = objConnection;
            objCommand.CommandText = "spUserLogin";
            objCommand.CommandType = System.Data.CommandType.StoredProcedure;

            string EncryptedPassword = FormsAuthentication.HashPasswordForStoringInConfigFile(txtPassword.Text, "SHA1");
            objCommand.Parameters.AddWithValue("@UserName", txtUserName.Text);
            objCommand.Parameters.AddWithValue("@Password", EncryptedPassword);

            int Result = (int)objCommand.ExecuteScalar();
            return Result == 1;

        }
    }
}