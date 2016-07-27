<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Archive.aspx.cs" Inherits="web.Archive" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title><%= pageTitle %></title>
</head>
<body>
    <form id="form1" runat="server">
    <div class="mainwrapper">
        <div class="header">
            <h1><a href="Default.aspx">Strona główna</a> - <a href="Archive.aspx">Zlecenia zarchiwizowane</a></h1>
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
                <a href="Archive.aspx" class="btn">Zarchiwizowane</a>
                <a href="FailureOthers.aspx" class="btn">Cudze</a>
                <a href="FailureService.aspx" class="btn">Zlecenia własne i nowe</a>
            </div>
        </div>
        <table class="table2">
            <tr>
                <td>Treść</td>
                <td style="width:20%; border-left: 1px #CCC solid;"><a href="FailureService.aspx">Nazwisko</a></td>
                <td style="width:20%; border-left: 1px #CCC solid;"><a href="FailureService.aspx">Data</a></td>
                <td style="width:20%; border-left: 1px #CCC solid;"><a href="FailureService.aspx">Właściciel</a></td>
                <td style="width:20%; border-left: 1px #CCC solid;"><a href="FailureService.aspx">Dotyczy</a></td>
                <td style="width:20%; border-left: 1px #CCC solid;"><a href="FailureService.aspx">Temat</a></td>
            </tr>
            <% Response.Write(getArchive()); %>
        </table>
        <div class="pagination">
            <div>
                <% Response.Write(getPagination()); %>
            </div>
        </div>
        <asp:Button ID="Button1" runat="server" Text="PDF" OnClick="pdfClick" />
    </div>
    </form>
</body>
</html>