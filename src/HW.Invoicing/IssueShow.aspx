<%@ Page Title="" Language="C#" MasterPageFile="~/Invoicing.Master" AutoEventWireup="true" CodeBehind="IssueShow.aspx.cs" Inherits="HW.Invoicing.IssueShow" %>
<%@ Import Namespace="HW.Core.Helpers" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<h3>#<%= issue.Id %> <%= issue.Title %></h3>
<p><%= issue.Description %></p>


<h4>Add an issue comment</h4>
<div class="form-group">
	<label for="<%= textBoxComments.ClientID %>">Comments</label>
    <asp:TextBox ID="textBoxComments" runat="server" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
</div>
<div>
    <asp:Button ID="buttonSave" CssClass="btn btn-success" runat="server" Text="Save comments" 
        onclick="buttonSave_Click" />
        or <i><%= HtmlHelper.Anchor("cancel", "issues.aspx") %></i>
</div>

<h4>Comment activity history</h4>
<table class="table table-hover">
    <tr>
        <th>Date</th>
        <th>Comments</th>
        <th>Created by</th>
    </tr>
    <% foreach (var c in issue.Comments) { %>
    <tr>
        <td><%= c.Date.Value.ToString("yyyy-MM-dd") %></td>
        <td><%= c.Comments %></td>
        <td><%= c.User.Name %></td>
    </tr>
    <% } %>
</table>

</asp:Content>
