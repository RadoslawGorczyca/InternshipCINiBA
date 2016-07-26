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
    public partial class ManageUsers : System.Web.UI.Page
    {
        public string pageTitle { get; set; }
        private int numOfUsersPerPage = 5;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["logged"] == null || (Session["logged"] != null && !(Boolean)Session["logged"]))
            {
                Response.Redirect("Login.aspx");
            }
            else if (Session["logged_level"].ToString().Equals(false.ToString()))
            {
                Response.Redirect("LogOut.aspx");
            }
            else
            {
                pageTitle = "Zarządzanie użytkownikami";

                HtmlGenericControl css;
                css = new HtmlGenericControl();
                css.TagName = "style";
                css.Attributes.Add("type", "text/css");
                css.InnerHtml = "@import \"" + Request.Url.GetLeftPart(UriPartial.Authority) + VirtualPathUtility.ToAbsolute("~/") + "Styles/site.css\";";
                Page.Header.Controls.Add(css);
            }
        }

        public String getUsers()
        {
            String output = "";

            OleDbConnection conn = null;

            conn = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + Server.MapPath("./App_Data/Users.accdb"));
            conn.Open();

            DataSet myDataSet = new DataSet();

            OleDbCommand cm = new OleDbCommand("SELECT * FROM users", conn);

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
                    output += "<tr>";
                    output += "<td><a href='EditUser.aspx?id=" + r["id"].ToString() + "'>" + r["login"].ToString() + "</a></td>";
                    output += "<td align='center'><a href='EditUser.aspx?id=" + r["id"].ToString() + "' class='badge'>Edytuj</a></td>";
                    output += "<td align='center'>" + (r["id"].ToString().Equals("1") ? "" : "<a href='DeleteUser.aspx?id=" + r["id"].ToString() + "' class='badge'>Usuń</a> ") + "</td>";
                    output += "</tr>";
                }

                ++rowID;

                if (rowID == numOfUsersPerPage)
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

            conn = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + Server.MapPath("./App_Data/Users.accdb"));
            conn.Open();

            DataSet myDataSet = new DataSet();

            OleDbCommand cmUsername = new OleDbCommand("SELECT COUNT(*) FROM users", conn);

            int iNum = Convert.ToInt32(cmUsername.ExecuteScalar().ToString());

            if (Math.Ceiling(iNum / (decimal)numOfUsersPerPage) <= 1)
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

                int iStart = (Math.Ceiling(iNum / (decimal)numOfUsersPerPage) > 9 && page > 3 ? (int)Math.Ceiling((decimal)page / 2) : 1);

                if (page > 3)
                {
                    output += "<a href='ManageUsers.aspx?page=1'><<</a>";
                }

                if (page > 1)
                {
                    output += "<a href='ManageUsers.aspx?page=" + (page - 1) + "'><</a>";
                }

                for (int i = iStart - 1; i < Math.Ceiling(iNum / (decimal)numOfUsersPerPage) && i < iStart + 8; i++)
                {
                    output += "<a href='ManageUsers.aspx?page=" + (i + 1) + "'" + ((i + 1) == page ? " class='active'" : "") + ">" + (i + 1) + "</a>";
                }

                if (page != Math.Ceiling(iNum / (decimal)numOfUsersPerPage))
                {
                    output += "<a href='ManageUsers.aspx?page=" + (page + 1) + "'>></a>";
                }

                if (Math.Ceiling(iNum / (decimal)numOfUsersPerPage) > 9)
                {
                    output += "<a href='ManageUsers.aspx?page=" + Math.Ceiling(iNum / (decimal)numOfUsersPerPage) + "'>>></a>";
                }
            }

            conn.Close();

            return output;
        }

        protected String getNumOfUsers()
        {
            OleDbConnection conn = null;

            conn = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + Server.MapPath("./App_Data/Users.accdb"));
            conn.Open();

            OleDbCommand cmUsername = new OleDbCommand("SELECT COUNT(*) FROM users", conn);

            int iNum = Convert.ToInt32(cmUsername.ExecuteScalar().ToString());

            conn.Close();

            return "" + iNum;
        }
    }
}