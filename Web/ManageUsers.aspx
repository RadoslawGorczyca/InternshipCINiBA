<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ManageUsers.aspx.cs" Inherits="web.ManageUsers" %>

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
        
        <div class="afterMenu">
            <div class="left">
                <span class="badgeBlue">Użytkowników: <% Response.Write(getNumOfUsers()); %></span>
                <% if (Request.QueryString["userDeleted"] != null) { %>
                    <span class="success">Użytkownik usunięty</span>
                <% } else if (Request.QueryString["userAdded"] != null) { %>
                    <span class="success">Użytkownik dodany</span>
                <% } else if (Request.QueryString["userEdited"] != null) { %>
                    <span class="success">Użytkownik edytowany</span>
                <% } %>
            </div>
            <div class="menuHeader">
                <a href="AddNewUser.aspx" class="btn">Dodaj nowego użytkownika</a>
            </div>
        </div>
        <table class="table2">
            <tr>
                <td style="width: 70%;">Nazwa użytkownika</td>
                <td style="width: 15%;"></td>
                <td style="width: 15%;"></td>
            </tr>
            <% Response.Write(getUsers()); %>
        </table>
        <div class="pagination">
            <div>
                <% Response.Write(getPagination()); %>
            </div>
        </div>
    </div>
    </form>
</body>
</html>
