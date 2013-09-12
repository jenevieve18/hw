<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Exercises.aspx.cs" Inherits="HW.EForm.Exercises" %>
<%@ Import Namespace="HW.Core.Helpers" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<h3>Exercises</h3>
<p>Exercises in health watch.</p>
<p><%= BootstrapHelper.Anchor("Add an exercise", "ExercisesAdd.aspx", "btn btn-success", "icon-plus") %></p>

<table class="table table-hover">
	<tr>
		<th>Image</th>
		<th>Actions</th>
	</tr>
	<% foreach (var e in exercises) { %>
	<tr>
		<td><%= HtmlHelper.Anchor(e.Image, "ExercisesShow.aspx?ExerciseID=" + e.Id)%></td>
		<td>
			<%= HtmlHelper.Anchor("Edit", "") %>
			<%= HtmlHelper.Anchor("Delete", "") %>
		</td>
	</tr>
	<% } %>
</table>

</asp:Content>
