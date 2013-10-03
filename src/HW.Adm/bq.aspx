<%@ Page Language="C#" MasterPageFile="~/Adm.Master" AutoEventWireup="true" CodeBehind="bq.aspx.cs" Inherits="HW.Adm.bq" %>
<%@ Import Namespace="HW.Core.Helpers" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
		<table width="500" border="0" cellspacing="0" cellpadding="0">
			<tr><td style="font-size:16px;" align="center">Background Questions</td></tr>
		</table>
		<table style="margin:20px;" width="500" border="0" cellspacing="0" cellpadding="0">
			<asp:Label ID=BQ runat=server />
		</table>
		<span style="margin:20px;">[<a href="bqSetup.aspx">Add</a>]</span>
</asp:Content>

