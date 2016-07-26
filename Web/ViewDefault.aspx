<%@ Page Language="C#" AutoEventWireup="true" EnableEventValidation = "false" %>
<%@ Import Namespace="System.IO" %>
<%@ Import Namespace="iTextSharp.text" %>
<%@ Import Namespace="iTextSharp.text.html.simpleparser" %>
<%@ Import Namespace="iTextSharp.text.pdf" %>
<%@ Import Namespace="iTextSharp.tool.xml" %>
<!DOCTYPE html>
<script runat="server">
    protected void ExportToPDF(object sender, EventArgs e)
    {
        using (StringWriter sw = new StringWriter())
        {
            using (HtmlTextWriter hw = new HtmlTextWriter(sw))
            {
                GridView1.RenderControl(hw);
                StringReader sr = new StringReader(sw.ToString());
                Document pdfDoc = new Document(PageSize.A3, 10f, 10f, 10f, 0f);
                PdfWriter writer = PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
                pdfDoc.Open();
                XMLWorkerHelper.GetInstance().ParseXHtml(writer, pdfDoc, sr);
                pdfDoc.Close();
                Response.ContentType = "application/pdf";
                Response.AddHeader("content-disposition", "attachment;filename=Spis.pdf");
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                Response.Write(pdfDoc);
                Response.End();
            }
        }
    }

    public override void VerifyRenderingInServerForm(Control control)
    {
        /* Verifies that the control is rendered */
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        /*GridView1.DataBind();
        Response.ContentType = "application/pdf";
        Response.AddHeader("content-disposition", "attachment;filename=TestPage.pdf");
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        StringWriter sw = new StringWriter();
        HtmlTextWriter hw = new HtmlTextWriter(sw);
        this.Page.RenderControl(hw);
        StringReader sr = new StringReader(sw.ToString());
        Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 100f, 0f);
        HTMLWorker htmlparser = new HTMLWorker(pdfDoc);
        PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
        pdfDoc.Open();
        htmlparser.Parse(sr);
        pdfDoc.Close();
        Response.Write(pdfDoc);
        Response.End();
         */
    }
</script>

<html lang="en" xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta charset="utf-8" />
    <title></title>    
</head>
<body>
    <a href="Failure.aspx">Powrót do formularza</a><br />
    <form id="form1" runat="server">   
        <asp:GridView ID="GridView1" runat="server" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" DataKeyNames="ID" DataSourceID="SqlDataSource2">
            <Columns>
                <asp:BoundField DataField="id" HeaderText="Identyfikator" InsertVisible="False" ReadOnly="True" SortExpression="ID" />
                <asp:BoundField DataField="surname" HeaderText="Nazwisko" SortExpression="surname" />
                <asp:BoundField DataField="email" HeaderText="Email" SortExpression="email" />
                <asp:BoundField DataField="content" HeaderText="Treść" SortExpression="content" />
                <asp:BoundField DataField="floor" HeaderText="Piętro" SortExpression="floor" />
                <asp:BoundField DataField="area" HeaderText="Strefa" SortExpression="area" />
                <asp:BoundField DataField="stand" HeaderText="Stanowisko" SortExpression="stand" />
                <asp:BoundField DataField="sendDate" HeaderText="Data przesłania" SortExpression="sendDate" />
                <asp:BoundField DataField="takeDate" HeaderText="Data przyjęcia" SortExpression="takeDate" />
                <asp:BoundField DataField="comments" HeaderText="Komentarz administratora" SortExpression="comments" />
                <asp:BoundField DataField="owner" HeaderText="Administrator" SortExpression="owner" />
                <asp:BoundField DataField="forwarded" HeaderText="Ilość przekierowań" SortExpression="forwarded" />
            </Columns>
        </asp:GridView>
        <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="Provider=Microsoft.ACE.OLEDB.12.0;Data Source=|DataDirectory|\Failures.accdb" ProviderName="System.Data.OleDb" SelectCommand="SELECT [id], [content], [surname], [email], [floor], [area], [stand], [sendDate], [takeDate], [comments], [owner], [forwarded] FROM [failure]"></asp:SqlDataSource>
        <asp:Button ID="Button1" runat="server" OnClick="ExportToPDF" Text="Eksport do PDF" />
        <br />


    </form>
</body>
</html>
