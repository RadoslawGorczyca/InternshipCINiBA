using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.OleDb;

using System.Web.Hosting;
using System.Web.UI.HtmlControls;
using System.Net.Mail;

namespace web
{
    public partial class ProblemReported : System.Web.UI.Page
    {
        public string pageTitle { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
                pageTitle = "Zgłoszenie przyjęte";

                HtmlGenericControl css;
                css = new HtmlGenericControl();
                css.TagName = "style";
                css.Attributes.Add("type", "text/css");
                css.InnerHtml = "@import \"" + Request.Url.GetLeftPart(UriPartial.Authority) + VirtualPathUtility.ToAbsolute("~/") + "Styles/site.css\";";
                Page.Header.Controls.Add(css);
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Response.Redirect("Default.aspx");
        }
    }
}