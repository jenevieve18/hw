<%@ Page Title="" Language="C#" MasterPageFile="~/Invoicing.Master" AutoEventWireup="true" CodeBehind="Issues.aspx.cs" Inherits="HW.Invoicing.Issues" %>
<%@ Import Namespace="HW.Core.Helpers" %>
<%@ Import Namespace="HW.Invoicing.Core.Models" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<h3>Issues</h3>
<p>
    <%= HtmlHelper.Anchor("Add an issue", "issueadd.aspx", "class='btn btn-info'") %>
    or <i><%= HtmlHelper.Anchor("check milestones", "milestones.aspx") %></i>
</p>
<div class="alert alert-info">
	<strong>Issue</strong> is an important topic or problem for debate or discussion.
</div>
<table class="table table-hover">
    <tr>
        <th>Number</th>
        <th>Title</th>
        <th>Description</th>
        <th>Milestone</th>
        <th>Priority</th>
        <th>Status</th>
        <th></th>
    </tr>
    <% foreach (var i in issues) { %>
        <% if (i.Status == Issue.DEACTIVATED) { %>
            <tr>
                <td><strike>#<%= i.Id %></strike></td>
                <td><strike><%= i.Title %></strike></td>
                <td><strike><%= i.Description %></strike></td>
                <td><strike><%= i.Milestone.Name %></strike></td>
                <td style="width:80px"><small><%= i.GetPriority() %></small></td>
                <td><%= i.GetStatus() %></td>
                <td style="width:50px">
                    <%= HtmlHelper.Anchor(" ", string.Format("issueedit.aspx?Id=" + i.Id), "title='Edit' class='glyphicon glyphicon-pencil'")%>
                    <%= HtmlHelper.Anchor(" ", string.Format("issuedelete.aspx?Id=" + i.Id), "title='Delete' class='glyphicon glyphicon-trash' onclick=\"return confirm('Are you sure you want to delete this issue?')\"")%>
                </td>
            </tr>
        <% } else { %>
            <tr>
                <td>#<%= i.Id %></td>
                <td><%= HtmlHelper.Anchor(i.Title, "issueshow.aspx?Id=" + i.Id) %></td>
                <td><%= i.Description %></td>
                <td><%= i.Milestone.Name %></td>
                <td style="width:80px"><small><%= i.GetPriority()  %></small></td>
                <td><%= i.GetStatus() %></td>
                <td style="width:50px">
                    <%= HtmlHelper.Anchor(" ", string.Format("issueedit.aspx?Id=" + i.Id), "title='Edit' class='glyphicon glyphicon-pencil'")%>
                    <%= HtmlHelper.Anchor(" ", string.Format("issuedeactivate.aspx?Id=" + i.Id), "title='Deactivate' class='glyphicon glyphicon-remove'")%>
                </td>
            </tr>
        <% } %>
    <% } %>
</table>

<nav>
  <% if (pager.EndPage > 1) { %>
  <ul class="pagination">
    <% if (pager.CurrentPage > 1) { %>
        <li><%= HtmlHelper.Anchor("&laquo;", "issues.aspx") %></li>
        <li><%= HtmlHelper.Anchor("&lt;", "issues.aspx?page=" + (pager.CurrentPage - 1).ToString()) %></li>
    <% } %>
    <% for (var page = pager.StartPage; page <= pager.EndPage; page++) { %>
        <li class="<%= page == pager.CurrentPage ? "active" : "" %>"><%= HtmlHelper.Anchor(page.ToString(), "issues.aspx?page=" + page.ToString()) %></li>
    <% } %>
    <% if (pager.CurrentPage < pager.TotalPages) { %>
        <li><%= HtmlHelper.Anchor("&gt;", "issues.aspx" + (pager.CurrentPage + 1).ToString())%></li>
        <li><%= HtmlHelper.Anchor("&raquo;", "issues.aspx?page=" + (pager.TotalPages).ToString())%></li>
    <% } %>
  </ul>
  <% } %>
</nav>

</asp:Content>
