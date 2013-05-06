<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SponsorsShow.aspx.cs" Inherits="HW.EForm.SponsorsShow" %>
<%@ Import Namespace="HW.Core.Helpers" %>
<%@ Import Namespace="HW.Core.Models" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<h3>Sponsor Information</h3>

<h4><%= sponsor.Name %></h4>

<div class="tabbable"> <!-- Only required for left/right tabs -->
  <ul class="nav nav-tabs">
    <li class="active"><a href="#tab1" data-toggle="tab">Admins</a></li>
    <li><a href="#tab2" data-toggle="tab">Invites</a></li>
    <li><a href="#tab3" data-toggle="tab">Background Questions</a></li>
    <li><a href="#tab4" data-toggle="tab">Project Round Units</a></li>
  </ul>
  <div class="tab-content">
    <div class="tab-pane active" id="tab1">
      <p>Admins</p>
	  <%= FormHelper.OpenForm("SponsorAdminsAdd.aspx?SponsorID=" + sponsor.Id)%>
		<p>
			<%= FormHelper.Input("Name", "", "Add user name...") %>
			<%= FormHelper.Password("Password", "", "Password") %>
			<%= BootstrapHelper.Button("Submit", "Add sponsor invite", "btn btn-success", "icon-plus") %>
		</p>
	  </form>
	  <table class="table table-hover">
		<tr>
			<th>Name</th>
			<th>Password</th>
			<th>Actions</th>
		</tr>
		<% if (sponsor.Admins != null) { %>
			<% foreach (var a in sponsor.Admins) { %>
			<tr>
				<td><%= a.Name %></td>
				<td><%= a.Password %></td>
				<td>
					<%= HtmlHelper.Anchor("Edit", "") %>
					<%= HtmlHelper.Anchor("Delete", "") %>
				</td>
			</tr>
			<% } %>
		<% } %>
	  </table>
    </div>
    <div class="tab-pane" id="tab2">
      <p>Invites</p>
	  <table class="table table-hover">
		<tr>
			<th>Department</th>
			<th>Email</th>
			<th>Actions</th>
		</tr>
		<% if (sponsor.Invites != null) { %>
			<% foreach (var i in sponsor.Invites) { %>
			<tr>
				<td><%= i.Department.Name %></td>
				<td><%= i.Email %></td>
				<td>
					<%= HtmlHelper.Anchor("Edit", "") %>
					<%= HtmlHelper.Anchor("Delete", "") %>
				</td>
			</tr>
			<% } %>
		<% } %>
	  </table>
    </div>
    <div class="tab-pane" id="tab3">
      <p>Background Questions</p>
	  <table class="table table-hover">
		<tr>
			<th>Question</th>
			<th>Actions</th>
		</tr>
		<% if (sponsor.BackgroundQuestions != null) { %>
			<% foreach (var bq in sponsor.BackgroundQuestions) { %>
			<tr>
				<td><%= HtmlHelper.Anchor(bq.BackgroundQuestion.Internal, "SponsorBackgroundQuestionsShow.aspx?SponsorBQID=" + bq.Id) %></td>
				<td>
					<%= HtmlHelper.Anchor("Edit", "") %>
					<%= HtmlHelper.Anchor("Delete", "") %>
				</td>
			</tr>
			<% } %>
		<% } %>
	  </table>
    </div>
    <div class="tab-pane" id="tab4">
      <p>Project Round Units</p>
	  <p>
		<%= FormHelper.DropdownList<ProjectRoundUnit>("ProjectRoundUnit", units) %>
		<%= BootstrapHelper.Button("Submit", "Add project round unit", "btn btn-success", "icon-plus") %>
	  </p>
	  <table class="table table-hover">
		<tr>
			<th>Project round unit</th>
			<th>Actions</th>
		</tr>
		<% if (sponsor.RoundUnits != null) { %>
			<% foreach (var r in sponsor.RoundUnits) { %>
			<tr>
				<td><%= HtmlHelper.Anchor(r.ProjectRoundUnit.Name, "SponsorProjectRoundUnitsShow.aspx?SponsorPRUID=" + r.Id) %></td>
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
