using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.OleDb;
using System.Web.UI.HtmlControls;
using System.Net.Mail;
namespace web
{
    public partial class Failure : System.Web.UI.Page
    {
        String TOwner = "";
        
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
                pageTitle = "Awaria";

                HtmlGenericControl css;
                css = new HtmlGenericControl();
                css.TagName = "style";
                css.Attributes.Add("type", "text/css");
                css.InnerHtml = "@import \"" + Request.Url.GetLeftPart(UriPartial.Authority) + VirtualPathUtility.ToAbsolute("~/") + "Styles/site.css\";";
                Page.Header.Controls.Add(css);

                OleDbConnection conn = null;


                conn = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + Server.MapPath("./App_Data/failures.accdb"));
                conn.Open();

                OleDbCommand cmArchive = new OleDbCommand("SELECT archived FROM failure WHERE id = " + Request.QueryString["id"] + "", conn);

                if (cmArchive.ExecuteScalar().ToString().Equals(true.ToString()))
                {
                    conn.Close();
                    Response.Redirect("FailureService.aspx?archived=true");
                }

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
                if (TComment.Text.Equals(""))
                {
                    TOldComment.Text = cmComments.ExecuteScalar().ToString();
                    TMail.Text = cmMail.ExecuteScalar().ToString();
                    TName.Text = cmName.ExecuteScalar().ToString();
                    TDate.Text = cmDate.ExecuteScalar().ToString();
                    TFloor.Text = cmFloor.ExecuteScalar().ToString();
                    TArea.Text = cmArea.ExecuteScalar().ToString();
                    TStand.Text = cmStand.ExecuteScalar().ToString();
                    TFailure.Text = cmFailure.ExecuteScalar().ToString();
                    TForwarded.Text = cmForwarded.ExecuteScalar().ToString();
                    TOwner = cmOwner.ExecuteScalar().ToString();
                    TTemat.Text = cmTemat.ExecuteScalar().ToString();
                }

                conn.Close();
            }
        }

        protected void pdfClick(object sender, EventArgs e)
        {
            string url = "ViewDefault.aspx?id=" + Request.QueryString["id"];
            Response.Redirect(url);
        }

        protected void BSave_Click(object sender, EventArgs e)
        {
            String date = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss").ToString();
            OleDbConnection conn = null;

            conn = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + Server.MapPath("./App_Data/failures.accdb"));
            conn.Open();

            OleDbCommand cmComments = new OleDbCommand("SELECT comments FROM failure WHERE id = " + Request.QueryString["id"] + "", conn);
            String oldComments = cmComments.ExecuteScalar().ToString();

            String owner = DDL1.SelectedItem.Value.ToString();

            int forward = 0;
            Int32.TryParse(TForwarded.Text, out forward);
            if (TOwner == "")
                TOwner = FirstCharToUpper(Session["logged as"].ToString());

            if (Cforward.Checked == true)
            {
                if (owner != TOwner)
                {
                    if (forward < 6)
                    {
                        forward += 1;
                    }
                }
            }else
            {
                owner = TOwner;
            }
            String save = (oldComments.Equals("") ? "" : cmComments.ExecuteScalar().ToString() + "\n") + date + " " +" Autor: " + (String)Session["logged as"] + " Treść:" + TComment.Text.ToString();

            OleDbCommand cm = new OleDbCommand("UPDATE failure SET forwarded ='" + forward.ToString() + "', owner = '" + owner + "', takeDate ='" + date + "', comments = '" + save + "', archived = " + (CArch.Checked ? "TRUE" : "FALSE") + " WHERE id = " + Request.QueryString["id"], conn);

            cm.ExecuteScalar();

            conn.Close();

            OleDbConnection cnn = null;
            cnn = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + Server.MapPath("./App_Data/Users.accdb"));
            cnn.Open();

            OleDbCommand cmmailFwd = new OleDbCommand("SELECT email FROM users WHERE login = '" + owner + "'", cnn);
            String mailFwd = cmmailFwd.ExecuteScalar().ToString();
            cnn.Close();

            //Definiowanie maili. Skomentowane bo wymaga konfiguracji tj. podania odpowiedniego portu, hostingu itd.
            string toFwd = mailFwd;
            string to = TMail.Text.ToString();
            string from = "system@ciniba.edu.pl";
            string system = "d-i@ciniba.edu.pl";
            MailMessage personFwd = new MailMessage(from, toFwd);
            MailMessage person = new MailMessage(from, to);
            MailMessage admin = new MailMessage(from, system);
            SmtpClient client = new SmtpClient("155.158.102.75", 25);
            client.UseDefaultCredentials = true;


            if(Cforward.Checked == true)
            {
                if (TTemat.Text.Equals(""))
                {
                    personFwd.Subject = "Przydzielono nowe zgłoszenie awarii sprzętu dla " + owner;
                    personFwd.Body = @"Przydzielono zgłoszenie awarii sprzętu użytkownikowi " + owner + ":" + "\n\n" + "Imię i nazwisko: " + TName.Text.ToString() + "\n" + "E-mail: " + TMail.Text.ToString() + "\n\n" + TFloor.Text.ToString() + "/" + TArea.Text.ToString() + "/" + TStand.Text.ToString() + "\n\n" + "Treść: " + TFailure.Text.ToString() + "\n\n" + "Komentarze: " + TOldComment.Text.ToString();
                    admin.Subject = "Przydzielono nowe zgłoszenie awarii sprzętu dla " + owner;
                    admin.Body = @"Przydzielono zgłoszenie awarii sprzętu użytkownikowi " + owner + ":" + "\n\n" + "Imię i nazwisko: " + TName.Text.ToString() + "\n" + "E-mail: " + TMail.Text.ToString() + "\n\n" + TFloor.Text.ToString() + "/" + TArea.Text.ToString() + "/" + TStand.Text.ToString() + "\n\n" + "Treść: " + TFailure.Text.ToString() + "\n\n" + "Komentarze: " + TOldComment.Text.ToString();
                }
                else
                {
                    personFwd.Subject = "Przydzielono nowe zgłoszenie awarii oprogramowania dla " + owner;
                    personFwd.Body = @"Przydzielono zgłoszenie awarii oprogramowania użytkownikowi " + owner + ":" + "\n\n" + "Imię i nazwisko: " + TName.Text.ToString() + "\n" + "E-mail: " + TMail.Text.ToString() + "\n\n" + "Temat: " + TTemat.Text.ToString() + "\n" + "Treść: " + TFailure.Text.ToString() + "\n\n" + "Komentarze: " + TOldComment.Text.ToString();
                    admin.Subject = "Przydzielono nowe zgłoszenie awarii oprogramowania dla " + owner;
                    admin.Body = @"Przydzielono zgłoszenie awarii oprogramowania użytkownikowi " + owner + ":" + "\n\n" + "Imię i nazwisko: " + TName.Text.ToString() + "\n" + "E-mail: " + TMail.Text.ToString() + "\n\n" + "Temat: " + TTemat.Text.ToString() + "\n" + "Treść: " + TFailure.Text.ToString() + "\n\n" + "Komentarze: " + TOldComment.Text.ToString();
                }

                client.Send(personFwd);
                client.Send(admin);
            }

            if(CArch.Checked == true)
            {
                if (TTemat.Text.Equals(""))
                {
                    person.Subject = "Zrealizowano zgłoszenie dla stanowiska " + TFloor.Text.ToString() + "/" + TArea.Text.ToString() + "/" + TStand.Text.ToString();
                    person.Body = @"Zrealizowane zgłoszenie:" + "\n\n" + "Imię i nazwisko: " + TName.Text.ToString() + "\n" + "E-mail: " + TMail.Text.ToString() + "\n\n" + TFloor.Text.ToString() + "/" + TArea.Text.ToString() + "/" + TStand.Text.ToString() + "\n\n" + TTemat.Text.ToString() + "\n" + TFailure.Text.ToString() + "\n\n" + "Komentarze: " + TOldComment.Text.ToString() + "\n\n" + "Dziękujemy za zgłoszenie awarii!";
                    admin.Subject = "Zarchiwizowano zgłoszenie dla stanowiska " + TFloor.Text.ToString() + "/" + TArea.Text.ToString() + "/" + TStand.Text.ToString();
                    admin.Body = @"Zarchiwizowane zgłoszenie:" + "\n\n" + "Imię i nazwisko: " + TName.Text.ToString() + "\n" + "E-mail: " + TMail.Text.ToString() + "\n\n" + TFloor.Text.ToString() + "/" + TArea.Text.ToString() + "/" + TStand.Text.ToString() + "\n\n" + TTemat.Text.ToString() + "\n" + TFailure.Text.ToString() + "\n\n" + "Komentarze: " + TOldComment.Text.ToString();
                }
                else
                {
                    person.Subject = "Zrealizowano zgłoszenie awarii oprogramowania";
                    person.Body = @"Zrealizowane zgłoszenie:" + "\n\n" + "Imię i nazwisko: " + TName.Text.ToString() + "\n" + "E-mail: " + TMail.Text.ToString() + "\n\n" + "Temat: " + TTemat.Text.ToString() + "\n" + "Treść: " + TFailure.Text.ToString() + "\n\n" + "Komentarze: " + TOldComment.Text.ToString() + "\n\n" + "Dziękujemy za zgłoszenie awarii!";
                    admin.Subject = "Zarchiwizowano zgłoszenie awarii oprogramowania";
                    admin.Body = @"Zarchiwizowane zgłoszenie:" + "\n\n" + "Imię i nazwisko: " + TName.Text.ToString() + "\n" + "E-mail: " + TMail.Text.ToString() + "\n\n" + "Temat: " + TTemat.Text.ToString() + "\n" + "Treść: " + TFailure.Text.ToString() + "\n\n" + "Komentarze: " + TOldComment.Text.ToString();
                }

                client.Send(person);
                client.Send(admin);
            }


            Response.Redirect("FailureService.aspx?saved=true");
        }
        
        public static string FirstCharToUpper(string input)
        {
            if (String.IsNullOrEmpty(input))
                throw new ArgumentException("ARGH!");
            return input.First().ToString().ToUpper() + input.Substring(1);
        }
    }
}