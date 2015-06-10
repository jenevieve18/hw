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
        <th>Title</th>
        <th></th>
    </tr>
    <% foreach (var i in issues) { %>
    <tr>
        <td><%= i.Title %></td>
        <td>
            <%= HtmlHelper.Anchor("Edit", "issueedit.aspx?Id=" + i.Id) %>
            <%= HtmlHelper.Anchor("Deactivate", "") %>
        </td>
    </tr>
    <% } %>
</table>

</asp:Content>
