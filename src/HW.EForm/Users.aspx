<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Users.aspx.cs" Inherits="HW.EForm.Users" %>
<%@ Import Namespace="HW.Core.Helpers" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<h3>Users</h3>

<p><%= BootstrapHelper.Anchor("Add a user", "UsersAdd.aspx", "btn btn-success", "icon-plus") %></p>

<table class="table table-hover">
	<tr>
		<th>User name</th>
		<th>Actions</th>
	</tr>
	<% foreach (var u in users) { %>
	<tr>
		<td><%= u.Name %></td>
		<td>
			<%= HtmlHelper.Anchor("Edit", "") %>
			<%= HtmlHelper.Anchor("Delete", "") %>
		</td>
	</tr>
	<% } %>
</table>

</asp:Content>
