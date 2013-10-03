<%@ Page Language="C#" MasterPageFile="~/Adm.Master" AutoEventWireup="true" CodeBehind="rss.aspx.cs" Inherits="HW.Adm.rss" %>
<%@ Import Namespace="HW.Core.Helpers" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
		<table width="500" border="0" cellspacing="0" cellpadding="0">
			<tr><td style="font-size:16px;" align="center">News setup</td></tr>
		</table>
		<table style="margin:20px;" width="500" border="0" cellspacing="5" cellpadding="0">
		    <tr>
		        <td>RSS Feeds [<a href="rssSetupChannel.aspx">Add</a>]</td>
		        <td>RSS Sources [<a href="rssSetupSource.aspx">Add</a>]</td>
		        <td>News categories [<a href="rssSetupNewsCategory.aspx">Add</a>]</td>
		    </tr>
			<tr>
			    <td valign="top"><asp:Label ID=Channel runat=server /></td>
			    <td valign="top"><asp:Label ID=Source runat=server /></td>
			    <td valign="top"><asp:Label ID="NewsCategory" runat=server /></td>
			</tr>
		</table>
</asp:Content>