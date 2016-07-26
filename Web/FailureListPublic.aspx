<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FailureListPublic.aspx.cs" Inherits="web.FailureListPublic" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<title>Zlecenia</title>
</head>
<body>
	<form id="form1" runat="server">
	<div class="mainwrapper">
		<div class="header">
			<h1>Zlecenia</h1>
		</div>
		<div class="afterMenu">
			<div class="menuHeader">
				TU BĘDĄ DROPBOXY
			</div>
		</div>
		<table class="table2">
			<tr>
				<td>Data</td>
				<td>Imię i nazwisko</td>
				<td>Treść</td>
				<td>Status realizacji</td>
			</tr>
				<
				<% Response.Write(getActive()); %>
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
