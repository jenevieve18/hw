<%@ Page Language="C#" MasterPageFile="~/Adm.Master" AutoEventWireup="true" CodeBehind="grpUser.aspx.cs" Inherits="HW.Adm.grpUser" %>
<%@ Import Namespace="HW.Core.Helpers" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
		<div style="padding:20px;">
		<a href="grpUserSetup.aspx?Rnd=<%=(new Random(unchecked((int)DateTime.Now.Ticks))).Next()%>">Add manager</a><br /><br />
		<table border="0" cellpadding="0" cellspacing="0">
		    <tr><td><b>User</b>&nbsp;&nbsp;</td><td><b>Sponsor</b></td></tr>
		    <asp:Label ID=List runat=server />
		</table>
		</div>
</asp:Content>