<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ProjectRoundsShow.aspx.cs" Inherits="HW.EForm.ProjectRoundsShow" %>
<%@ Import Namespace="HW.Core.Helpers" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<h3>Project Round Information</h3>

<div class="tabbable"> <!-- Only required for left/right tabs -->
  <ul class="nav nav-tabs">
    <li class="active"><a href="#tab1" data-toggle="tab">Units</a></li>
    <li><a href="#tab2" data-toggle="tab">Languages</a></li>
  </ul>
  <div class="tab-content">
    <div class="tab-pane active" id="tab1">
      <p>Project round units</p>
	  <table class="table table-hover">
		<tr>
			<th>Name</th>
			<th>Actions</th>
		</tr>
		<% foreach (var u in round.Units) { %>
		<tr>
			<td><%= u.Name %></td>
			<td>
				<%= HtmlHelper.Anchor("Edit", "") %>
				<%= HtmlHelper.Anchor("Delete", "") %>
			</td>
		</tr>
		<% } %>
	  </table>

    </div>
    <div class="tab-pane" id="tab2">
      <p>Project round languages</p>
	  <table class="table table-hover">
		<tr>
			<th>Language</th>
			<th>Invitation Subject</th>
			<th>Actions</th>
		</tr>
		<% foreach (var l in round.Languages) { %>
		<tr>
			<td><%= l.Language.Name %></td>
			<td><%= l.InvitationSubject %></td>
			<td>
				<%= HtmlHelper.Anchor("Edit", "") %>
				<%= HtmlHelper.Anchor("Delete", "") %>
			</td>
		</tr>
		<% } %>
	  </table>
    </div>
  </div>
</div>

</asp:Content>
