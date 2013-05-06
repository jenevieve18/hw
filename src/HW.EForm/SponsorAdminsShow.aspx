<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SponsorAdminsShow.aspx.cs" Inherits="HW.EForm.SponsorAdminsShow" %>
<%@ Import Namespace="HW.Core.Helpers" %>
<%@ Import Namespace="HW.Core.Models" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<h3>Sponsor admin information</h3>

<p>Name: <%= admin.Name %></p>
<p>Password: <%= admin.Password %></p>

<div class="tabbable"> <!-- Only required for left/right tabs -->
  <ul class="nav nav-tabs">
    <li class="active"><a href="#tab1" data-toggle="tab">Functions</a></li>
    <li><a href="#tab2" data-toggle="tab">Departments</a></li>
  </ul>
  <div class="tab-content">
    <div class="tab-pane active" id="tab1">
      <p>Functions.</p>
	  <p>
		<%= FormHelper.DropdownList<ManagerFunction>("Function", functions) %>
		<%= BootstrapHelper.Button("Submit", "Add manager function", "btn btn-success", "icon-plus") %>
	  </p>
	  <table class="table table-hover">
		<tr>
			<th>Function</th>
			<th>Actions</th>
		</tr>
		<% foreach (var f in admin.Functions) { %>
		<tr>
			<td><%= f.Function.Function %></td>
			<td>
				<%= HtmlHelper.Anchor("Edit", "") %>
				<%= HtmlHelper.Anchor("Delete", "") %>
			</td>
		</tr>
		<% } %>
	  </table>
    </div>
    <div class="tab-pane" id="tab2">
      <p>Departments</p>
	  <p>
		<%= FormHelper.DropdownList<Department>("Department", departments) %>
		<%= BootstrapHelper.Button("Submit", "Add a department", "btn btn-success", "icon-plus") %>
	  </p>
	  <table class="table table-hover">
		<tr>
			<th>Department</th>
			<th>Actions</th>
		</tr>
		<% foreach (var d in admin.Departments) { %>
		<tr>
			<td><%= d.Department.Name %></td>
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
