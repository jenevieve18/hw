<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="BackgroundQuestionsShow.aspx.cs" Inherits="HW.EForm.BackgroundQuestionsShow" %>
<%@ Import Namespace="HW.Core.Helpers" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<h3>Background question information</h3>

<h4><%= question.Internal %></h4>

<div class="tabbable"> <!-- Only required for left/right tabs -->
  <ul class="nav nav-tabs">
    <li class="active"><a href="#tab1" data-toggle="tab">Languages</a></li>
    <li><a href="#tab2" data-toggle="tab">Some</a></li>
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
			<td><%= l.Question %></td>
			<td>
				<%= HtmlHelper.Anchor("Edit", "") %>
				<%= HtmlHelper.Anchor("Delete", "") %>
			</td>
		</tr>
		<% } %>
	  </table>

	  <%= FormHelper.OpenForm("BackgroundQuestionLanguagesAdd.aspx") %>
	  <p>
		<%= FormHelper.Input("Internal", "", "Add a new internal...") %>
		<%= FormHelper.DropdownList("Language", languages, "input-small") %>
		<%= BootstrapHelper.Button("Submit", "Add a background question language", "btn btn-success", "icon-plus") %>
	  </p>
	  </form>

    </div>
    <div class="tab-pane" id="tab2">
      <p>Some</p>
    </div>
  </div>
</div>

</asp:Content>
