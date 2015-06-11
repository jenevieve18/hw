<%@ Page Title="" Language="C#" MasterPageFile="~/Invoicing.Master" AutoEventWireup="true" CodeBehind="Items.aspx.cs" Inherits="HW.Invoicing.Items" %>
<%@ Import Namespace="HW.Core.Helpers" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<h3>Items</h3>
<p><%= HtmlHelper.Anchor("Add an item", "itemadd.aspx", "class='btn btn-info'") %></p>
<div class="alert alert-info">
	<strong>Item</strong> is an individual article or unit, especially one that is part of a list, collection, or set.
</div>
<table class="table table-hover">
    <tr>
        <th>Name</th>
        <th>Description</th>
        <th>Price</th>
        <th>Unit</th>
        <th>Actions</th>
    </tr>
    <% foreach (var i in items) { %>
    <% if (i.Inactive) { %>
    <tr class="strikeout">
    <% } else { %>
    <tr>
    <% } %>
        <td><%= i.Name %></td>
        <td><%= i.Description %></td>
        <td><%= i.Price.ToString("# ##0.00") %></td>
        <td><%= i.Unit.Name %></td>
        <td>
            <%= HtmlHelper.Anchor("Edit", "itemedit.aspx?Id=" + i.Id)%>
            <% if (i.Inactive) { %>
                <%= HtmlHelper.Anchor("Delete", "itemdelete.aspx?Id=" + i.Id, "onclick=\"return confirm('Are you sure you want to delete this item?')\"")%>
            <% } else { %>
                <%= HtmlHelper.Anchor("Deactivate", "itemdeactivate.aspx?Id=" + i.Id)%>
            <% } %>
        </td>
    </tr>
    <% } %>
</table>

</asp:Content>
