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
        <!--<th>Date</th>-->
        <th>Number</th>
        <th>Customer</th>
        <th>Amount</th>
        <th>VAT</th>
        <th>Total Amount</th>
        <th>Status</th>
        <th>Actions</th>
        <th style="width:16px"></th>
        <th>Comments</th>
    </tr>
    <% decimal totalSubTotal = 0, totalVAT = 0, totalAmount = 0; %>
    <% foreach (var i in invoices) { %>
    <tr>
        <% totalSubTotal += i.SubTotal; %>
        <% totalVAT += i.TotalVAT; %>
        <% totalAmount += i.TotalAmount; %>

        <!--<td><%= i.Date.Value.ToString("yyyy-MM-dd") %></td>-->
        <td><%= i.Number %></td>
        <td><%= i.Customer.Name %></td>
        <td><%= i.SubTotal.ToString("### ### ##0.00") %></td>
        <td><%= i.TotalVAT.ToString("### ### ##0.00") %></td>
        <td><%= i.TotalAmount.ToString("### ### ##0.00") %></td>
        <td><%= i.GetStatus() %></td>
        <td>
            <div class="btn-group">
				<button class="btn btn-default">Action</button> 
				<button data-toggle="dropdown" class="btn btn-default dropdown-toggle">
					<span class="caret"></span>
				</button>
				<ul class="dropdown-menu">
					<li><%= HtmlHelper.Anchor("Show", "invoiceshow.aspx?Id=" + i.Id) %></li>
					<li><%= HtmlHelper.Anchor("Edit", "invoiceedit.aspx?Id=" + i.Id) %></li>
                    <% if (i.Status != 2) { %>
					    <li><%= HtmlHelper.Anchor("Paid", "invoicereceivepayment.aspx?Id=" + i.Id) %></li>
                    <% } else { %>
                        <li><%= HtmlHelper.Anchor("Revert Payment", "invoicerevertpayment.aspx?Id=" + i.Id) %></li>
                    <% } %>
				</ul>
			</div>
        </td>
        <td style="width:16px"><%= HtmlHelper.AnchorImage("invoiceexport.aspx?Id=" + i.Id, "img/page_white_acrobat.png")%></td>
        <td><%= i.InternalComments %></td>
    </tr>
    <% } %>
    <!--<tr><td colspan="8">&nbsp;</td></tr>-->
    <tr>
        <td colspan="2"><strong>TOTAL</strong></td>
        <td><strong><%= totalSubTotal.ToString("### ### ##0.00") %></strong></td>
        <td><strong><%= totalVAT.ToString("### ### ##0.00") %></strong></td>
        <td><strong><%= totalAmount.ToString("### ### ##0.00") %></strong></td>
        <td colspan="4"></td>
    </tr>
</table>

</asp:Content>
