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
    public partial class FailureListPublic : System.Web.UI.Page
    {
        private int numOfQPerPage = 10;

	private static readonly string defaultQueryString = "SELECT sendDate, surname, content, archived FROM failure ORDER BY sendDate DESC, surname DESC";

	private string queryStringFilter
	{
		get
		{
			string returnValue = (string)ViewState["queryStringFilter"];
			return returnValue == null ? String.Empty : returnValue;
		}
		set
		{
			ViewState["queryStringFilter"] = value;
		}
	}

        protected void Page_Load(object sender, EventArgs e)
        {
                HtmlGenericControl css;
                css = new HtmlGenericControl();
                css.TagName = "style";
                css.Attributes.Add("type", "text/css");
                css.InnerHtml = "@import \"" + Request.Url.GetLeftPart(UriPartial.Authority) + VirtualPathUtility.ToAbsolute("~/") +"Styles/site.css\";";
                Page.Header.Controls.Add(css);
        }

        protected String getActive()
        {
            String output = "";

            OleDbConnection conn = null;

            conn = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + Server.MapPath("./App_Data/failures.accdb"));
            conn.Open();

            DataSet myDataSet = new DataSet();

            OleDbCommand cm = new OleDbCommand(defaultQueryString + queryStringFilter, conn);

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
                if ((page == currentPage))
                {
                    output += "<tr>";
                    output += "<td>" + r["sendDate"] + "</td>";
                    output += "<td style='border-left: 1px #CCC solid;'>" + r["surname"] + "</td>";
                    output += "<td style='border-left: 1px #CCC solid;'>" + r["content"] + "</td>";
                    output += "<td style='border-left: 1px #CCC solid;'>" + r["archived"] + "</td>";
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

            OleDbCommand cmLogin = new OleDbCommand("SELECT COUNT(*) FROM failure", conn);

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

                if (page > 1)
                {
                    output += "<a href='FailureListPublic.aspx?page=1'>&lt;</a>";
                }

                if (page > 1)
                {
                    output += "<a href='FailureListPublic.aspx?page=" + (page - 1) + "'></a>";
                }

                for (int i = iStart - 1; i < Math.Ceiling(iNum / (decimal)numOfQPerPage) && i < iStart + 8; i++)
                {
                    output += "<a href='FailureListPublic.aspx?page=" + (i + 1) + "" + "'" + ((i + 1) == page ? " class='active'" : "") + ">" + (i + 1) + "</a>";
                }

                if (page != Math.Ceiling(iNum / (decimal)numOfQPerPage))
                {
                    output += "<a href='FailureListPublic.aspx?page=" + (page + 1) + "" + sortBy + "'>></a>";
                }

                if (Math.Ceiling(iNum / (decimal)numOfQPerPage) > 9)
                {
                    output += "<a href='FailureListPublic.aspx?page=" + Math.Ceiling(iNum / (decimal)numOfQPerPage) + "" + sortBy + "'>>></a>";
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
    }
}