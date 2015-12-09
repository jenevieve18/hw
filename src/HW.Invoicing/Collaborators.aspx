<%@ Page Title="" Language="C#" MasterPageFile="~/Invoicing.Master" AutoEventWireup="true" CodeBehind="Collaborators.aspx.cs" Inherits="HW.Invoicing.Collaborators" %>
<%@ Import Namespace="HW.Core.Helpers" %>
<%@ Import Namespace="HW.Invoicing.Core.Models" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<h3>Collaborators</h3>
<p><%= HtmlHelper.Anchor("Add a collaborator", "collaboratoradd.aspx", "class='btn btn-info'") %></p>
<div class="alert alert-info">
	<strong>Collaborator</strong> is a person who works jointly on an activity or project; an associate.
</div>
<table class="table table-hover">
    <tr>
        <th>Username</th>
        <th>Name</th>
        <th>Color</th>
        <th>Roles</th>
        <th>Actions</th>
    </tr>
    <% foreach (var u in users) { %>
    <tr>
        <td><%= u.Username %></td>
        <td><%= u.Name %></td>
        <td><small class="label" style="background:<%= u.Color %>"><%= u.Color %></small></td>
        <td>
            <% var links = u.IsOwner(company) ? Link.GetLinks() : ur.FindLinks(u.Id, company.Id); %>
            <% int i = 0; %>
            <% string s = ""; %>
            <% foreach (var l in links) { %>
                <% s += StrHelper.Str(i++ == 0, l.Name, ", " + l.Name); %>
            <% } %>
            <%= s %>
        </td>
        <td>
            <% if (!u.IsOwner(company)) { %>
                <%= HtmlHelper.Anchor(" ", "collaboratoredit.aspx?UserID=" + u.Id, "title='Edit' class='glyphicon glyphicon-edit'") %>
                <%= HtmlHelper.Anchor(" ", "collaboratordelete.aspx?UserID=" + u.Id, "title='Delete' class='glyphicon glyphicon-remove-circle' onclick=\"return confirm('Are you sure you want to delete this collaborator?')\"")%>
            <% } %>
        </td>
    </tr>
    <% } %>
</table>

</asp:Content>
