<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Questions.aspx.cs" Inherits="HW.EForm.Questions" %>
<%@ Import Namespace="HW.Core.Helpers" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<h3>Questions</h3>

<table class="table table-hover">
	<tr>
		<th>Internal</th>
		<th>Container</th>
		<th>Actions</th>
	</tr>
	<% foreach (var q in questions) { %>
	<tr>
		<td><%= q.Internal %></td>
		<td>
			<% if (q.Container != null) { %>
			<%= q.Container.Container %>
			<% } %>
		</td>
		<td>
			<%= HtmlHelper.Anchor("Edit", "QuestionsEdit.aspx?QuestionID=" + q.Id) %>
			<%= HtmlHelper.Anchor("Delete", "QuestionsDelete.aspx?QuestionID=" + q.Id) %>
		</td>
	</tr>
	<% } %>
</table>

</asp:Content>
