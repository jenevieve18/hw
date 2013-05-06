<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ReportsShow.aspx.cs" Inherits="HW.EForm.ReportsShow" %>
<%@ Import Namespace="HW.Core.Helpers" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<h3>Report Information</h3>
<p>Internal: <%= report.Internal %></p>

<div class="tabbable"> <!-- Only required for left/right tabs -->
	<ul class="nav nav-tabs">
		<li class="active"><a href="#tab1" data-toggle="tab">Parts</a></li>
		<li><a href="#tab3" data-toggle="tab">Project Rounds</a></li>
	</ul>
	<div class="tab-content">
		<div class="tab-pane active" id="tab1">
      
			<p>Report parts</p>

			<%= FormHelper.OpenForm("ReportPartsAdd.aspx?ReportID=" + report.Id) %>
				<p>
					<%= FormHelper.Input("Internal", "", "Add a report part internal...") %>
					<%= BootstrapHelper.Button("Submit", "Add report part", "btn btn-success", "icon-plus") %>
				</p>
			</form>

			<table class="table table-hover">
				<tr>
					<th>Internal</th>
					<th>Type</th>
					<th>Actions</th>
				</tr>
				<% if (report.Parts != null) { %>
					<% foreach (var p in report.Parts) { %>
					<tr>
						<td><%= HtmlHelper.Anchor(p.Internal, "ReportPartsShow.aspx?ReportPartID=" + p.Id) %></td>
						<td><%= p.Type %></td>
						<td>
							<%= HtmlHelper.Anchor("Edit", "ReportPartsEdit.aspx?ReportPartID=" + p.Id) %>
							<%= HtmlHelper.Anchor("Delete", "ReportPartsDelete.aspx?ReportPartID=" + p.Id) %>
						</td>
					</tr>
					<% } %>
				<% } %>
			</table>
		</div>

		<div class="tab-pane" id="tab3">
	
			<p>Project round units</p>

			<table class="table table-hover">
				<tr>
					<th>Internal</th>
					<th>Started</th>
					<th>Actions</th>
				</tr>
				<% if (report.ProjectRounds != null) { %>
					<% foreach (var pr in report.ProjectRounds) { %>
					<tr>
						<td><%= HtmlHelper.Anchor(pr.Internal, "ProjectRoundsShow.aspx?ProjectRoundID=" + pr.Id) %></td>
						<td><%= pr.Started %></td>
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
