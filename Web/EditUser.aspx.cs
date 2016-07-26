using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using System.Data.OleDb;

using System.Web.UI.HtmlControls;

using System.Text;
using System.Security.Cryptography;

namespace web
{
    public partial class EditUser : System.Web.UI.Page
    {
        public Boolean loaded;

        public string pageTitle { get; set; }
        public int minPasswordLength { get; set; }
        public int minLoginLength { get; set; }
        public int minEmailLength { get; set; }
        public int minSurnameLength { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["logged"] == null || (Session["logged"] != null && !(Boolean)Session["logged"]))
            {
                Response.Redirect("Login.aspx");
            }
            else if (Session["logged_level"].ToString().Equals(false.ToString()))
            {
                Response.Redirect("Logout.aspx");
            }
            else
            {
                pageTitle = "Edycja użytkownika";
                minPasswordLength = 6;
                minLoginLength = 3;
                minEmailLength = 3;
                minSurnameLength = 2;

                HtmlGenericControl css;
                css = new HtmlGenericControl();
                css.TagName = "style";
                css.Attributes.Add("type", "text/css");
                css.InnerHtml = "@import \"" + Request.Url.GetLeftPart(UriPartial.Authority) + VirtualPathUtility.ToAbsolute("~/") + "Styles/site.css\";";
                Page.Header.Controls.Add(css);

                string id = Request.QueryString["id"];

                OleDbConnection conn = null;

                conn = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + Server.MapPath("./App_Data/Users.accdb"));
                conn.Open();

                OleDbCommand cmLogin = new OleDbCommand("SELECT login FROM users WHERE id = " + id + "", conn);
                OleDbCommand cmSurname = new OleDbCommand("SELECT surname FROM users WHERE id = " + id + "", conn);
                OleDbCommand cmEmail = new OleDbCommand("SELECT email FROM users WHERE id = " + id + "", conn);
                OleDbCommand cmAdmin = new OleDbCommand("SELECT admin FROM users WHERE id = " + id + "", conn);
                
                if(TNLogin.Text.Equals("")) {
                    TNLogin.Text = cmLogin.ExecuteScalar().ToString();
                    TNSurname.Text = cmSurname.ExecuteScalar().ToString();
                    TNEmail.Text = cmEmail.ExecuteScalar().ToString();
                    CBAdmin.Checked = cmAdmin.ExecuteScalar().ToString().Equals(true.ToString());
                }
                conn.Close();
            }
        }

        protected void BSave_Click(object sender, EventArgs e)
        {
            if (TNLogin.Text.ToString().Equals("") || (TNPassword.Text.ToString().Equals("") && !TNPassword2.Text.ToString().Equals("")) || (!TNPassword.Text.ToString().Equals("") && TNPassword2.Text.ToString().Equals("")))
            {
                Response.Redirect("EditUser.aspx?id=" + Request.QueryString["id"] + "&empty=true");
            }
            else if (TNLogin.Text.ToString().Length < minLoginLength)
            {
                Response.Redirect("EditUser.aspx?id=" + Request.QueryString["id"] + "&loginLength=true");
            }
            else if ((TNPassword.Text.ToString().Equals("") && !TNPassword2.Text.ToString().Equals("")) || (!TNPassword.Text.ToString().Equals("") && TNPassword2.Text.ToString().Equals("")) && TNPassword.Text.ToString().Length < minPasswordLength)
            {
                Response.Redirect("EditUser.aspx?id=" + Request.QueryString["id"] + "&passwordLength=true");
            }
            else if (!TNPassword.Text.ToString().Equals(TNPassword2.Text.ToString()))
            {
                Response.Redirect("EditUser.aspx?id=" + Request.QueryString["id"] + "&badPassword=true");
            }
            else if (TNEmail.Text.ToString().Length < minEmailLength)
            {
                Response.Redirect("EditUser.aspx?id=" + Request.QueryString["id"] + "&emailLength=true");
            }
            else if (TNSurname.Text.ToString().Length < minSurnameLength)
            {
                Response.Redirect("EditUser.aspx?id=" + Request.QueryString["id"] + "&surnameLength=true");
            }
            {
                saveUser(TNLogin.Text.ToString(), TNPassword.Text.ToString(), TNSurname.Text.ToString(), TNEmail.Text.ToString());
            }
        }

        private void saveUser(String login, String password, String surname, String email)
        {
            OleDbConnection conn = null;

            conn = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + Server.MapPath("./App_Data/Users.accdb"));
            conn.Open();

            OleDbCommand cm = null;

            if (!password.Equals(""))
            {
                cm = new OleDbCommand("UPDATE users SET login = '" + login + "', surname = '" + surname + "', email = '" + email + "', [password] = '" + sha256_hash(password) + "', admin = " + (CBAdmin.Checked || Request.QueryString["id"].Equals("1") ? "TRUE" : "FALSE") + " WHERE id = " + Request.QueryString["id"], conn);
            }
            else
            {
                cm = new OleDbCommand("UPDATE users SET login = '" + login + "', surname = '" + surname + "', email = '" + email + "', admin = " + (CBAdmin.Checked || Request.QueryString["id"].Equals("1") ? "TRUE" : "FALSE") + " WHERE id = " + Request.QueryString["id"], conn);
            }
            
            cm.ExecuteScalar();

            conn.Close();

            Response.Redirect("ManageUsers.aspx?userEdited=true");
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
        public static string FirstCharToUpper(string input)
        {
            if (String.IsNullOrEmpty(input))
                throw new ArgumentException("ARGH!");
            return input.First().ToString().ToUpper() + input.Substring(1);
        }
    }
}