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
    public partial class AddNewUser : System.Web.UI.Page
    {
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
                pageTitle = "Dodawanie nowego użytkownika";
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
            }
        }

        protected void BAdd_Click(object sender, EventArgs e)
        {
            OleDbConnection conn = null;

            conn = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + Server.MapPath("./App_Data/Users.accdb"));
            conn.Open();

            OleDbCommand cmLogin = new OleDbCommand("SELECT COUNT(*) FROM users WHERE login = '" + TNLogin.Text.ToString() + "'", conn);

            int result = Convert.ToInt32(cmLogin.ExecuteScalar().ToString());

            conn.Close();

            if (TNLogin.Text.ToString().Equals("") || TNPassword.Text.ToString().Equals("") || TNPassword2.Text.ToString().Equals("") || TNEmail.Text.ToString().Equals("") || TNSurname.Text.ToString().Equals(""))
            {
                Response.Redirect("AddNewUser.aspx?empty=true");
            }
            else if (TNLogin.Text.ToString().Length < minLoginLength)
            {
                Response.Redirect("AddNewUser.aspx?usernameLength=true");
            }
            else if (TNPassword.Text.ToString().Length < minPasswordLength)
            {
                Response.Redirect("AddNewUser.aspx?passwordLength=true");
            }
            else if (result > 0)
            {
                Response.Redirect("AddNewUser.aspx?userExists=true");
            }
            else if (!TNPassword.Text.ToString().Equals(TNPassword2.Text.ToString()))
            {
                Response.Redirect("AddNewUser.aspx?badPassword=true");
            }
            else if (TNEmail.Text.ToString().Length < minEmailLength)
            {
                Response.Redirect("AddNewUser.aspx?emailLength=true");
            }
            else if (TNSurname.Text.ToString().Length < minSurnameLength)
            {
                Response.Redirect("AddNewUser.aspx?surnameLength=true");
            }
            else
            {
                addUser(TNLogin.Text.ToString(), TNPassword.Text.ToString(), TNSurname.Text.ToString(), TNEmail.Text.ToString());
            }
        }

        private void addUser(String login, String password, String surname, String email)
        {
            OleDbConnection conn = null;

            conn = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + Server.MapPath("./App_Data/Users.accdb"));
            conn.Open();

            OleDbCommand cm = new OleDbCommand("INSERT INTO users (login, surname, email, [password], admin) values ('" + FirstCharToUpper(login) + "', '" + FirstCharToUpper(surname) + "', '" + email + "', '" + sha256_hash(password) + "', " + (CBAdmin.Checked ? "TRUE" : "FALSE") + ")", conn);

            cm.ExecuteScalar();

            conn.Close();

            Response.Redirect("ManageUsers.aspx?userAdded=true");
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