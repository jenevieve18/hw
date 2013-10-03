<%@ Page Language="C#" MasterPageFile="~/Adm.Master" AutoEventWireup="true" CodeBehind="wise.aspx.cs" Inherits="HW.Adm.wise" %>
<%@ Import Namespace="HW.Core.Helpers" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
		<table width="970" border="0" cellspacing="0" cellpadding="0">
			<tr><td style="font-size:16px;" align="center">Words of wisdom</td></tr>
		</table>
		<table style="margin:20px;" border="1" cellspacing="0" cellpadding="5">
			<asp:Label ID=Wisdom runat=server />
		</table>
</asp:Content>