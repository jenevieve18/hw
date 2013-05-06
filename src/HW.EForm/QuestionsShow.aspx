<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="QuestionsShow.aspx.cs" Inherits="HW.EForm.QuestionsShow" %>
<%@ Import Namespace="HW.Core.Helpers" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<div class="tabbable"> <!-- Only required for left/right tabs -->
  <ul class="nav nav-tabs">
    <li class="active"><a href="#tab1" data-toggle="tab">Languages</a></li>
    <li><a href="#tab2" data-toggle="tab">Section 2</a></li>
  </ul>
  <div class="tab-content">
    <div class="tab-pane active" id="tab1">
      <p>Languages</p>
	  <table class="table table-hover">
		<tr>
			<th>Language</th>
			<th>Question</th>
			<th>Actions</th>
		</tr>
		<% foreach (var l in question.Languages) { %>
		<tr>
			<td><%= l.Language.Name %></td>
			<td><%= l.QuestionText %></td>
			<td>
				<%= HtmlHelper.Anchor("Edit", "") %>
				<%= HtmlHelper.Anchor("Delete", "") %>
			</td>
		</tr>
		<% } %>
	  </table>
    </div>
    <div class="tab-pane" id="tab2">
      <p>Howdy, I'm in Section 2.</p>
    </div>
  </div>
</div>

</asp:Content>
