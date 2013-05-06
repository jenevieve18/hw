<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="BackgroundQuestions.aspx.cs" Inherits="HW.EForm.BackgroundQuestions" %>
<%@ Import Namespace="HW.Core.Helpers" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<h3>Background Questions</h3>

<p><%= BootstrapHelper.Anchor("Add a background question", "BackgroundQuestionsAdd.aspx", "btn btn-success", "icon-plus") %></p>

<table class="table table-hover">
	<tr>
		<th>Internal</th>
		<th>Actions</th>
	</tr>
	<% foreach (var q in questions) { %>
	<tr>
		<td><%= HtmlHelper.Anchor(q.Internal, "BackgroundQuestionsShow.aspx?BQID=" + q.Id) %></td>
		<td>
			<%= HtmlHelper.Anchor("Edit", "") %>
			<%= HtmlHelper.Anchor("Delete", "") %>
		</td>
	</tr>
	<% } %>
</table>

</asp:Content>
