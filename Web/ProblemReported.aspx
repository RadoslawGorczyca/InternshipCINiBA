<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ProblemReported.aspx.cs" Inherits="web.ProblemReported" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <asp:Label ID="Label1" runat="server" Text="Zgłoszenie przyjęte! Dziękujemy!" Font-Names="Arial Unicode MS" ForeColor="#FF9900"></asp:Label>
    
        <br />
    
        <br />
    
    </div>
        <asp:DataList ID="DataList1" runat="server" CellPadding="4" DataSourceID="Awarie" ForeColor="#333333" Width="650px">
            <AlternatingItemStyle BackColor="White" />
            <FooterStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
            <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
            <ItemStyle BackColor="#FFFBD6" ForeColor="#333333" />
            <ItemTemplate>
                Dotyczy:
                <asp:Label ID="whatLabel" runat="server" Text='<%# Eval("what") %>' />
                <br />
                Imię i nazwisko:
                <asp:Label ID="surnameLabel" runat="server" Text='<%# Eval("surname") %>' />
                <br />
                Email:
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
                Treść:
                <asp:Label ID="contentLabel" runat="server" Text='<%# Eval("content") %>' />
                <br />
                <br />
            </ItemTemplate>
            <SelectedItemStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />
        </asp:DataList>
        <asp:SqlDataSource ID="Awarie" runat="server" ConnectionString="<%$ ConnectionStrings:Failures %>" ProviderName="<%$ ConnectionStrings:Failures.ProviderName %>" SelectCommand="SELECT [topic], [what], [content], [surname], [email], [floor], [area], [stand], [sendDate] FROM [failure] WHERE ([id] = ?)">
            <SelectParameters>
                <asp:QueryStringParameter Name="id" QueryStringField="ID" Type="Int32" />
            </SelectParameters>
        </asp:SqlDataSource>
        <asp:Button ID="Button1" runat="server" Font-Names="Arial Unicode MS" OnClick="Button1_Click" Text="Wróć" />
        <br />
    </form>
</body>
</html>
