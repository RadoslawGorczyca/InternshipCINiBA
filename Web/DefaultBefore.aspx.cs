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
    public partial class DefaultBefore : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
            {
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



                string system_mail = "d_i@ciniba.edu.pl";
                string system_password = "a";
                string administration_mail = "d_i@ciniba.edu.pl";
                
                    SmtpClient client = new SmtpClient
                    {
                        Host = "155.158.102.75",
                        Port = 25,
                        EnableSsl = false,
                        DeliveryMethod = SmtpDeliveryMethod.Network,
                        Credentials = new System.Net.NetworkCredential(system_mail, system_password),
                        Timeout = 10000,
                    };
                    MailMessage customer = new MailMessage(system_mail, M, "Zgłoszenie awarii e-mail dla zgłaszającego", "Awaria została pomyślnie zgłoszona. Zespół DI");
                    client.Send(customer);

                    MailMessage administration = new MailMessage(system_mail, administration_mail, "Zgłoszenie awarii email dla administratorów", "Nowe zgłoszenie awarii oczekuje na przyjęcie.");
                    client.Send(administration);
               



                TC.Text = "";

                Response.Redirect("Default.aspx");
            }
            else
            {

            }
        }
        protected void LogClick(object sender, EventArgs e)
        {
            Response.Redirect("Login.aspx");
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