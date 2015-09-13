﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Invoicing.Master" AutoEventWireup="true" CodeBehind="Invoices.aspx.cs" Inherits="HW.Invoicing.Invoices" %>
<%@ Import Namespace="HW.Core.Helpers" %>
<%@ Import Namespace="HW.Invoicing.Core.Models" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script type="text/javascript">
        var invoices = [];
        $(document).ready(function () {
            $('#button-download-pdf-selected').click(function () {
                invoices.forEach(function (e) {
                    window.open('invoiceexport.aspx?Id=' + e.id, '_blank');
                });
            });
            $('#checkbox-invoice-all').click(function () {
                invoices = [];
                if ($(this).is(':checked')) {
                    $('.checkbox-invoice').prop('checked', true);
                    $('.checkbox-invoice').change();
                } else {
                    $('.checkbox-invoice').prop('checked', false);
                }
            });
            $('.checkbox-invoice').change(function () {
                if ($(this).is(':checked')) {
                    var selected = $(this);
                    var id = selected.data('id');
                    var invoice = {
                        'id': id
                    };
                    invoices.push(invoice);
                } else {
                    var selected = $(this);
                    var id = selected.data('id');
                    findAndRemove(invoices, 'id', id);
                }
            });
            $('.internal-comments-text').hide();
            $('.spinner').hide();
            $('.internal-comments').click(function () {
                var text = $(this).find('.internal-comments-text');
                $(this).find('.internal-comments-label').hide();
                text.show();
                text.focus();
            });
            $('.internal-comments-text').focusout(function () {
                var comments = $(this).val();
                var id = $(this).data('id');
                var label = $(this).closest('td').find('.internal-comments-label');
                label.text(comments);

                var spinner = $(this).closest('td').find('.spinner');
                spinner.show();
                $.ajax({
                    type: 'POST',
                    url: 'Invoices.aspx/UpdateInternalComments',
                    data: JSON.stringify({ comments: comments, id: id }),
                    contentType: "application/json; charset=utf-8",
                    dataType: 'json',
                    success: function (msg) {
                        spinner.hide();
                    }
                });
                $(this).hide();
                label.show();
            });
        });
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<h3>Invoices</h3>

<%--<p><%= HtmlHelper.Anchor("Revert all invoices", "invoicerevertall.aspx", "class='btn btn-danger'") %></p>--%>

<p>
    <asp:DropDownList ID="dropDownListFinancialYear" CssClass="form-control" 
        runat="server" AutoPostBack="true"
        onselectedindexchanged="dropDownListFinancialYear_SelectedIndexChanged">
    </asp:DropDownList><br />
</p>
<div class="alert alert-info">
	<strong>Invoices</strong> are lists of goods sent or services provided, with a statement of the sum due for these; a bill.
</div>
<table class="table table-hover">
    <tr>
        <th>Number</th>
        <th>Date</th>
        <th>Customer</th>
        <th class="text-right">Amount</th>
        <th class="text-right">VAT</th>
        <th class="text-right">Total Amount</th>
        <th>Status</th>
        <th>Actions</th>
        <th style="width:16px">
            <input type="checkbox" id="checkbox-invoice-all" />
        </th>
        <th style="width:16px">
            <span id="button-download-pdf-selected" class="btn btn-xs btn-info">Export</span>
        </th>
        <th class="col-md-2">Comments</th>
    </tr>
    <% decimal totalSubTotal = 0, totalVAT = 0, totalAmount = 0; %>
    <% if (invoices != null) { %>
        <% foreach (var i in invoices) { %>
        <tr>
            <% totalSubTotal += i.SubTotal; %>
            <% totalVAT += i.TotalVAT; %>
            <% totalAmount += i.TotalAmount; %>

            <td><%= HtmlHelper.Anchor(i.Number, "invoiceshow.aspx?Id=" + i.Id) %></td>
            <td><%= i.Date.Value.ToString("yyyy-MM-dd") %></td>
            <td><%= HtmlHelper.Anchor(i.Customer.Name, string.Format("customershow.aspx?Id={0}&SelectedTab=timebook", i.Customer.Id)) %></td>
            <td class="text-right"><%= i.SubTotal.ToString("### ### ##0.00") %></td>
            <td class="text-right"><%= i.TotalVAT.ToString("### ### ##0.00") %></td>
            <td class="text-right"><%= i.TotalAmount.ToString("### ### ##0.00") %></td>
            <td><%= i.GetStatus() %></td>
            <td>
                <div class="btn-group">
				    <button onclick="javascript:;return false;" class="btn btn-default">
                        <i class="glyphicon glyphicon-cog"></i>&nbsp;
                    </button> 
				    <button data-toggle="dropdown" class="btn btn-default dropdown-toggle">
					    <span class="caret"></span>
				    </button>
				    <ul class="dropdown-menu">
					    <li><%= HtmlHelper.Anchor("Show", "invoiceshow.aspx?Id=" + i.Id) %></li>
                        <% if (i.Status == Invoice.PAID) { %>
                            <li><%= HtmlHelper.Anchor("Revert Payment", "invoicerevertpayment.aspx?Id=" + i.Id) %></li>
                        <% } else { %>
					        <li><%= HtmlHelper.Anchor("Edit", "invoiceedit.aspx?Id=" + i.Id) %></li>
                            <li><%= HtmlHelper.Anchor("Paid", "invoicereceivepayment.aspx?Id=" + i.Id) %></li>
                        <% } %>
                        <li><%= HtmlHelper.Anchor("PDF", "invoiceexport.aspx?Id=" + i.Id, "target='_blank'")%></li>
				    </ul>
			    </div>
            </td>
            <td style="width:16px">
                <input type="checkbox" class="checkbox-invoice"
                    data-id="<%= i.Id %>"
                 />
            </td>
            <td style="width:16px" class="text-center">
                <% if (i.Exported) { %>
                    <%= HtmlHelper.AnchorImage("invoiceexport.aspx?Id=" + i.Id, "img/page_white_acrobat.png", "target='_blank'")%>
                <% } %>
            </td>
            <td class="internal-comments">
                <span class="internal-comments-label"><%= i.InternalComments %></span>
                <textarea data-id="<%= i.Id %>" type="text" class="form-control internal-comments-text"><%= i.InternalComments %></textarea>
                <img alt="" class="spinner" src="img/spiffygif_30x30.gif" />
            </td>
        </tr>
        <% } %>
    <% } %>
    <tr>
        <td colspan="3"><strong>TOTAL</strong></td>
        <td class="text-right"><strong><%= totalSubTotal.ToString("### ### ##0.00") %></strong></td>
        <td class="text-right"><strong><%= totalVAT.ToString("### ### ##0.00") %></strong></td>
        <td class="text-right"><strong><%= totalAmount.ToString("### ### ##0.00") %></strong></td>
        <td colspan="5"></td>
    </tr>
</table>

</asp:Content>
