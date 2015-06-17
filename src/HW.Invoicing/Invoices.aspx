<%@ Page Title="" Language="C#" MasterPageFile="~/Invoicing.Master" AutoEventWireup="true" CodeBehind="Invoices.aspx.cs" Inherits="HW.Invoicing.Invoices" %>
<%@ Import Namespace="HW.Core.Helpers" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<h3>Invoices</h3>
<div class="alert alert-info">
	<strong>Invoices</strong> are lists of goods sent or services provided, with a statement of the sum due for these; a bill.
</div>
<table class="table table-hover">
    <tr>
        <th>Date</th>
        <th>Number</th>
        <th>Customer</th>
        <th>Amount</th>
        <th>Status</th>
        <th></th>
    </tr>
    <% foreach (var i in invoices) { %>
    <tr>
        <td><%= i.Date.Value.ToString("yyyy-MM-dd") %></td>
        <td><%= i.Customer.Number %></td>
        <td><%= i.Customer.Name %></td>
        <td><%= i.AmountDue.ToString("0.00") %></td>
        <td><%= i.TotalAmount.ToString("0.00") %></td>
        <td><span class="label label-danger">NOT PAID</span></td>
        <td>
            <%= HtmlHelper.Anchor("Edit", "invoiceedit.aspx?Id=" + i.Id)%>
        </td>
    </tr>
    <% } %>
</table>

</asp:Content>
