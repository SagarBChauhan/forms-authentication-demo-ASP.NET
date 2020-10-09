using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.Security;

namespace SecurityApp.Register
{
    public partial class Registration : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnRegister_Click(object sender, EventArgs e)
        {
            string ConnectionString = ConfigurationManager.ConnectionStrings["UserConnectionString"].ConnectionString;

            SqlConnection objConnection = new SqlConnection(ConnectionString);
            objConnection.Open();

            SqlCommand objCommand = new SqlCommand();
            objCommand.Connection = objConnection;
            objCommand.CommandText = "spRegisterUser";
            objCommand.CommandType = System.Data.CommandType.StoredProcedure;

            string EncryptedPassword = FormsAuthentication.HashPasswordForStoringInConfigFile(txtPassword.Text, "SHA1");
            objCommand.Parameters.AddWithValue("@UserName", txtUserName.Text);
            objCommand.Parameters.AddWithValue("@Password", EncryptedPassword);
            objCommand.Parameters.AddWithValue("@Email", txtEmail.Text);

            int Result =Convert.ToInt32(objCommand.ExecuteScalar());
            if (Result > 0)
            {
                Response.Redirect("~/Login.aspx");
            }
        }
    }
}