<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="web.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <style type="text/css">        #Select1 {
            width: 173px;
        }
    </style>

    <script>
        /*function validateForm() {
            var c = document.forms["form1"]["TM"].value;
            var d = document.forms["form1"]["TN"].value;
            var e = document.forms["form1"]["Floor"].value;
            var f = document.forms["form1"]["Area"].value;
            var g = document.forms["form1"]["Stand"].value;
            if (c == null || c == "" || d == null || d == "" || e == null || e == "" || f == null || f == "" || g == null || g == "") {
                alert("Wypełnij wszystkie pola!");
                return false;
            }

            var x = document.forms["form1"]["TM"].value;
            var atpos = x.indexOf("@");
            var dotpos = x.lastIndexOf(".");
            if (atpos < 1 || dotpos < atpos + 2 || dotpos + 2 >= x.length) {
                alert("Nieprawidłowy adres e-mail!");
                return false;
            }
        }*/
    </script>
</head>
<body>
    <form id="form1" runat="server" name="form1">
     <div class="mainwrapper">
        <div class="header">
            <h1><a href="Default.aspx">Zgłaszanie Awarii<asp:ScriptManager ID="ScriptManager1" runat="server">
                </asp:ScriptManager>
                </a></h1>
        </div>
            <div class="menuHeader">
                <ul>
                    <% if (Session["logged_level"].ToString().Equals(true.ToString()))
                       {
                           %><li class="menuHeaderExtraLi">Zalogowany jako: <strong><% Response.Write((String)Session["logged as"]); %></strong> </li><%
                           %><li><a href="ManageUsers.aspx" class="btn">Zarządzanie użytkownikami</a></li> <% 
                           %><li><a href="FailureService.aspx" class="btn">Obsługa zgłoszeń</a></li> <%                            
                           %><li><a href="Logout.aspx" class="btn">Wyloguj</a></li><%
                       }%>
                   <% else if(Session["logged_level"].ToString().Equals(false.ToString()))
                       {
                          %><li class="menuHeaderExtraLi">Zalogowany jako: <strong><% Response.Write((String)Session["logged as"]); %></strong> </li><%
                          %><li><a href="Logout.aspx" class="btn">Wyloguj</a></li><%
                       }
                   %>
                </ul>
            </div>
         <table class="table">
            <tr>
                <td style="width: 10%; text-align: right; padding-right: 10px;">&nbsp;</td>
                <td>
                    <asp:Label ID="Error" runat="server" ForeColor="Red" Text="Wszystkie pola muszą zostać wypełnione, minimum 2 znaki!" Visible="False"></asp:Label>
                    <br />
                </td>
            </tr>
            <tr>
                <td style="width: 10%; text-align: right; padding-right: 10px;">Dotyczy:</td>
                <td>
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <asp:RadioButtonList ID="RBDotyczy" runat="server" Font-Names="Arial Unicode MS" OnSelectedIndexChanged="RadioButtonList1_SelectedIndexChanged" Width="450px" AutoPostBack="True">
                                <asp:ListItem Selected="True" Value="0">Sprzętu</asp:ListItem>
                                <asp:ListItem Value="1">Oprogramowania</asp:ListItem>
                            </asp:RadioButtonList>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <br />
                </td>
            </tr>
            <tr>
                <td style="width: 10%; text-align: right; padding-right: 10px;">Nazwisko:</td>
                <td>
                    <asp:TextBox ID="TN" runat="server" Width="98%"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="width: 10%; text-align: right; padding-right: 10px;">E-mail:</td>
                <td>
                    <asp:TextBox ID="TM" runat="server" Width="98%"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="width: 10%; text-align: right; padding-right: 10px;">Piętro:</td>
                <td>
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <asp:DropDownList ID="Floor" runat="server" DataSourceID="SqlDataSource1" DataTextField="floor" DataValueField="floor" Height="25px" Width="89%" AutoPostBack="True" OnSelectedIndexChanged="Floor_SelectedIndexChanged">
                            </asp:DropDownList>
                            <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:areas %>" ProviderName="<%$ ConnectionStrings:areas.ProviderName %>" SelectCommand="SELECT DISTINCT [floor] FROM [areas] ORDER BY [floor]"></asp:SqlDataSource>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="RBDotyczy" EventName="SelectedIndexChanged" />
                        </Triggers>
                    </asp:UpdatePanel>
                </td>
            </tr>
            <tr>
                <td style="width: 10%; text-align: right; padding-right: 10px;">Strefa:</td>
                <td>
                    <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <asp:DropDownList ID="Area" runat="server" DataSourceID="SqlDataSource2" DataTextField="area" DataValueField="area" Height="25px" Width="89%" AutoPostBack="True" OnSelectedIndexChanged="Area_SelectedIndexChanged">
                            </asp:DropDownList>
                            <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:areas %>" ProviderName="<%$ ConnectionStrings:areas.ProviderName %>" SelectCommand="SELECT DISTINCT [area] FROM [areas] WHERE ([floor] = ?) ORDER BY [area]">
                                <SelectParameters>
                                    <asp:ControlParameter ControlID="Floor" Name="floor" PropertyName="SelectedValue" Type="String" />
                                </SelectParameters>
                            </asp:SqlDataSource>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="RBDotyczy" EventName="SelectedIndexChanged" />
                            <asp:AsyncPostBackTrigger ControlID="Floor" EventName="SelectedIndexChanged" />
                        </Triggers>
                    </asp:UpdatePanel>
                </td>
            </tr>
            <tr>
                <td style="width: 10%; text-align: right; padding-right: 10px;">Stanowisko:</td>
                <td>
                    <asp:UpdatePanel ID="UpdatePanel4" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <asp:DropDownList ID="Stand" runat="server" DataSourceID="SqlDataSource3" DataTextField="stand" DataValueField="stand" Height="25px" Width="89%" AutoPostBack="True" OnSelectedIndexChanged="Stand_SelectedIndexChanged">
                            </asp:DropDownList>
                            <asp:SqlDataSource ID="SqlDataSource3" runat="server" ConnectionString="<%$ ConnectionStrings:areas %>" ProviderName="<%$ ConnectionStrings:areas.ProviderName %>" SelectCommand="SELECT DISTINCT [stand] FROM [areas] WHERE (([floor] = ?) AND ([area] = ?)) ORDER BY [stand]">
                                <SelectParameters>
                                    <asp:ControlParameter ControlID="Floor" Name="floor" PropertyName="SelectedValue" Type="String" />
                                    <asp:ControlParameter ControlID="Area" Name="area" PropertyName="SelectedValue" Type="String" />
                                </SelectParameters>
                            </asp:SqlDataSource>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="RBDotyczy" EventName="SelectedIndexChanged" />
                            <asp:AsyncPostBackTrigger ControlID="Floor" EventName="SelectedIndexChanged" />
                            <asp:AsyncPostBackTrigger ControlID="Area" EventName="SelectedIndexChanged" />
                        </Triggers>
                    </asp:UpdatePanel>

                </td>
            </tr>
            <tr>
                <td style="width: 10%; text-align: right; padding-right: 10px;">Temat:</td>
                <td>
                    <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                        <ContentTemplate>
                            <asp:TextBox ID="TTemat" runat="server" Width="98%"></asp:TextBox>
                        </ContentTemplate>
                    </asp:UpdatePanel>

                </td>
            </tr>
            <tr>
                <td style="width: 10%; text-align: right; padding-right: 10px;">Treść awarii:</td>
                <td>
                    <asp:TextBox ID="TC" TextMode="multiline" runat="server" Width="98%" Rows="10"></asp:TextBox>

                </td>
            </tr>
            <tr>
                <td></td>
                <td>
                    <asp:Button ID="BSend" runat="server" Text="Wyślij" OnClick="BSend_Click" />
                </td>
            </tr>
            </table>
    </div>
        <p>
        </p>
        <asp:UpdatePanel ID="UpdatePanel8" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <asp:GridView ID="TabelaSprzet" runat="server" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" CellPadding="4" DataSourceID="Awarie" ForeColor="#333333" GridLines="None" Width="1142px">
                    <AlternatingRowStyle BackColor="White" />
                    <Columns>
                        <asp:BoundField DataField="content" HeaderText="treść" SortExpression="content" />
                        <asp:BoundField DataField="surname" HeaderText="nazwisko" SortExpression="surname" />
                        <asp:BoundField DataField="email" HeaderText="e-mail" SortExpression="email" />
                        <asp:BoundField DataField="floor" HeaderText="piętro" SortExpression="floor" />
                        <asp:BoundField DataField="area" HeaderText="strefa" SortExpression="area" />
                        <asp:BoundField DataField="stand" HeaderText="stanowisko" SortExpression="stand" />
                        <asp:BoundField DataField="sendDate" HeaderText="data" SortExpression="sendDate" />
                        <asp:BoundField DataField="comments" HeaderText="komentarze" SortExpression="comments" />
                    </Columns>
                    <FooterStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle BackColor="#FF9900" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Center" />
                    <RowStyle BackColor="#FFFBD6" ForeColor="#333333" />
                    <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />
                    <SortedAscendingCellStyle BackColor="#FDF5AC" />
                    <SortedAscendingHeaderStyle BackColor="#CC9900" />
                    <SortedDescendingCellStyle BackColor="#FCF6C0" />
                    <SortedDescendingHeaderStyle BackColor="#996600" />
                </asp:GridView>
                <asp:SqlDataSource ID="Awarie" runat="server" ConnectionString="<%$ ConnectionStrings:Failures %>" ProviderName="<%$ ConnectionStrings:Failures.ProviderName %>" SelectCommand="SELECT [content], [surname], [email], [floor], [area], [stand], [sendDate], [comments] FROM [failure] WHERE (([floor] = ?) AND ([area] = ?) AND ([stand] = ?)) ORDER BY [sendDate] DESC">
                    <SelectParameters>
                        <asp:ControlParameter ControlID="Floor" Name="floor" PropertyName="SelectedValue" Type="String" />
                        <asp:ControlParameter ControlID="Area" Name="area" PropertyName="SelectedValue" Type="String" />
                        <asp:ControlParameter ControlID="Stand" Name="stand" PropertyName="SelectedValue" Type="String" />
                    </SelectParameters>
                </asp:SqlDataSource>
                <asp:UpdatePanel ID="UpdatePanel9" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:GridView ID="TabelaOprogramowanie" runat="server" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" CellPadding="4" datasourceid="Awarie0" ForeColor="#333333" GridLines="None" Width="1139px">
                            <AlternatingRowStyle BackColor="White" />
                            <Columns>
                                <asp:BoundField DataField="topic" HeaderText="temat" SortExpression="topic" />
                                <asp:BoundField DataField="content" HeaderText="treść" SortExpression="content" />
                                <asp:BoundField DataField="surname" HeaderText="nazwisko" SortExpression="surname" />
                                <asp:BoundField DataField="email" HeaderText="e-mail" SortExpression="email" />
                                <asp:BoundField DataField="sendDate" HeaderText="data" SortExpression="sendDate" />
                                <asp:BoundField DataField="comments" HeaderText="komentarze" SortExpression="comments" />
                            </Columns>
                            <FooterStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
                            <HeaderStyle BackColor="#FF9900" Font-Bold="True" ForeColor="White" />
                            <PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Center" />
                            <RowStyle BackColor="#FFFBD6" ForeColor="#333333" />
                            <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />
                            <SortedAscendingCellStyle BackColor="#FDF5AC" />
                            <SortedAscendingHeaderStyle BackColor="#CC9900" />
                            <SortedDescendingCellStyle BackColor="#FCF6C0" />
                            <SortedDescendingHeaderStyle BackColor="#996600" />
                        </asp:GridView>
                        <asp:SqlDataSource ID="Awarie0" runat="server" ConnectionString="<%$ ConnectionStrings:Failures %>" ProviderName="<%$ ConnectionStrings:Failures.ProviderName %>" SelectCommand="SELECT [topic], [content], [surname], [email], [sendDate], [comments] FROM [failure] WHERE ([what] = ?) ORDER BY [sendDate] DESC">
                            <SelectParameters>
                                <asp:Parameter DefaultValue="OPROGRAMOWANIE" Name="what" Type="String" />
                            </SelectParameters>
                        </asp:SqlDataSource>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="Floor" EventName="SelectedIndexChanged" />
                        <asp:AsyncPostBackTrigger ControlID="Area" EventName="SelectedIndexChanged" />
                        <asp:AsyncPostBackTrigger ControlID="Stand" EventName="SelectedIndexChanged" />
                        <asp:AsyncPostBackTrigger ControlID="RBDotyczy" EventName="SelectedIndexChanged" />
                    </Triggers>
                </asp:UpdatePanel>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="Floor" EventName="SelectedIndexChanged" />
                <asp:AsyncPostBackTrigger ControlID="Area" EventName="SelectedIndexChanged" />
                <asp:AsyncPostBackTrigger ControlID="Stand" EventName="SelectedIndexChanged" />
                <asp:AsyncPostBackTrigger ControlID="RBDotyczy" EventName="SelectedIndexChanged" />
            </Triggers>
        </asp:UpdatePanel>
    </form>
</body>
</html>
