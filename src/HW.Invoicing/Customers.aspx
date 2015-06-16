<%@ Page Title="" Language="C#" MasterPageFile="~/Invoicing.Master" AutoEventWireup="true" CodeBehind="Customers.aspx.cs" Inherits="HW.Invoicing.Customers" %>
<%@ Import Namespace="HW.Core.Helpers" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<h3>Customers</h3>
<p><%= HtmlHelper.Anchor("Add a customer", "customeradd.aspx", "class='btn btn-info'") %></p>
<div class="alert alert-info">
	<strong>Customers</strong> are people or organization that buys goods or services from a store or business.
</div>
<table class="table table-hover">
    <tr>
        <th>Name</th>
        <th>Contact Person</th>
        <th>Phone</th>
        <th>Email</th>
    </tr>
    <% foreach (var c in customers) { %>
        <% if (c.Inactive) { %>
            <tr>
                <td><strike><%= HtmlHelper.Anchor(c.Name, "customershow.aspx?Id=" + c.Id) %></strike></td>
                <td>
                    <% if (c.FirstPrimaryContact != null) { %>
                        <strike><%= c.FirstPrimaryContact.Contact %></strike>
                    <% } %>
                </td>
                <td><strike><%= c.Phone %></strike></td>
                <td><strike><%= c.Email %></strike></td>
            </tr>
        <% } else { %>
            <tr>
                <td><%= HtmlHelper.Anchor(c.Name, "customershow.aspx?Id=" + c.Id) %></td>
                <td>
                    <% if (c.FirstPrimaryContact != null) { %>
                        <%= c.FirstPrimaryContact.Contact %>
                    <% } %>
                </td>
                <td><%= c.Phone %></td>
                <td><%= c.Email %></td>
            </tr>
        <% } %>
    <% } %>
</table>

</asp:Content>
