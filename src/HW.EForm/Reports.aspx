<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Reports.aspx.cs" Inherits="HW.EForm.Reports" %>
<%@ Import Namespace="HW.Core.Helpers" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<h3>Reports</h3>

<p>All reports for eForm.</p>

<p><%= BootstrapHelper.Anchor("Add a report", "ReportsAdd.aspx", "btn btn-success", "icon-plus") %></p>

<table class="table table-hover">
	<tr>
		<th>Internal</th>
		<th>Report Key</th>
		<th>Actions</th>
	</tr>
	<% foreach (var r in reports) { %>
	<tr>
		<td><%= HtmlHelper.Anchor(r.Internal, "ReportsShow.aspx?ReportID=" + r.Id) %></td>
		<td><%= r.ReportKey %></td>
		<td>
			<%= HtmlHelper.Anchor("Edit", "ReportsEdit.aspx?ReportID=" + r.Id) %>
			<%= HtmlHelper.Anchor("Delete", "ReportsDelete.aspx?ReportID=" + r.Id) %>
		</td>
	</tr>
	<% } %>
</table>

</asp:Content>
