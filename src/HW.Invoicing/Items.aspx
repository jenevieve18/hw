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
        <th>Actions</th>
    </tr>
    <% foreach (var i in items) { %>
    <tr>
        <td><%= i.Name %></td>
        <td><%= i.Description %></td>
        <td><%= i.Price.ToString("# ##0.00") %></td>
        <td>
            <%= HtmlHelper.Anchor("Edit", "itemedit.aspx?ItemID=" + i.Id)%>
            <%= HtmlHelper.Anchor("Delete", "itemdelete.aspx?ItemID=" + i.Id)%>
        </td>
    </tr>
    <% } %>
</table>

</asp:Content>
