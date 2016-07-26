using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace web
{
    public partial class Login : System.Web.UI.Page
    {
        public const string pageTitle = "Login Page";
        protected void Page_Load(object sender, EventArgs e)
        {
            #region CSS

            HtmlGenericControl css;
            css = new HtmlGenericControl();
            css.TagName = "style";
            css.Attributes.Add("type", "text/css");
            css.InnerHtml = "@import \"" + Request.Url.GetLeftPart(UriPartial.Authority) + VirtualPathUtility.ToAbsolute("~/") + "Styles/site.css\";";
            Page.Header.Controls.Add(css);

            #endregion

            #region Session
            if (Session["logged"] != null && (Boolean)Session["logged"])
            {
                logged();
            }
            #endregion
        }

        protected void ButtonClick(object sender, EventArgs e)
        {
            #region Database connection
            OleDbConnection connection = null;

            connection = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + Server.MapPath("./App_Data/Users.accdb"));
            connection.Open();
            #endregion


            #region Password checking
            OleDbCommand commandLogin = new OleDbCommand("SELECT COUNT(*) FROM Users WHERE login = '" 
                + TBLogin.Text.ToString() + "'", connection);
            
            if(Convert.ToInt32(commandLogin.ExecuteScalar().ToString()) > 0)
            {
                OleDbCommand commandPassword = new OleDbCommand("SELECT password FROM Users WHERE login = '" 
                    + TBLogin.Text.ToString() + "'", connection);

                if(commandPassword.ExecuteScalar().ToString().Equals(sha256_hash(TBPassword.Text.ToString())))
                {
                    OleDbCommand commandAdmin = new OleDbCommand("SELECT admin FROM Users WHERE login = '" 
                        + TBLogin.Text.ToString() + "'", connection);

                    Session["logged"] = true;
                    Session["logged as"] = TBLogin.Text.ToString();
                    Session["logged_level"] = commandAdmin.ExecuteScalar().ToString();

                    connection.Close();
                    logged();
                } else {
                    connection.Close();
                    errorLogin();
                }
               
            } else {
                connection.Close();
                errorLogin();
            }

            #endregion
        }

        protected void errorLogin()
        {
            Response.Redirect("Login.aspx?login=fail");
        }

        protected void logged()
        {
            Response.Redirect("Default.aspx");
        }

        public static String sha256_hash(String value)
        {
            StringBuilder Sb = new StringBuilder();

            using (SHA256 hash = SHA256Managed.Create())
            {
                Encoding enc = Encoding.UTF8;
                Byte[] result = hash.ComputeHash(enc.GetBytes(value));

                foreach (Byte b in result)
                    Sb.Append(b.ToString("x2"));
            }

            return Sb.ToString();
        }
    }
}