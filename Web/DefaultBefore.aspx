<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DefaultBefore.aspx.cs" Inherits="web.DefaultBefore" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <style type="text/css">        #Select1 {
            width: 173px;
        }
        .auto-style1 {
            width: 1085px;
        }
        .auto-style2 {
            width: 995px;
        }
        .auto-style3 {
            width: 1167px;
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
    <form id="form1" runat="server" name="form1" onsubmit="return validateForm();">
     <div class="mainwrapper">
        <div class="header">
            <h1 class="auto-style3"><a href="Default.aspx">System zgłaszania awarii sprzętu informatycznego budynku CINiBA</a></h1>
        </div>
        <div class="menuHeader">
            <%if (Session["logged"] == null || Session["logged"] != null && !(Boolean)Session["logged"])
            {
                %>
            <asp:ScriptManager ID="ScriptManager1" runat="server">
            </asp:ScriptManager>
            <asp:Button ID="Button1" runat="server" Text="Zaloguj się" OnClick="LogClick" /><%
            }%>
        </div>
    </div>
         <table class="auto-style2">
            <tr>
                <td style="width: 10%; text-align: right; padding-right: 10px;">&nbsp;</td>
                <td class="auto-style1">
                    &nbsp;</td>
            </tr>
            <tr>
                <td style="width: 10%; text-align: right; padding-right: 10px;">Dotyczy:</td>
                <td class="auto-style1">
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <asp:RadioButtonList ID="RBDotyczy" runat="server" AutoPostBack="True" Font-Names="Arial Unicode MS" OnSelectedIndexChanged="RadioButtonList1_SelectedIndexChanged" Width="450px">
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
                <td class="auto-style1">
                    <asp:TextBox ID="TN" runat="server" Width="98%"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="width: 10%; text-align: right; padding-right: 10px;">E-mail:</td>
                <td class="auto-style1">
                    <asp:TextBox ID="TM" runat="server" Width="98%"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="width: 10%; text-align: right; padding-right: 10px;">Piętro:</td>
                <td class="auto-style1">
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
                <td class="auto-style1">
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
                <td class="auto-style1">
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
                <td class="auto-style1">
                    <asp:UpdatePanel ID="UpdatePanel5" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <asp:TextBox ID="TTemat" runat="server" Width="98%"></asp:TextBox>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="RBDotyczy" EventName="SelectedIndexChanged" />
                        </Triggers>
                    </asp:UpdatePanel>

                </td>
            </tr>
            <tr>
                <td style="width: 10%; text-align: right; padding-right: 10px;">Treść awarii:</td>
                <td class="auto-style1">
                    <asp:TextBox ID="TC" TextMode="multiline" runat="server" Width="98%" Rows="10"></asp:TextBox>

                </td>
            </tr>
            <tr>
                <td></td>
                <td class="auto-style1">
                    <asp:Button ID="BSend" runat="server" Text="Wyślij" OnClick="BSend_Click" />
                </td>
            </tr>
            </table>
    </form>
</body>
</html>
