<%@ Page Title="" Language="C#" MasterPageFile="~/Invoicing.Master" AutoEventWireup="true" CodeBehind="Users.aspx.cs" Inherits="HW.Invoicing.Users" %>
<%@ Import Namespace="HW.Core.Helpers" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<h3>Users</h3>
<p><%= HtmlHelper.Anchor("Add a user", "useradd.aspx", "class='btn btn-info'") %></p>
<div class="alert alert-info">
	<strong>User</strong> is a person who uses or operates something, especially a computer or other machine.
</div>
<table class="table table-hover">
    <tr>
        <th>Username</th>
        <th>Name</th>
        <th>Color</th>
        <th>Actions</th>
    </tr>
    <% foreach (var u in users) { %>
    <tr>
        <td><%= u.Username %></td>
        <td><%= u.Name %></td>
        <td><small class="label" style="background:<%= u.Color %>"><%= u.Color %></small></td>
        <td>
            <%--<%= HtmlHelper.Anchor("Edit", "useredit.aspx?UserID=" + u.Id) %>
            <%= HtmlHelper.Anchor("Delete", "userdelete.aspx?UserID=" + u.Id)%>--%>
            <%= HtmlHelper.Anchor(" ", "useredit.aspx?UserID=" + u.Id, "title='Edit' class='glyphicon glyphicon-edit'") %>
            <%= HtmlHelper.Anchor(" ", "userdelete.aspx?UserID=" + u.Id, "title='Delete' class='glyphicon glyphicon-remove-circle' onclick=\"return confirm('Are you sure you want to delete this customer note?')\"")%>
        </td>
    </tr>
    <% } %>
</table>
</asp:Content>
