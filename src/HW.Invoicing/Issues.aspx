<%@ Page Title="" Language="C#" MasterPageFile="~/Invoicing.Master" AutoEventWireup="true" CodeBehind="Issues.aspx.cs" Inherits="HW.Invoicing.Issues" %>
<%@ Import Namespace="HW.Core.Helpers" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<h3>Issues</h3>
<p><%= HtmlHelper.Anchor("Add an issue", "issueadd.aspx", "class='btn btn-info'") %></p>
<div class="alert alert-info">
	<strong>Issue</strong> is an important topic or problem for debate or discussion.
</div>
<table class="table table-hover">
    <tr>
        <th style="width: 10%;">Number</th>
        <th>Title</th>
        <th>Status</th>
        <th>Description</th>
        <th></th>
    </tr>
    <% foreach (var i in issues) { %>
        <% if (i.Status == 3) { %>
            <tr>
                <td><strike>#<%= i.Id %></strike></td>
                <td><strike><%= i.Title %></strike></td>
                <td><strike><%= i.Description %></strike></td>
                <td><%= i.GetStatus() %></td>
                <td>
                    <%= HtmlHelper.Anchor("Edit", "issueedit.aspx?Id=" + i.Id) %>
                    <%= HtmlHelper.Anchor("Delete", "issuedelete.aspx?Id=" + i.Id) %>
                </td>
            </tr>
        <% } else { %>
            <tr>
                <td>#<%= i.Id %></td>
                <td><%= i.Title %></td>
                <td><%= i.Description %></td>
                <td><%= i.GetStatus() %></td>
                <td>
                    <%= HtmlHelper.Anchor("Edit", "issueedit.aspx?Id=" + i.Id) %>
                    <%= HtmlHelper.Anchor("Deactivate", "issuedeactivate.aspx?Id=" + i.Id) %>
                </td>
            </tr>
        <% } %>
    <% } %>
</table>

</asp:Content>
