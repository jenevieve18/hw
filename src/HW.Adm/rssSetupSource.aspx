<%@ Page Language="C#" MasterPageFile="~/Adm.Master" AutoEventWireup="true" CodeBehind="rssSetupSource.aspx.cs" Inherits="HW.Adm.rssSetupSource" %>
<%@ Import Namespace="HW.Core.Helpers" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
		<table width="500" border="0" cellspacing="0" cellpadding="0">
			<tr><td style="font-size:16px;" align="center">RSS Source setup</td></tr>
		</table>
		<table style="margin:20px;" width="500" border="0" cellspacing="0" cellpadding="0">
		    <tr><td>Source&nbsp;</td><td><asp:TextBox ID=source runat=server Width="300" /></td></tr>
			<tr><td>Source shortname&nbsp;</td><td><asp:TextBox ID=sourceShort runat=server Width="300" /></td></tr>
			<tr><td>Favourite&nbsp;</td><td><asp:CheckBox ID=Favourite runat=server /></td></tr>
			<tr><td><asp:Button ID=Save runat=server Text=Save /></td></tr>
		</table>
</asp:Content>
