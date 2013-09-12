<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ExercisesShow.aspx.cs" Inherits="HW.EForm.ExercisesShow" %>
<%@ Import Namespace="HW.Core.Helpers" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<h3>Exercise information</h3>

<p><%= exercise.Image %></p>

<h4>Exercise Variants</h4>

<table class="table table-hover">
    <tr>
        <th>File</th>
        <th>Language</th>
        <th>Actions</th>
    </tr>
    <% foreach (var v in exercise.Variants) { %>
        <% if (v.Languages != null) { %>
            <% foreach (var l in v.Languages) { %>
            <tr>
                <td><%= l.File %></td>
                <td><%= l.Language.Name %></td>
                <td>
                    <%= HtmlHelper.Anchor("Edit", "") %>
                    <%= HtmlHelper.Anchor("Delete", "") %>
                </td>
            </tr>
            <% } %>
        <% } %>
    <% } %>
</table>

</asp:Content>
