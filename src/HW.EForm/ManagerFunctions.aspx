<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ManagerFunctions.aspx.cs" Inherits="HW.EForm.ManagerFunctions" %>
<%@ Import Namespace="HW.Core.Helpers" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<h3>Manager functions</h3>
<p>Functions supported for navigation purposes.</p>

<p><%= BootstrapHelper.Anchor("Add a manager function", "ManagerFunctionsAdd.aspx", "btn btn-success", "icon-plus") %></p>

<table class="table table-hover">
	<tr>
		<th>Function</th>
		<th>URL</th>
		<th>Actions</th>
	</tr>
	<% foreach (var f in functions) { %>
	<tr>
		<td><%= f.Function %></td>
		<td><%= f.URL %></td>
		<td>
			<%= HtmlHelper.Anchor("Edit", "") %>
			<%= HtmlHelper.Anchor("Delete", "") %>
		</td>
	</tr>
	<% } %>
</table>

</asp:Content>
