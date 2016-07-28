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
                DataList1.RenderControl(hw);
                StringReader sr = new StringReader(sw.ToString());
                Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 10f, 0f);
                PdfWriter writer = PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
                pdfDoc.Open();
                FontFactory.RegisterDirectory("C:\\Windows\\Fonts");
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
    <form id="form1" runat="server">   
        <asp:DataList ID="DataList1" runat="server" DataSourceID="failure" Width="513px" CellPadding="4" ForeColor="#333333">
            <AlternatingItemStyle BackColor="White" />
            <FooterStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
            <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
            <ItemStyle BackColor="#FFFBD6" ForeColor="#333333" />
            <ItemTemplate>
                Dotyczy:
                <asp:Label ID="whatLabel" runat="server" Text='<%# Eval("what") %>' />
                <br />
                Data:
                <asp:Label ID="sendDateLabel" runat="server" Text='<%# Eval("sendDate") %>' />
                <br />
                <br />
                Imię i nazwisko:
                <asp:Label ID="surnameLabel" runat="server" Text='<%# Eval("surname") %>' />
                <br />
                E-mail:
                <asp:Label ID="emailLabel" runat="server" Text='<%# Eval("email") %>' />
                <br />
                <br />
                Piętro:
                <asp:Label ID="floorLabel" runat="server" Text='<%# Eval("floor") %>' />
                <br />
                Strefa:
                <asp:Label ID="areaLabel" runat="server" Text='<%# Eval("area") %>' />
                <br />
                Stanowisko:
                <asp:Label ID="standLabel" runat="server" Text='<%# Eval("stand") %>' />
                <br />
                <br />
                Temat:
                <asp:Label ID="topicLabel" runat="server" Text='<%# Eval("topic") %>' />
                <br />
                <br />
                Treść:
                <asp:Label ID="contentLabel" runat="server" Text='<%# Eval("content") %>' />
                <br />
                <br />
                Właściciel:
                <asp:Label ID="ownerLabel" runat="server" Text='<%# Eval("owner") %>' />
                <br />
                Komentarze:
                <asp:Label ID="commentsLabel" runat="server" Text='<%# Eval("comments") %>' />
                <br />
                Zrealizowano?:
                <asp:Label ID="archivedLabel" runat="server" Text='<%# Eval("archived") %>' />
<br />
            </ItemTemplate>
            <SelectedItemStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />
        </asp:DataList>
        <asp:SqlDataSource ID="failure" runat="server" ConnectionString="<%$ ConnectionStrings:Failures %>" ProviderName="<%$ ConnectionStrings:Failures.ProviderName %>" SelectCommand="SELECT [what], [topic], [surname], [content], [email], [floor], [area], [stand], [sendDate], [comments], [archived], [owner] FROM [failure] WHERE ([id] = ?)">
            <SelectParameters>
                <asp:QueryStringParameter Name="id" QueryStringField="id" Type="Int32" />
            </SelectParameters>
        </asp:SqlDataSource>
        <br />
        <asp:Button ID="Button1" runat="server" OnClick="ExportToPDF" Text="Eksport do PDF" />
        <br />


    </form>
</body>
</html>
