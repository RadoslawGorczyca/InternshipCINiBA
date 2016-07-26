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
    public partial class Archive : System.Web.UI.Page
    {
        public string pageTitle { get; set; }
        private int numOfQPerPage = 10;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["logged"] == null || (Session["logged"] != null && !(Boolean)Session["logged"]))
            {
                Response.Redirect("Login.aspx");
            }
            else
            {
                pageTitle = "Zlecenia Zarchiwizowane";

                HtmlGenericControl css;
                css = new HtmlGenericControl();
                css.TagName = "style";
                css.Attributes.Add("type", "text/css");
                css.InnerHtml = "@import \"" + Request.Url.GetLeftPart(UriPartial.Authority) + VirtualPathUtility.ToAbsolute("~/") + "Styles/site.css\";";
                Page.Header.Controls.Add(css);
            }
        }

        protected void pdfClick(object sender, EventArgs e)
        {
            Response.Redirect("ViewDefaultList.aspx");
        }

        protected String getArchive()
        {
            String output = "";

            OleDbConnection conn = null;

            conn = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + Server.MapPath("./App_Data/failures.accdb"));
            conn.Open();

            DataSet myDataSet = new DataSet();

            OleDbCommand cm = new OleDbCommand("SELECT * FROM failure WHERE archived = TRUE ORDER BY id DESC;", conn);

            IDataReader r = cm.ExecuteReader();
            int page = 1;

            if (Request.QueryString["page"] != null)
            {
                page = Convert.ToInt32(Request.QueryString["page"]);
            }

            int rowID = 0;
            int currentPage = 1;

            while (r.Read())
            {
                if (page == currentPage)
                {
                    String msg = r["content"].ToString();

                    if (msg.Length > 127)
                    {
                        msg = msg.Substring(0, 127) + "...";
                    }

                    output += "<tr>";
                    output += "<td><a href='FailureArchive.aspx?id=" + r["id"].ToString() + "'>" + msg + "</a></td>";
                    output += "<td style='border-left: 1px #CCC solid;'>" + r["surname"].ToString() + "</td>";
                    output += "<td style='border-left: 1px #CCC solid;'>" + r["sendDate"].ToString() + "</td>";
                    output += "<td style='border-left: 1px #CCC solid;'>" + r["owner"].ToString() + "</td>";
                    output += "</tr>";
                }

                ++rowID;

                if (rowID == numOfQPerPage)
                {
                    rowID = 0;
                    ++currentPage;
                    if (page < currentPage)
                    {
                        break;
                    }
                }
            }

            conn.Close();

            return output;
        }

        public String getPagination()
        {
            String output = "";

            OleDbConnection conn = null;

            conn = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + Server.MapPath("./App_Data/failures.accdb"));
            conn.Open();

            DataSet myDataSet = new DataSet();

            OleDbCommand cmLogin = new OleDbCommand("SELECT COUNT(*) FROM failure WHERE archived = TRUE", conn);

            int iNum = Convert.ToInt32(cmLogin.ExecuteScalar().ToString());

            if (Math.Ceiling(iNum / (decimal)numOfQPerPage) <= 1)
            {
                return "";
            }
            else
            {
                int page = 1;

                if (Request.QueryString["page"] != null)
                {
                    page = Convert.ToInt32(Request.QueryString["page"]);
                }

                int iStart = (Math.Ceiling(iNum / (decimal)numOfQPerPage) > 9 && page > 3 ? (int)Math.Ceiling((decimal)page / 2) : 1);

                if (page > 3)
                {
                    output += "<a href='Archive.aspx?page=1'><<</a>";
                }

                if (page > 1)
                {
                    output += "<a href='Archive.aspx?page=" + (page - 1) + "'><</a>";
                }

                for (int i = iStart - 1; i < Math.Ceiling(iNum / (decimal)numOfQPerPage) && i < iStart + 8; i++)
                {
                    output += "<a href='Archive.aspx?page=" + (i + 1) + "'" + ((i + 1) == page ? " class='active'" : "") + ">" + (i + 1) + "</a>";
                }

                if (page != Math.Ceiling(iNum / (decimal)numOfQPerPage))
                {
                    output += "<a href='Archive.aspx?page=" + (page + 1) + "'>></a>";
                }

                if (Math.Ceiling(iNum / (decimal)numOfQPerPage) > 9)
                {
                    output += "<a href='Archive.aspx?page=" + Math.Ceiling(iNum / (decimal)numOfQPerPage) + "'>>></a>";
                }
            }

            conn.Close();

            return output;
        }
    }
}