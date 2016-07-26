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
    public partial class FailureService : System.Web.UI.Page
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
                pageTitle = "Zlecenia własne i nowe";

                HtmlGenericControl css;
                css = new HtmlGenericControl();
                css.TagName = "style";
                css.Attributes.Add("type", "text/css");
                css.InnerHtml = "@import \"" + Request.Url.GetLeftPart(UriPartial.Authority) + VirtualPathUtility.ToAbsolute("~/") +"Styles/site.css\";";
                Page.Header.Controls.Add(css);
            }
        }

        protected void pdfClick(object sender, EventArgs e)
        {
            Response.Redirect("ViewDefaultList.aspx");
        }


        protected String getActive()
        {
            String output = "";

            OleDbConnection conn = null;

            conn = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + Server.MapPath("./App_Data/failures.accdb"));
            conn.Open();

            DataSet myDataSet = new DataSet();

            String sortBy = "id DESC";

            if (Request.QueryString["sortby"] != null)
            {
                if (Request.QueryString["sortby"].Equals("surname"))
                {
                    sortBy = "surname";
                }
                else
                {
                    sortBy = "surname DESC";
                }
            }
            OleDbCommand cm = new OleDbCommand("SELECT * FROM failure WHERE archived = FALSE ORDER BY " + sortBy + ";", conn);

            IDataReader r = cm.ExecuteReader();
            int page = 1;

            if (Request.QueryString["page"] != null)
            {
                page = Convert.ToInt32(Request.QueryString["page"]);
            }

            int rowID = 0;
            int currentPage = 1;
            String log = FirstCharToUpper((String)Session["logged as"]);
            while (r.Read())
            {
                if ((page == currentPage))
                {
                    if (((r["owner"].ToString() == log) || (r["owner"].ToString() == "")))
                    {
                    Boolean bWIP = !r["comments"].Equals("");

                    String msg = r["content"].ToString();

                    if (msg.Length > 127)
                    {
                        msg = msg.Substring(0, 127) + "...";
                    }

                    output += "<tr>";
                    output += "<td" + (bWIP ? " class='withComment' style='background: #F7F7F7;'" : "") + "><a href='Failure.aspx?id=" + r["id"].ToString() + "'>" + msg + "</a></td>";
                    output += "<td style='border-left: 1px #CCC solid;" + (bWIP ? " background: #F7F7F7;" : "") + "'>" + r["surname"].ToString() + "</td>";
                    output += "<td style='border-left: 1px #CCC solid;" + (bWIP ? " background: #F7F7F7;" : "") + "'>" + r["sendDate"].ToString() + "</td>";
                    output += "<td style='border-left: 1px #CCC solid;" + (bWIP ? " background: #F7F7F7;" : "") + "'>" + r["owner"].ToString() + "</td>";
                    output += "</tr>";
                }
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

            OleDbCommand cmLogin = new OleDbCommand("SELECT COUNT(*) FROM failure WHERE archived = FALSE", conn);

            int iNum = Convert.ToInt32(cmLogin.ExecuteScalar().ToString());

            if (Math.Ceiling(iNum / (decimal)numOfQPerPage) <= 1)
            {
                return "";
            }
            else
            {
                int page = 1;

                String sortBy = "";

                if (Request.QueryString["sortby"] != null)
                {
                    if (Request.QueryString["sortby"].Equals("name"))
                    {
                        sortBy = "&sortby=name";
                    }
                    else
                    {
                        sortBy = "&sortby=named";
                    }
                }

                if (Request.QueryString["page"] != null)
                {
                    page = Convert.ToInt32(Request.QueryString["page"]);
                }

                int iStart = (Math.Ceiling(iNum / (decimal)numOfQPerPage) > 9 && page > 3 ? (int)Math.Ceiling((decimal)page / 2) : 1);

                if (page > 3)
                {
                    output += "<a href='FailureService.aspx?page=1" + sortBy + "'><<</a>";
                }

                if (page > 1)
                {
                    output += "<a href='FailureService.aspx?page=" + (page - 1) + "" + sortBy + "'><</a>";
                }

                for (int i = iStart - 1; i < Math.Ceiling(iNum / (decimal)numOfQPerPage) && i < iStart + 8; i++)
                {
                    output += "<a href='FailureService.aspx?page=" + (i + 1) + "" + sortBy + "'" + ((i + 1) == page ? " class='active'" : "") + ">" + (i + 1) + "</a>";
                }

                if (page != Math.Ceiling(iNum / (decimal)numOfQPerPage))
                {
                    output += "<a href='FailureService.aspx?page=" + (page + 1) + "" + sortBy + "'>></a>";
                }

                if (Math.Ceiling(iNum / (decimal)numOfQPerPage) > 9)
                {
                    output += "<a href='FailureService.aspx?page=" + Math.Ceiling(iNum / (decimal)numOfQPerPage) + "" + sortBy + "'>>></a>";
                }
            }

            conn.Close();

            return output;
        }

        public static string FirstCharToUpper(string input)
        {
            if (String.IsNullOrEmpty(input))
                throw new ArgumentException("ARGH!");
            return input.First().ToString().ToUpper() + input.Substring(1);
        }
        public String getSort()
        {
            if (Request.QueryString["sortby"] != null && Request.QueryString["sortby"].Equals("name"))
            {
                return "?sortby=named";
            }
            else
            {
                return "?sortby=name";
            }
        }
    }
}