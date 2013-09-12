<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ExerciseAreas.aspx.cs" Inherits="HW.EForm.ExerciseAreas" %>
<%@ Import Namespace="HW.Core.Helpers" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<h3>Exercise Area List</h3>

<p><%= BootstrapHelper.Anchor("Add an exercise area", "ExerciseAreasAdd.aspx", "btn btn-success", "icon-plus") %></p>

<table class="table table-hovered">
    <tr>
        <th>Area</th>
        <th>Language</th>
        <th>Actions</th>
    </tr>
    <% foreach (var a in areas) { %>
    <tr>
        <td><%= a.AreaName %></td>
        <td><%= a.Language.Name %></td>
        <td>
            
        </td>
    </tr>
    <% } %>
</table>

</asp:Content>
