<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ProjectsShow.aspx.cs" Inherits="HW.EForm.ProjectsShow" %>
<%@ Import Namespace="HW.Core.Helpers" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<h3>Project Information</h3>

<h4><%= project.Internal %></h4>

<div class="tabbable"> <!-- Only required for left/right tabs -->
  <ul class="nav nav-tabs">
    <li class="active"><a href="#tab1" data-toggle="tab">Rounds</a></li>
    <li><a href="#tab2" data-toggle="tab">Section 2</a></li>
  </ul>
  <div class="tab-content">
    <div class="tab-pane active" id="tab1">
      <p>Rounds</p>
	  <table class="table table-hover">
		<tr>
			<th>Internal</th>
			<th>Actions</th>
		</tr>
		<% if (project.Rounds != null) { %>
			<% foreach (var r in project.Rounds) { %>
			<tr>
				<td><%= r.Internal %></td>
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
      <p>Howdy, I'm in Section 2.</p>
    </div>
  </div>
</div>

</asp:Content>
