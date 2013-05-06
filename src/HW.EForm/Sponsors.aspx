<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Sponsors.aspx.cs" Inherits="HW.EForm.Sponsors" %>
<%@ Import Namespace="HW.Core.Helpers" %>
<%@ Import Namespace="HW.Core.Helpers" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<h3>Sponsors List</h3>

<p>List of all sponsors for Health Watch.</p>

<p><%= BootstrapHelper.Anchor("Add a sponsor", "SponsorsAdd.aspx", "btn btn-success", "icon-plus") %></p>

<table class="table table-hover">
	<tr>
		<th>Name</th>
		<th>Invite Subject</th>
		<th>Actions</th>
	</tr>
	<% foreach (var s in sponsors) { %>
	<tr>
		<td><%= HtmlHelper.Anchor(s.Name, "SponsorsShow.aspx?SponsorID=" + s.Id) %></td>
		<td><%= s.InviteSubject %></td>
		<td>
			<%= HtmlHelper.Anchor("Edit", "SponsorsEdit.aspx?SponsorID=" + s.Id) %>
			<%= HtmlHelper.Anchor("Delete", "SponsorsDelete.aspx?SponsorID=" + s.Id) %>
		</td>
	</tr>
	<% } %>
</table>

</asp:Content>
