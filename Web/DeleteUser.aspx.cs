/* -- Jakowski Lipiec 2014 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using System.Data.OleDb;

using System.Web.UI.HtmlControls;

namespace web
{
    public partial class DeleteUser : System.Web.UI.Page
    {
        public string pageTitle { get; set; }

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
            else if(Request.QueryString["id"] == null) {
                Response.Redirect("ManageUsers.aspx");
            }
            else if (Request.QueryString["id"].Equals("1"))
            {
                Response.Redirect("ManageUsers.aspx");
            }
            else
            {
                pageTitle = "Usuwanie użytkownika";

                HtmlGenericControl css;
                css = new HtmlGenericControl();
                css.TagName = "style";
                css.Attributes.Add("type", "text/css");
                css.InnerHtml = "@import \"" + Request.Url.GetLeftPart(UriPartial.Authority) + VirtualPathUtility.ToAbsolute("~/") + "Styles/site.css\";";
                Page.Header.Controls.Add(css);
            }
        }

        public String getLogin()
        {
            string id = Request.QueryString["id"];

            OleDbConnection conn = null;

            conn = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + Server.MapPath("./App_Data/Users.accdb"));
            conn.Open();

            OleDbCommand cmLogin = new OleDbCommand("SELECT login FROM users WHERE id = " + id + "", conn);

            String output = "";

            output = cmLogin.ExecuteScalar().ToString();

            conn.Close();

            return output;
        }

        protected void BDelete_Click(object sender, EventArgs e)
        {
            string id = Request.QueryString["id"];

            OleDbConnection conn = null;

            conn = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + Server.MapPath("./App_Data/Users.accdb"));
            conn.Open();

            OleDbCommand cm = new OleDbCommand("DELETE FROM users WHERE id = " + id + "", conn);

            cm.ExecuteScalar();

            conn.Close();

            Response.Redirect("ManageUsers.aspx?userDeleted=true");
        }

        protected void BNo_Click(object sender, EventArgs e)
        {
            Response.Redirect("ManageUsers.aspx");
        }
    }
}