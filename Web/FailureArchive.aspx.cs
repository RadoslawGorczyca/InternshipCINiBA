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
    public partial class FailureArchive : System.Web.UI.Page
    {
        public string pageTitle { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["logged"] == null || (Session["logged"] != null && !(Boolean)Session["logged"]))
            {
                Response.Redirect("Login.aspx");
            }
            else if (Request.QueryString["id"] == null)
            {
                Response.Redirect("ManageUsers.aspx");
            }
            else if (Request.QueryString["id"].Equals("1"))
            {
                Response.Redirect("ManageUsers.aspx");
            }
            else
            {
                pageTitle = "Zarchiwizowane";

                HtmlGenericControl css;
                css = new HtmlGenericControl();
                css.TagName = "style";
                css.Attributes.Add("type", "text/css");
                css.InnerHtml = "@import \"" + Request.Url.GetLeftPart(UriPartial.Authority) + VirtualPathUtility.ToAbsolute("~/") + "Styles/site.css\";";
                Page.Header.Controls.Add(css);

                OleDbConnection conn = null;

                conn = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + Server.MapPath("./App_Data/failures.accdb"));
                conn.Open();

                OleDbCommand cmComments = new OleDbCommand("SELECT comments FROM failure WHERE id = " + Request.QueryString["id"] + "", conn);
                OleDbCommand cmMail = new OleDbCommand("SELECT email FROM failure WHERE id = " + Request.QueryString["id"] + "", conn);
                OleDbCommand cmName = new OleDbCommand("SELECT surname FROM failure WHERE id = " + Request.QueryString["id"] + "", conn);
                OleDbCommand cmDate = new OleDbCommand("SELECT sendDate FROM failure WHERE id = " + Request.QueryString["id"] + "", conn);
                OleDbCommand cmFloor = new OleDbCommand("SELECT floor FROM failure WHERE id = " + Request.QueryString["id"] + "", conn);
                OleDbCommand cmArea = new OleDbCommand("SELECT area FROM failure WHERE id = " + Request.QueryString["id"] + "", conn);
                OleDbCommand cmStand = new OleDbCommand("SELECT stand FROM failure WHERE id = " + Request.QueryString["id"] + "", conn);
                OleDbCommand cmFailure = new OleDbCommand("SELECT content FROM failure WHERE id = " + Request.QueryString["id"] + "", conn);
                OleDbCommand cmForwarded = new OleDbCommand("SELECT forwarded FROM failure WHERE id = " + Request.QueryString["id"] + "", conn);
                OleDbCommand cmOwner = new OleDbCommand("SELECT owner FROM failure WHERE id = " + Request.QueryString["id"] + "", conn);
                OleDbCommand cmTemat = new OleDbCommand("SELECT topic FROM failure WHERE id = " + Request.QueryString["id"] + "", conn);
                OleDbCommand cmArchive = new OleDbCommand("SELECT archived FROM failure WHERE id = " + Request.QueryString["id"] + "", conn);


                if (TOldComment.Text.Equals(""))
                {
                    TOldComment.Text = cmComments.ExecuteScalar().ToString();
                    TMail.Text = cmMail.ExecuteScalar().ToString();
                    TName.Text = cmName.ExecuteScalar().ToString();
                    TDate.Text = cmDate.ExecuteScalar().ToString();
                    TFloor.Text = cmFloor.ExecuteScalar().ToString();
                    TArea.Text = cmArea.ExecuteScalar().ToString();
                    TStand.Text = cmStand.ExecuteScalar().ToString();
                    TFailure.Text = cmFailure.ExecuteScalar().ToString();
                    TTemat.Text = cmTemat.ExecuteScalar().ToString();
                    TForwarded.Text = cmForwarded.ExecuteScalar().ToString();
                }

                conn.Close();
            }
        }
        protected void pdfClick(object sender, EventArgs e)
        {
            Response.Redirect("ViewDefault.aspx");
        }

        protected void BBack_Click(object sender, EventArgs e)
        {
            OleDbConnection conn = null;

            conn = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + Server.MapPath("./App_Data/failures.accdb"));
            conn.Open();

            OleDbCommand cm = new OleDbCommand("UPDATE failure SET archived = " + (cmArchive.Checked ? "TRUE" : "FALSE"), conn);

            cm.ExecuteScalar();

            conn.Close();

            Response.Redirect("Archive.aspx");
        }
    }
}