<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ExerciseCategories.aspx.cs" Inherits="HW.EForm.ExerciseCategories" %>
<%@ Import Namespace="HW.Core.Helpers" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<h3>Exercise Category List</h3>

<p><%= BootstrapHelper.Anchor("Add an exercise category", "ExerciseCategoriesAdd.aspx", "btn btn-success", "icon-plus") %></p>

<table class="table table-hover">
    <tr>
        <th>Category</th>
        <th>Language</th>
        <th>Actions</th>
    </tr>
    <% foreach (var c in categories) { %>
    <tr>
        <td><%= HtmlHelper.Anchor(c.ToString(), "ExerciseCategoriesShow.aspx?ExerciseCategoryLangID=" + c.Id)%></td>
        <td><%= c.Language.Name %></td>
        <td>
            <%= HtmlHelper.Anchor("Edit", "ExerciseCategoriesEdit.aspx?ExerciseCategoryLangID=" + c.Id)%>
            <%= HtmlHelper.Anchor("Delete", "") %>
        </td>
    </tr>
    <% } %>
</table>

</asp:Content>
