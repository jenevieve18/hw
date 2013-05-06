<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Languages.aspx.cs" Inherits="HW.EForm.Languages" %>
<%@ Import Namespace="HW.Core.Helpers" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<h3>Languages</h3>
<p>All languages supported.</p>

<%= FormHelper.OpenForm("LanguagesAdd.aspx") %>
	<%= FormHelper.Input("Name", "", "Add a new language...") %>
	<%= BootstrapHelper.Button("Submit", "Add a language", "btn btn-success", "icon-plus") %>
</form>

<table class="table table-hover">
	<tr>
		<th>Language</th>
		<th>Actions</th>
	</tr>
	<% foreach (var l in languages) { %>
	<tr>
		<td><%= l.Name %></td>
		<td>
			<%= HtmlHelper.Anchor("Edit", "LanguagesEdit.aspx?LangID=" + l.Id) %>
			<%= HtmlHelper.Anchor("Delete", "LanguagesDelete.aspx?LangID=" + l.Id) %>
		</td>
	</tr>
	<% } %>
</table>

</asp:Content>
