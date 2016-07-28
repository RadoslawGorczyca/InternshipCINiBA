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
                Document pdfDoc = new Document(PageSize.A3.Rotate(), 10f, 10f, 10f, 0f);
                PdfWriter writer = PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
                FontFactory.RegisterDirectory("C:\\Windows\\Fonts");
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
    <a href="FailureService.aspx">Powrót do formularza</a><br />
    <br />
    <form id="form1" runat="server">   
        <asp:Button ID="Button1" runat="server" OnClick="ExportToPDF" Text="Eksport do PDF" />
        <br />
        <br />
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSource2" Font-Names="Arial Unicode MS">
            <Columns>
                <asp:BoundField DataField="what" HeaderText="what" SortExpression="what" />
                <asp:BoundField DataField="topic" HeaderText="topic" SortExpression="topic" />
                <asp:BoundField DataField="content" HeaderText="treść" SortExpression="content" />
                <asp:BoundField DataField="surname" HeaderText="surname" SortExpression="surname" />
                <asp:BoundField DataField="email" HeaderText="email" SortExpression="email" />
                <asp:BoundField DataField="floor" HeaderText="floor" SortExpression="floor" />
                <asp:BoundField DataField="area" HeaderText="area" SortExpression="area" />
                <asp:BoundField DataField="stand" HeaderText="stand" SortExpression="stand" />
                <asp:BoundField DataField="sendDate" HeaderText="sendDate" SortExpression="sendDate" />
                <asp:BoundField DataField="comments" HeaderText="comments" SortExpression="comments" />
                <asp:CheckBoxField DataField="archived" HeaderText="archived" SortExpression="archived" ReadOnly="True" >
                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                </asp:CheckBoxField>
                <asp:BoundField DataField="owner" HeaderText="owner" SortExpression="owner" />
            </Columns>
        </asp:GridView>
        <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:Failures %>" ProviderName="<%$ ConnectionStrings:Failures.ProviderName %>" SelectCommand="SELECT [what], [topic], [content], [surname], [email], [floor], [area], [stand], [sendDate], [comments], [archived], [owner] FROM [failure] ORDER BY [sendDate] DESC"></asp:SqlDataSource>
        <br />


    </form>
</body>
</html>
