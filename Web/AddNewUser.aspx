<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddNewUser.aspx.cs" Inherits="web.AddNewUser" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
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
                           %><li><a href="Default.aspx" class="btn">Obsługa zgłoszeń</a></li> <%        
                       } %>
                    <li>
                        <a href="LogOut.aspx" class="btn">Wyloguj</a>
                    </li>
                </ul>
            </div>

        <table class="table">
            <% if (Request.QueryString["badPassword"] != null || Request.QueryString["userExists"] != null || Request.QueryString["passwordLength"] != null || Request.QueryString["logineLength"] != null || Request.QueryString["empty"] != null || Request.QueryString["emailLength"] != null || Request.QueryString["surnameLength"] != null)
               { %>
             <tr>
                <td>&nbsp;</td>
                <td>
                    <% if (Request.QueryString["badPassword"] != null) { %>
                    <span class="error">Hasła się nie zgadzają</span>
                    <% } else if(Request.QueryString["userExists"] != null) { %>
                    <span class="error">Użytkownik z taką nazwą już istnieje</span>
                    <% } else if(Request.QueryString["passwordLength"] != null) { %>
                    <span class="error">Hasło: Minimum 6 znaków</span>
                    <% } else if(Request.QueryString["loginLength"] != null) { %>
                    <span class="error">Nazwa użytkownika: Minimum znaków</span>
                    <% } else if(Request.QueryString["empty"] != null) { %>
                    <span class="error">Uzupełnij wszystkie pola</span>
                    <% } else if (Request.QueryString["emailLength"] != null) { %>
                    <span class="error">Email: Minimum 3 znaki</span>
                    <% } else if (Request.QueryString["surnameLength"] != null) { %>
                    <span class="error">Nazwisko: Minimum dwa znaki</span>
                    <% }%>
                </td>
            </tr>
            <% } %>
            <tr>
                <td>Nazwa użytkownika</td>
                <td>
                    <asp:TextBox ID="TNLogin" runat="server" Width="250px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>Nazwisko</td>
                <td>
                    <asp:TextBox ID="TNSurname" runat="server" Width="250px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>Email</td>
                <td>
                    <asp:TextBox ID="TNEmail" runat="server" Width="250px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>Hasło</td>
                <td>
                    <asp:TextBox ID="TNPassword" runat="server" Width="250px" TextMode="Password"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>Powtórz hasło</td>
                <td>
                    <asp:TextBox ID="TNPassword2" runat="server" Width="250px" TextMode="Password"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>Admin</td>
                <td>
                    <asp:CheckBox ID="CBAdmin" runat="server" />
                </td>
            </tr>
            <tr>
                <td></td>
                <td>
                    <asp:Button ID="BAdd" runat="server" Text="Dodaj" OnClick="BAdd_Click" />
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>