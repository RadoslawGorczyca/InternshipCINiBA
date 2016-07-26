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
    <form id="form1" runat="server" name="form1" onsubmit="return validateForm();">
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
                    &nbsp;</td>
            </tr>
            <tr>
                <td style="width: 10%; text-align: right; padding-right: 10px;">Dotyczy:</td>
                <td>
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <asp:RadioButtonList ID="RBDotyczy" runat="server" AutoPostBack="True" Font-Names="Arial Unicode MS" OnSelectedIndexChanged="RadioButtonList1_SelectedIndexChanged" Width="450px">
                                <asp:ListItem Selected="True" Value="0">Sprzętu</asp:ListItem>
                                <asp:ListItem Value="1">Oprogramowania</asp:ListItem>
                            </asp:RadioButtonList>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="RBDotyczy" EventName="SelectedIndexChanged" />
                        </Triggers>
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
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                        <ContentTemplate>
                            <asp:DropDownList ID="Floor" runat="server" DataSourceID="SqlDataSource1" DataTextField="floor" DataValueField="floor" Height="25px" Width="89%">
                            </asp:DropDownList>
                            <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:areas %>" ProviderName="<%$ ConnectionStrings:areas.ProviderName %>" SelectCommand="SELECT DISTINCT [floor] FROM [areas]"></asp:SqlDataSource>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
            </tr>
            <tr>
                <td style="width: 10%; text-align: right; padding-right: 10px;">Strefa:</td>
                <td>
                    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                        <ContentTemplate>
                            <asp:DropDownList ID="Area" runat="server" DataSourceID="SqlDataSource2" DataTextField="area" DataValueField="area" Height="25px" Width="89%">
                            </asp:DropDownList>
                            <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:areas %>" ProviderName="<%$ ConnectionStrings:areas.ProviderName %>" SelectCommand="SELECT DISTINCT [area] FROM [areas] WHERE ([floor] = ?)">
                                <SelectParameters>
                                    <asp:ControlParameter ControlID="Floor" Name="floor" PropertyName="SelectedValue" Type="String" />
                                </SelectParameters>
                            </asp:SqlDataSource>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
            </tr>
            <tr>
                <td style="width: 10%; text-align: right; padding-right: 10px;">Stanowisko:</td>
                <td>
                    <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                        <ContentTemplate>
                            <asp:DropDownList ID="Stand" runat="server" DataSourceID="SqlDataSource3" DataTextField="stand" DataValueField="stand" Height="25px" Width="89%">
                            </asp:DropDownList>
                            <asp:SqlDataSource ID="SqlDataSource3" runat="server" ConnectionString="<%$ ConnectionStrings:areas %>" ProviderName="<%$ ConnectionStrings:areas.ProviderName %>" SelectCommand="SELECT DISTINCT [stand] FROM [areas] WHERE ([area] = ?)AND([floor] = ?)">
                                <SelectParameters>
                                    <asp:ControlParameter ControlID="Area" Name="area" PropertyName="SelectedValue" Type="String" />
                                    <asp:ControlParameter ControlID="Floor" Name="floor" PropertyName="SelectedValue" Type="String" />
                                </SelectParameters>
                            </asp:SqlDataSource>
                        </ContentTemplate>
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
    </form>
</body>
</html>
