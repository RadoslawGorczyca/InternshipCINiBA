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
    public partial class Default : System.Web.UI.Page
    {
        public string pageTitle { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["logged"] == null || (Session["logged"] != null && !(Boolean)Session["logged"]))
            {
               Response.Redirect("DefaultBefore.aspx");
            }
            else
            {

                pageTitle = "Zgłaszenie Awarii";

                HtmlGenericControl css;
                css = new HtmlGenericControl();
                css.TagName = "style";
                css.Attributes.Add("type", "text/css");
                css.InnerHtml = "@import \"" + Request.Url.GetLeftPart(UriPartial.Authority) + VirtualPathUtility.ToAbsolute("~/") + "Styles/site.css\";";
                Page.Header.Controls.Add(css);
                
                String log = FirstCharToUpper(Session["logged as"].ToString());

                OleDbConnection conn = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + Server.MapPath("./App_Data/Users.accdb"));
                conn.Open();

                OleDbCommand cmSurname = new OleDbCommand("SELECT surname FROM Users WHERE login = '" + log + "'", conn);
                OleDbCommand cmEmail = new OleDbCommand("SELECT email FROM Users WHERE login = '" + log + "'", conn);

                if (Session["logged_level"].ToString().Equals(true.ToString()))
                {
                TN.Text = cmSurname.ExecuteScalar().ToString();
                TM.Text = cmEmail.ExecuteScalar().ToString();

                conn.Close();
                }
                else
                if (Session["logged_level"].ToString().Equals(false.ToString()))
                {
                    TN.Text = cmSurname.ExecuteScalar().ToString();
                    TM.Text = cmEmail.ExecuteScalar().ToString();

                    conn.Close();
                }
             }

        }

        protected void BSend_Click(object sender, EventArgs e)
        {
            String date = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss").ToString();

            String C = TC.Text;
            String N = TN.Text;
            String M = TM.Text;
            String F = Floor.Text;
            String A = Area.Text;
            String S = Stand.Text;

            if (C.Length > 4 && N.Length > 2 && M.Length > 2 && F.Length > 2 && A.Length > 2 && S.Length > 2)
            {
                OleDbConnection cnn = null;

                cnn = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + Server.MapPath("./App_Data/Failures.accdb"));
                cnn.Open();

                OleDbCommand cm = new OleDbCommand("INSERT INTO failure (content, surname, email, floor, area, stand, sendDate) values ('" + C + "', '" + N + "', '" + M + "', '" + F + "','" + A + "', '" + S + "', '" + date + "')", cnn);

                cm.ExecuteNonQuery();

                cnn.Close();
                /*
                string system_mail = "system@o2.pl";
                string system_password = "system";
                string adminstration_mail = "admin@o2.pl";

                SmtpClient client = new SmtpClient
                {
                    Host = "poczta.o2.pl",
                    Port = 587,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    Credentials = new System.Net.NetworkCredential(system_mail, system_password),
                    Timeout = 10000,
                };
                MailMessage customer = new MailMessage(system_mail, M, "Zgłoszenie awarii", "Awaria została pomyślnie zgłoszona.");
                client.Send(customer);

                MailMessage administration = new MailMessage(system_mail, adminstration_mail, "Zgłoszenie awarii", "Nowe zgłoszenie awarii oczekujące na przjęcie.");
                client.Send(administration);
                */
                TC.Text = "";
                

                Response.Redirect("ProblemReported.aspx");
            }
            else
            {

            }
        }
        public static string FirstCharToUpper(string input)
        {
            if (String.IsNullOrEmpty(input))
                throw new ArgumentException("ARGH!");
            return input.First().ToString().ToUpper() + input.Substring(1);
        }
        protected void Click(object sender, EventArgs e)
        {
            //Do nothing lol
        }

        protected void RadioButtonList1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}