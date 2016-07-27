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

                if (RBDotyczy.SelectedValue.ToString() == "1")
                {
                    Floor.Enabled = false;
                    Area.Enabled = false;
                    Stand.Enabled = false;
                    TTemat.Enabled = true;
                    TabelaSprzet.Visible = false;
                    TabelaOprogramowanie.Visible = true;
                    
                }
                if (RBDotyczy.SelectedValue.ToString() == "0")
                {
                    Floor.Enabled = true;
                    Area.Enabled = true;
                    Stand.Enabled = true;
                    TTemat.Enabled = false;
                    TabelaSprzet.Visible = true;
                    TabelaOprogramowanie.Visible = false;
                }

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
            String T = TTemat.Text;

            int ID;

            if (RBDotyczy.SelectedValue.ToString() == "0")
            {
                if (C.Length > 2 && N.Length > 2 && M.Length > 2 && F.Length > 2 && A.Length > 2 && S.Length > 2)
                {

                    OleDbConnection cnn = null;

                    cnn = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + Server.MapPath("./App_Data/Failures.accdb"));
                    cnn.Open();

                    OleDbCommand cm = new OleDbCommand("INSERT INTO failure (what, content, surname, email, floor, area, stand, sendDate) values ('SPRZĘT', '" + C + "', '" + N + "', '" + M + "', '" + F + "','" + A + "', '" + S + "', '" + date + "')", cnn);

                    cm.ExecuteNonQuery();


                    string to = M;
                    string from = "system@ciniba.edu.pl";
                    string system = "d-i@ciniba.edu.pl";
                    MailMessage person = new MailMessage(from, to);
                    MailMessage admin = new MailMessage(from, system);
                    SmtpClient client = new SmtpClient("155.158.102.75", 25);
                    client.UseDefaultCredentials = true;

                    person.Subject = "Zgłoszenie awarii sprzętu - " + F + "/" + A + "/" + S;
                    person.Body = @"Zgłoszenie awarii wysłane!" + "\n\n" + N + "\n" + M + "\n\n" + F + "/" + A + "/" + S + "\n\n" + "Treść: " + C;
                    admin.Subject = "Zgłoszenie awarii sprzętu - " + F + "/" + A + "/" + S;
                    admin.Body = @"Zgłoszenie awarii wysłane!" + "\n\n" + N + "\n" + M + "\n\n" + F + "/" + A + "/" + S + "\n\n" + "Treść: " + C;

                    client.Send(person);
                    client.Send(admin);

                    cm = null;
                    cm = new OleDbCommand("Select @@Identity", cnn);
                    ID = (int)cm.ExecuteScalar();

                    cnn.Close();

                    String url = "ProblemReported.aspx?ID=" + ID;

                    TC.Text = "";

                    Response.Redirect(url);

                }
                else
                {
                    Error.Visible = true;
                }
            }

            if (RBDotyczy.SelectedValue.ToString() == "1")
            {
                if (C.Length > 2 && N.Length > 2 && M.Length > 2 && T.Length > 2)
                {

                    OleDbConnection cnn = null;

                    cnn = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + Server.MapPath("./App_Data/Failures.accdb"));
                    cnn.Open();

                    OleDbCommand cm = new OleDbCommand("INSERT INTO failure (what, content, surname, email, floor, area, stand, sendDate) values ('OPROGRAMOWANIE', '" + C + "', '" + N + "', '" + M + "', '" + F + "','" + A + "', '" + S + "', '" + date + "')", cnn);

                    cm.ExecuteNonQuery();


                    string to = M;
                    string from = "system@ciniba.edu.pl";
                    string system = "d-i@ciniba.edu.pl";
                    MailMessage person = new MailMessage(from, to);
                    MailMessage admin = new MailMessage(from, system);
                    SmtpClient client = new SmtpClient("155.158.102.75", 25);
                    client.UseDefaultCredentials = true;

                    person.Subject = "Zgłoszenie awarii oprogramowania - " + T;
                    person.Body = @"Zgłoszenie awarii wysłane!" + "\n\n" + N + "\n" + M + "\n\n" + "Temat: " + T + "\n" + "Treść: " + C;
                    admin.Subject = "Zgłoszenie awarii oprogramowania - " + T;
                    admin.Body = @"Nowe zgłoszenie awarii!" + "\n\n" + N + "\n" + M + "\n\n" + "Temat: " + T + "\n" + "Treść: " + C;

                    client.Send(person);
                    client.Send(admin);


                    cm = null;
                    cm = new OleDbCommand("Select @@Identity", cnn);
                    ID = (int)cm.ExecuteScalar();

                    cnn.Close();

                    String url = "ProblemReported.aspx?ID=" + ID;

                    TC.Text = "";

                    Response.Redirect(url); ;



                }
                else
                {
                    Error.Visible = true;
                }
            }
        }
        public static string FirstCharToUpper(string input)
        {
            if (String.IsNullOrEmpty(input))
                throw new ArgumentException("ARGH!");
            return input.First().ToString().ToUpper() + input.Substring(1);
        }
        protected void RadioButtonList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdatePanel2.Update();
            UpdatePanel3.Update();
            UpdatePanel4.Update();

            if (RBDotyczy.SelectedValue.ToString() == "1")
            {
                Floor.Enabled = false;
                Area.Enabled = false;
                Stand.Enabled = false;
                TTemat.Enabled = true;
                TabelaSprzet.Visible = false;
                TabelaOprogramowanie.Visible = true;
            }
            if (RBDotyczy.SelectedValue.ToString() == "0")
            {
                Floor.Enabled = true;
                Area.Enabled = true;
                Stand.Enabled = true;
                TTemat.Enabled = false;
                TabelaSprzet.Visible = true;
                TabelaOprogramowanie.Visible = false;
            }
        }

        protected void Floor_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdatePanel3.Update();
            UpdatePanel4.Update();
        }

        protected void Area_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdatePanel4.Update();
        }

        protected void Stand_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}