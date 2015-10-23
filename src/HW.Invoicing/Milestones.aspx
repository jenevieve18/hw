<%@ Page Title="" Language="C#" MasterPageFile="~/Invoicing.Master" AutoEventWireup="true" CodeBehind="Milestones.aspx.cs" Inherits="HW.Invoicing.Milestones" %>
<%@ Import Namespace="HW.Core.Helpers" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">



<h3>Milestones</h3>
<p><%= HtmlHelper.Anchor("Add a milestone", "milestoneadd.aspx", "class='btn btn-info'") %></p>
<div class="alert alert-info">
    <strong>Milestone</strong> is an action or event marking a significant change or stage in development.
</div>
<table class="table table-hover">
    <tr>
        <th>Milestone</th>
        <th>Actions</th>
    </tr>
    <% foreach (var m in milestones) { %>
    <tr>
        <td>
            <%= m.Name %>
        </td>
        <td>
            <%= HtmlHelper.Anchor("Edit", "milestoneedit.aspx?Id=" + m.Id) %>
        </td>
    </tr>
    <% } %>
</table>


</asp:Content>
