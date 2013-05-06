<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Projects.aspx.cs" Inherits="HW.EForm.Projects" %>
<%@ Import Namespace="HW.Core.Helpers" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<h3>Projects</h3>

<p>All projects for eForm.</p>

<%= FormHelper.OpenForm("ProjectsAdd.aspx") %>
<p>
	<%= FormHelper.Input("Internal", "", "Add a project...") %>
	<%= BootstrapHelper.Button("Submit", "Add a project", "btn btn-success", "icon-plus") %>
</p>
</form>

<table class="table table-hover">
	<tr>
		<th>Internal</th>
		<th>Actions</th>
	</tr>
	<% foreach (var p in projects) { %>
	<tr>
		<td><%= HtmlHelper.Anchor(p.Internal, "ProjectsShow.aspx?ProjectID=" + p.Id) %></td>
		<td>
			<%= HtmlHelper.Anchor("Edit", "ProjectsEdit.aspx?ProjectID=" + p.Id) %>
			<%= HtmlHelper.Anchor("Delete", "") %>
		</td>
	</tr>
	<% } %>
</table>

</asp:Content>
