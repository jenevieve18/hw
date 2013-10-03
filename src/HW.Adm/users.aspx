<%@ Page Language="C#" MasterPageFile="~/Adm.Master" AutoEventWireup="true" CodeBehind="users.aspx.cs" Inherits="HW.Adm.users" %>
<%@ Import Namespace="HW.Core.Helpers" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
		<table width="500" border="0" cellspacing="0" cellpadding="0">
			<tr><td style="font-size:16px;" align="center">User administration</td></tr>
		</table>
		<table style="margin:20px;" width="1200" border="0" cellspacing="5" cellpadding="0">
		    <tr><td colspan="8">Search by username or email <asp:TextBox ID="search" runat=server /><asp:Button ID=OK runat=server Text=OK /><asp:Button ID=FindDupes runat=server Text="Find dupes" /></td></tr>
		    <asp:Label ID="list" EnableViewState=false runat=server />
		</table>
</asp:Content>