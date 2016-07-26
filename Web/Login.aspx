<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="web.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title><%= pageTitle %></title>
</head>

<body>
    <form id="form1" runat="server">
    <div class="mainwrapper">
        <div class="header">
            <h1><a href="DefaultBefore.aspx">Strona główna</a> - <a href="FailureService.aspx">Logowanie</a></h1>
        </div>
        <table class="table">
            <tr class="trBorder">
                <td>
                    <asp:Label ID="Label1" style="font-weight: 700" runat="server" Text="Login"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="TBLogin" runat="server" Width="250px"></asp:TextBox>
                </td>
            </tr>
            <tr class="trBorder">
                <td>
                    <asp:Label ID="Label2" style="font-weight: 700" runat="server" Text="Password"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="TBPassword" TextMode="Password" runat="server" Width="250px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td >
                    <asp:Button ID="ButtonLogin" OnClick="ButtonClick" runat="server" Text="Log In" />
                </td>
            </tr>
        </table>
    
    </div>
    </form>
</body>
</html>
