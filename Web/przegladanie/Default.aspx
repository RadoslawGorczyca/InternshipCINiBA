<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="przegladanie_Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Zgłoszenia</title>
    <link href="../Styles/przegladanie.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>

        <asp:AccessDataSource ID="accessDataSourceFloor" runat="server" DataFile="~/Data/strefy_CINiBA.accdb" SelectCommand="SELECT DISTINCT floor FROM areas WHERE floor <> '' ORDER BY floor"></asp:AccessDataSource>

        Piętro: <asp:DropDownList ID="dropDownListFloor" runat="server" DataSourceID="accessDataSourceFloor" DataTextField="floor" DataValueField="floor" AutoPostBack="True" OnDataBound="dropDownListFloor_DataBound" OnSelectedIndexChanged="dropDownListFloor_SelectedIndexChanged"></asp:DropDownList>
        <br />
        
        <asp:AccessDataSource ID="accessDataSourceArea" runat="server" DataFile="~/Data/strefy_CINiBA.accdb"></asp:AccessDataSource>

        Strefa: <asp:DropDownList ID="dropDownListArea" runat="server" DataSourceID="" DataTextField="area" DataValueField="area" AutoPostBack="True" OnDataBound="dropDownListArea_DataBound" OnSelectedIndexChanged="dropDownListArea_SelectedIndexChanged"></asp:DropDownList>
        <br />

        <asp:AccessDataSource ID="accessDataSourceStand" runat="server" DataFile="~/Data/strefy_CINiBA.accdb"></asp:AccessDataSource>

        Stanowisko: <asp:DropDownList ID="dropDownListStand" runat="server" DataSourceID="" DataTextField="stand" DataValueField="stand" AutoPostBack="True" OnDataBound="dropDownListStand_DataBound" OnSelectedIndexChanged="dropDownListStand_SelectedIndexChanged"></asp:DropDownList>
        <br />
        
        <asp:AccessDataSource ID="accessDataSourceStandFailure" runat="server" DataFile="~/Data/Failures.accdb"></asp:AccessDataSource>

        <br />

        <asp:PlaceHolder ID="placeHolderEmptyGridViewStandFailure" runat="server" Visible="False">
            результатов не найдено
        </asp:PlaceHolder>

        <asp:GridView ID="gridViewStandFailure" runat="server" AutoGenerateColumns="False" AllowPaging="True" PageSize="10" OnPageIndexChanging="gridViewStandFailure_PageIndexChanging" GridLines="None" CssClass="tableStandFailureCSSClass">
            <Columns>
                <asp:BoundField DataField="sendDate" DataFormatString="{0:yyyy-MM-dd HH:mm:ss}" HeaderText="Data" />
                <asp:BoundField DataField="content" HeaderText="Treść" />
                <asp:BoundField DataField="surname" HeaderText="Imię i nazwisko" />
                <asp:TemplateField HeaderText="Komentarz">
                    <ItemTemplate>
                        <%# replaceCRLFWithBR((string)Eval("comments")) %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Zarchiwizowane">
                    <ItemTemplate>
                        <%# getArchivedString((bool)Eval("archived")) %>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <PagerSettings Mode="NumericFirstLast" />
        </asp:GridView>

    </div>
    </form>
</body>
</html>
