<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FailureArchive.aspx.cs" Inherits="web.FailureArchive" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title><%= pageTitle %></title>
</head>
<body>
    <form id="form1" runat="server">
    <div class="mainwrapper">
        <div class="header">
            <h1><a href="Default.aspx">Strona główna</a> - <a href="Archive.aspx">Archiwum</a></h1>
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
        <table class="table">
            <tr>
                <td style="width: 20%; border-right: 1px #CCC solid; border-bottom: 1px #CCC solid;">Nazwisko</td>
                <td style="border-bottom: 1px #CCC solid;">
                    <asp:Label ID="TName" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr>
                <td style="border-right: 1px #CCC solid; border-bottom: 1px #CCC solid;">E-mail</td>
                <td style="border-bottom: 1px #CCC solid;">
                    <asp:Label ID="TMail" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr>
                <td style="border-right: 1px #CCC solid; border-bottom: 1px #CCC solid;">Data</td>
                <td style="border-bottom: 1px #CCC solid;">
                    <asp:Label ID="TDate" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr>
                <td style="border-right: 1px #CCC solid; border-bottom: 1px #CCC solid;">Piętro</td>
                <td style="border-bottom: 1px #CCC solid;">
                    <asp:Label ID="TFloor" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr>
                <td style="border-right: 1px #CCC solid; border-bottom: 1px #CCC solid;">Strefa</td>
                <td style="border-bottom: 1px #CCC solid;">
                    <asp:Label ID="TArea" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr>
                <td style="border-right: 1px #CCC solid; border-bottom: 1px #CCC solid;">Stanowisko</td>
                <td style="border-bottom: 1px #CCC solid;">
                    <asp:Label ID="TStand" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr>
                <td style="border-right: 1px #CCC solid; border-bottom: 1px #CCC solid;">Temat</td>
                <td style="border-bottom: 1px #CCC solid;">
                    <asp:Label ID="TTemat" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr>
                <td style="border-right: 1px #CCC solid; border-bottom: 1px #CCC solid;">Awaria</td>
                <td style="border-bottom: 1px #CCC solid;">
                    <asp:TextBox ID="TFailure" TextMode="multiline" readonly = 'true' runat="server" Width="100%" Rows="15" BorderStyle="None" ForeColor="#646464"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="border-right: 1px #CCC solid; border-bottom: 1px #CCC solid;">Komentarz</td>
                <td style="border-bottom: 1px #CCC solid;">
                    <asp:TextBox ID="TOldComment" TextMode="multiline" readonly = 'true' runat="server" Width="100%" Rows="4" BorderStyle="None" ForeColor="#959595"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="border-right: 1px #CCC solid; border-bottom: 1px #CCC solid;">Ilość przekazań</td>
                <td style="border-bottom: 1px #CCC solid;">
                    <asp:Label ID="TForwarded" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr>
                <td style="border-right: 1px #CCC solid;">Zarchiwizowane</td>
                <td style="border-bottom: 1px #CCC solid;">
                    <asp:CheckBox ID="cmArchive" runat="server"></asp:CheckBox>
                </td>
            </tr>
            <tr>
                <td><asp:Button ID="Button1" runat="server" Text="PDF" OnClick="pdfClick" /></td>
                <td>
                    <asp:Button ID="BBack" runat="server" Text="Przywróć" style="height: 26px" OnClick="BBack_Click" />
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>