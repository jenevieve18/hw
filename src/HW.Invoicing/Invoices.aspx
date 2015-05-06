<%@ Page Title="" Language="C#" MasterPageFile="~/Invoicing.Master" AutoEventWireup="true" CodeBehind="Invoices.aspx.cs" Inherits="HW.Invoicing.Invoices" %>
<%@ Import Namespace="HW.Core.Helpers" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<h3>Invoices</h3>
<p><%= HtmlHelper.Anchor("Add an invoice", "invoiceadd.aspx", "class='btn btn-info'") %></p>
<div class="alert alert-info">
	<strong>Invoices</strong> are lists of goods sent or services provided, with a statement of the sum due for these; a bill.
</div>
<table class="table table-hover">
    <tr>
        <th>Date</th>
        <th>Actions</th>
    </tr>
    <% foreach (var i in invoices) { %>
    <tr>
        <td><%= i.Date %></td>
        <td>
            <%= HtmlHelper.Anchor("Edit", "invoiceedit.aspx?ItemID=" + i.Id, "class='icon-edit'") %>
        </td>
    </tr>
    <% } %>
</table>

</asp:Content>
