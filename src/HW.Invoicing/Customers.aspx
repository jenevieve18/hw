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
        <th>Actions</th>
    </tr>
    <% foreach (var c in customers) { %>
    <tr>
        <td><%= c.Name %></td>
        <td>
            <%= HtmlHelper.Anchor("", "customeredit.aspx?CustomerID=" + c.Id, "class='glyphicon glyphicon-edit'") %>
            <%= HtmlHelper.Anchor("", "customerdelete.aspx?CustomerID=" + c.Id, "class='glyphicon glyphicon-remove'")%>
        </td>
    </tr>
    <% } %>
</table>

</asp:Content>
