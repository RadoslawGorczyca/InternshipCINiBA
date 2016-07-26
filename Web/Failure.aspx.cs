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
                    
                }

                conn.Close();
            }
        }

        protected void pdfClick(object sender, EventArgs e)
        {
            Response.Redirect("ViewDefault.aspx");
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
            /*  
            //Definiowanie maili. Skomentowane bo wymaga konfiguracji tj. podania odpowiedniego portu, hostingu itd.
            string system_mail = "andrzej.koziara@us.edu.pl";
            string system_password = "";
            string adminstration_mail = "";

            SmtpClient client = new SmtpClient
            {
                Host = "155.158.",
                Port = 587,
                EnableSsl = false,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                Credentials = new System.Net.NetworkCredential(system_mail, system_password),
                Timeout = 10000,
            };
            if ((Cforward.Checked == false) && (CArch.Checked == false))
            {
                MailMessage customer = new MailMessage(system_mail, TMail.ToString(), "Przyjęcie Awarii", "Twoje zgłoszenie zostało przyjęte przez: " + owner);
                client.Send(customer);

                MailMessage administration = new MailMessage(system_mail, adminstration_mail, "Zgłoszenie awarii", "Nowe zgłoszenie awarii oczekuje na przyjęcie.");
                client.Send(administration);
            }
            if ((Cforward.Checked == true) && (CArch.Checked == false))
            {
                OleDbCommand cmOwner = new OleDbCommand("SELECT email FROM Users WHERE login = '" + owner + "'", conn);
                String ownerEmail = cmOwner.ExecuteScalar().ToString();

                MailMessage ownerMail = new MailMessage(system_mail, ownerEmail, "Zgłoszenie awarii", "Nowe zgłoszenie awarii zostało przekazane Tobie.");
                client.Send(ownerMail);
            }

            if ((Cforward.Checked == true) && (FirstCharToUpper(Session["logged as"].ToString()) == "Admin") && (CArch.Checked == false))
            {
                OleDbCommand cmOwner = new OleDbCommand("SELECT email FROM Users WHERE login = '" + owner + "'", conn);
                String ownerEmail = cmOwner.ExecuteScalar().ToString();

                MailMessage ownerMail = new MailMessage(system_mail, ownerEmail, "Zgłoszenie awarii - Pilne!", "Nowe zgłoszenie awarii zostało przekazane Tobie.");
                client.Send(ownerMail);
            }

            if ((Cforward.Checked == false) && (CArch.Checked == true))
            {
                MailMessage customer = new MailMessage(system_mail, TMail.ToString(), "Rozwiązanie Awarii", "Twoje zgłoszenie zostało rozpatrzone i przeniesione do archiwum. Komentarz działu technicznego: " + oldComments);
                client.Send(customer);

                MailMessage administration = new MailMessage(system_mail, adminstration_mail, "Rozwiązanie Awarii", "Zgłoszenie zostało przeniesione do archiwum");
                client.Send(administration);
            }

            if ((Cforward.Checked == true) && (CArch.Checked == true))
            {
                //do nothing lol
            }
            */

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