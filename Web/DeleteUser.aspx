<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DeleteUser.aspx.cs" Inherits="web.DeleteUser" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title><%= pageTitle %></title>
</head>
<body>
    <form id="form1" runat="server">
    <div class="mainwrapper">
        <div class="header">
            <h1><a href="Default.aspx">Strona główna</a> - <a href="ManageUsers.aspx">Użytkownicy</a></h1>
        </div>
            <div class="menuHeader">
                <ul>
                    <% if(Session["logged_level"].ToString().Equals(true.ToString())) 
                       { 
                           %><li class="menuHeaderExtraLi">Zalogowany jako: <strong><% Response.Write((String)Session["logged as"]); %></strong> </li><%
                           %><li><a href="ManageUsers.aspx" class="btn">Zarządzanie użytkownikami</a></li> <%
                           %><li><a href="FailureService.aspx" class="btn">Obsługa zgłoszeń</a></li> <%        
                       } %>
                    <li>
                        <a href="LogOut.aspx" class="btn">Wyloguj</a>
                    </li>
                </ul>
            </div>
        <table class="table">
            <tr>
                <td style="width: 35%;">Usunąć użytkonwika:</td>
                <td>
                    <% Response.Write(getLogin()); %>
                </td>
            </tr>
            <tr>
                <td></td>
                <td>
                    <asp:Button ID="BDelete" runat="server" Text="Tak" OnClick="BDelete_Click" />
                    <asp:Button ID="BNo" runat="server" Text="Nie" OnClick="BNo_Click" />
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
