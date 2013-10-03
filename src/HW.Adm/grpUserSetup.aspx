<%@ Page Language="C#" MasterPageFile="~/Adm.Master" AutoEventWireup="true" CodeBehind="grpUserSetup.aspx.cs" Inherits="HW.Adm.grpUserSetup" %>
<%@ Import Namespace="HW.Core.Helpers" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
		<div style="padding:20px;">
		<a href="grpUser.aspx?Rnd=<%=(new Random(unchecked((int)DateTime.Now.Ticks))).Next()%>">Back</a><br /><br />
		<table border="0" cellpadding="0" cellspacing="0">
		    <tr><td>Username&nbsp;</td><td><asp:TextBox ID="Usr" runat=server />&nbsp;&nbsp;<asp:Button ID=Save runat=server Text="Save" /></td></tr>
		    <tr><td>Password&nbsp;</td><td><asp:TextBox TextMode=Password ID="Pas" runat=server /></td></tr>
		    <tr><td>Sponsor&nbsp;</td><td><asp:DropDownList AutoPostBack=true ID="SponsorID" runat=server /></td></tr>
		    <tr><td valign="top">Departments&nbsp;</td><td><asp:CheckBoxList ID="DepartmentID" runat=server /></td></tr>
		    <tr><td valign="top">Access&nbsp;</td><td><asp:CheckBoxList ID="AccessID" runat=server /></td></tr>
		</table>
		</div>
</asp:Content>