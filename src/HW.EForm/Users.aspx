<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Users.aspx.cs" Inherits="HW.EForm.Users" %>
<%@ Import Namespace="HW.Core.Helpers" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<h3>Users</h3>

<p><%= BootstrapHelper.Anchor("Add a user", "UsersAdd.aspx") %></p>

<table class="table table-hover">
	<tr>
		<th>User name</th>
		<th>Actions</th>
	</tr>
	<tr>
		<td></td>
		<td></td>
	</tr>
</table>

</asp:Content>
