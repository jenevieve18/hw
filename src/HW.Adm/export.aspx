<%@ Page Language="C#" MasterPageFile="~/Adm.Master" AutoEventWireup="true" CodeBehind="export.aspx.cs" Inherits="HW.Adm.export" %>
<%@ Import Namespace="HW.Core.Helpers" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
		<table width="500" border="0" cellspacing="0" cellpadding="0">
			<tr><td style="font-size:16px;" align="center">Data export</td></tr>
		</table>
		<table style="margin:20px;" border="0" cellspacing="0" cellpadding="0">
			<tr><td><b>Sponsor</b>&nbsp;&nbsp;</td><td><b>Survey</b>&nbsp;&nbsp;</td></tr>
			<tr><td><asp:DropDownList AutoPostBack ID=SponsorID runat=server />&nbsp;&nbsp;</td><td><asp:DropDownList ID=SurveyID runat=server />&nbsp;&nbsp;</td><td><asp:Button ID=Execute Text="Execute" runat=server /></td></tr>
		</table>
</asp:Content>
