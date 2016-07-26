<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FailureService.aspx.cs" Inherits="web.FailureService" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title><%= pageTitle %></title>
</head>
<body>
    <form id="form1" runat="server">
    <div class="mainwrapper">
        <div class="header">
            <h1><a href="Default.aspx">Strona główna</a> - <a href="FailureService.aspx">Zlecenia własne i nowe</a></h1>
        </div>
            <div class="menuHeader">
                <ul>
                    <% if(Session["logged_level"].ToString().Equals(true.ToString())) 
                         %><li class="menuHeaderExtraLi">Zalogowany jako: <strong><% Response.Write((String)Session["logged as"]); %></strong> </li><% 
                       { %><li><a href="ManageUsers.aspx" class="btn">Zarządzanie użytkownikami</a></li> <% 
                         %><li><a href="FailureService.aspx" class="btn">Obsługa zgłoszeń</a></li> <%                                                                                           
                       } %>
                    <li>
                        <a href="LogOut.aspx" class="btn">Wyloguj</a>
                    </li>
                </ul>
            </div>
        <div class="afterMenu">
            <div class="menuHeader">
                <% if (Request.QueryString["saved"] != null) 
                   { 
                       %><span class="success">Zapisane</span><% 
                   } 
                   else if (Request.QueryString["archived"] != null) 
                   { 
                       %><span class="error">Pytania zarchiwizowane</span><% 
                   } %>
            </div>
       <div class="menuHeader">
                <a href="Archive.aspx" class="btn">Zarchiwizowane</a>
                <a href="FailureOthers.aspx" class="btn">Cudze</a>
                <a href="FailureService.aspx" class="btn">Zlecenia własne i nowe</a>
            </div>
        </div>
        <table class="table2">
            <tr>
                <td>Treść</td>
                <td style="width:20%; border-left: 1px #CCC solid;"><a href="FailureService.aspx<% Response.Write(getSort()); %>" style="width: 100%;background-repeat:no-repeat; background-position: 0 center; padding-left: 12px";
                    <% if(Request.QueryString["sortby"] != null) 
                        { if (Request.QueryString["sortby"].Equals("name")) 
                          { %>background-image: url(img/up.png);<% } 
                          else 
                          { %>background-image: url(img/down.png);<%} 
                        } else 
                          { %>background-image: url(img/sort.png);"<% } 
                          %>>Nazwisko</a></td>
                <td style="width:20%; border-left: 1px #CCC solid;"><a href="FailureService.aspx">Data</a></td>
                <td style="width:20%; border-left: 1px #CCC solid;"><a href="FailureService.aspx">Właściciel</a></td>
            </tr>
            <% Response.Write(getActive()); %>
        </table>
        <div class="pagination">
            <div>
                <% Response.Write(getPagination()); %>
            </div>
            <asp:Button ID="Button1" runat="server" Text="PDF" OnClick="pdfClick" />
        </div>
    </div>
    </form>
</body>
</html>
