<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ReportPartsShow.aspx.cs" Inherits="HW.EForm.ReportPartsShow" %>
<%@ Import Namespace="HW.Core.Helpers" %>
<%@ Import Namespace="HW.Core.Models" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<h3>Report Part Information</h3>

<p><%= HtmlHelper.Anchor("Go Back", "ReportsShow.aspx?ReportID=" + part.Report.Id) %></p>

<h4><%= part.Internal %></h4>

<table>
	<tr>
		<td>Type</td>
		<td><%= part.Type %></td>
	</tr>
</table>

<div class="tabbable"> <!-- Only required for left/right tabs -->
  <ul class="nav nav-tabs">
    <li class="active"><a href="#tab1" data-toggle="tab">Languages</a></li>
    <li><a href="#tab2" data-toggle="tab">Components</a></li>
  </ul>
  <div class="tab-content">
    <div class="tab-pane active" id="tab1">
      <p>This section describes the report part languages.</p>

	  <%= FormHelper.OpenForm("ReportPartLanguagesAdd.aspx?ReportPartID=" + part.Id)%>
		<p>
			<%= FormHelper.Input("Subject", "", "Add a report part subject...") %>
			<%= FormHelper.DropdownList<Language>("Language", languages, "") %>
			<%= BootstrapHelper.Button("Submit", "Add a report part language", "btn btn-success", "icon-plus") %>
		</p>
	  </form>

      <table class="table table-hover">
	<tr>
		<th>Language</th>
		<th>Subject</th>
		<th>Header</th>
		<th>Footer</th>
		<th>Actions</th>
	</tr>
	<% if (part.Languages != null) { %>
		<% foreach (var l in part.Languages) { %>
		<tr>
			<td><%= l.Language.Name %></td>
			<td><%= l.Subject %></td>
			<td><%= l.Header %></td>
			<td><%= l.Footer %></td>
			<td>
				<%= HtmlHelper.Anchor("Edit", "ReportPartLanguagesEdit.aspx?ReportPartLangID=" + l.Id)%>
				<%= HtmlHelper.Anchor("Delete", "ReportPartLanguagesDelete.aspx?ReportPartLangID=" + l.Id)%>
			</td>
		</tr>
		<% } %>
	<% } %>
	</table>
    </div>
    <div class="tab-pane" id="tab2">
	<p>Components</p>
		<table class="table table-hover">
		<tr>
			<th>Weighted Question Option</th>
			<th>Actions</th>
		</tr>
		<% if (part.Components != null) { %>
			<% foreach (var c in part.Components) { %>
			<tr>
				<td><%= c.QuestionOption.Internal %></td>
				<td>
					<%= HtmlHelper.Anchor("Edit", "") %>
					<%= HtmlHelper.Anchor("Delete", "") %>
				</td>
			</tr>
			<% } %>
		<% } %>
</table>
    </div>
  </div>
</div>



</asp:Content>
