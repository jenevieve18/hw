<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Departments.aspx.cs" Inherits="HW.EForm.Departments" %>
<%@ Import Namespace="HW.Core.Helpers" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<h3>Departments</h3>
<p>All departments in health watch</p>
<p>
	<%= FormHelper.Input("Name", "", "Add a department...") %>
	<%= BootstrapHelper.Anchor("Add a department", "DepartmentsAdd.aspx", "btn btn-success", "icon-plus") %>
</p>

<table class="table table-hover">
	<tr>
		<th>Department name</th>
		<th>Actions</th>
	</tr>
	<% foreach (var d in departments) { %>
	<tr>
		<td><%= d.Name %></td>
		<td>
			<%= HtmlHelper.Anchor("Edit", "DepartmentsEdit.aspx?DepartmentID=" + d.Id) %>
			<%= HtmlHelper.Anchor("Delete", "DepartmentsDelete.aspx?DepartmentID=" + d.Id) %>
		</td>
	</tr>
	<% } %>
</table>

</asp:Content>
