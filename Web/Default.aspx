﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="web.Default" %>

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
            <h1><a href="Default.aspx">Zgłaszanie Awarii</a></h1>
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
                <td style="width: 10%; text-align: right; padding-right: 10px;">Treść awarii:</td>
                <td>
                    <asp:TextBox ID="TC" TextMode="multiline" runat="server" Width="98%" Rows="10"></asp:TextBox>
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
                    <asp:DropDownList ID="Floor" runat="server" DataSourceID="SqlDataSource1" DataTextField="floor" DataValueField="floor" Height="25px" Width="89%">
                    </asp:DropDownList>
                    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:areas %>" ProviderName="<%$ ConnectionStrings:areas.ProviderName %>" SelectCommand="SELECT DISTINCT [floor] FROM [areas]"></asp:SqlDataSource>
                    <asp:Button ID="Button2" runat="server" Text="Zatwierdź" OnClick="Click" />
                </td>
            </tr>
            <tr>
                <td style="width: 10%; text-align: right; padding-right: 10px;">Strefa:</td>
                <td>
                    <asp:DropDownList ID="Area" runat="server" DataSourceID="SqlDataSource2" DataTextField="area" DataValueField="area" Height="25px" Width="89%">
                    </asp:DropDownList>
                    <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:areas %>" ProviderName="<%$ ConnectionStrings:areas.ProviderName %>" SelectCommand="SELECT DISTINCT [area] FROM [areas] WHERE ([floor] = ?)">
                        <SelectParameters>
                            <asp:ControlParameter ControlID="Floor" Name="floor" PropertyName="SelectedValue" Type="String" />
                        </SelectParameters>
                    </asp:SqlDataSource>
                    <asp:Button ID="Button1" runat="server" Text="Zatwierdź" OnClick="Click" />
                </td>
            </tr>
            <tr>
                <td style="width: 10%; text-align: right; padding-right: 10px;">Stanowisko:</td>
                <td>
                    <asp:DropDownList ID="Stand" runat="server" DataSourceID="SqlDataSource3" DataTextField="stand" DataValueField="stand" Height="25px" Width="89%">
                    </asp:DropDownList>
                    <asp:SqlDataSource ID="SqlDataSource3" runat="server" ConnectionString="<%$ ConnectionStrings:areas %>" ProviderName="<%$ ConnectionStrings:areas.ProviderName %>" SelectCommand="SELECT DISTINCT [stand] FROM [areas] WHERE ([area] = ?)AND([floor] = ?)">
                        <SelectParameters>
                            <asp:ControlParameter ControlID="Area" Name="area" PropertyName="SelectedValue" Type="String" />
                            <asp:ControlParameter ControlID="Floor" Name="floor" PropertyName="SelectedValue" Type="String" />
                        </SelectParameters>
                    </asp:SqlDataSource>
                    <asp:Button ID="Button3" runat="server" Text="Zatwierdź" OnClick="Click" />

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
