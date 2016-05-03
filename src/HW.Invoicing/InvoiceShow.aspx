<%@ Page Title="" Language="C#" MasterPageFile="~/Invoicing.Master" AutoEventWireup="true" CodeBehind="InvoiceShow.aspx.cs" Inherits="HW.Invoicing.InvoiceShow" %>
<%@ Import Namespace="HW.Core.Helpers" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<table width="100%" cellpadding="5px">
    <tr>
        <td rowspan="4" valign="bottom">
            <% if (company.HasInvoiceLogo) { %>
                <img src="uploads/<%= company.InvoiceLogo %>" style="width:<%= company.InvoiceLogoPercentage %>%" /><br />
            <% } else { %>
                <img src="img/ihg.png"><br />
            <% } %>
        </td>
        <td></td>
        <td></td>
        <td><h3>INVOICE</h3></td>
    </tr>
    <tr>
        <td></td>
        <td class="hw-border-top">Customer Number</td>
        <td class="hw-border-top">
            <strong>
                <%= HtmlHelper.Anchor(invoice.Customer.Number, string.Format("customershow.aspx?Id={0}&SelectedTab=customer-info", invoice.Customer.Id)) %>
            </strong>
        </td>
    </tr>
    <tr>
        <td></td>
        <td class="hw-border-top">Invoice Number</td>
        <td class="hw-border-top">
            <strong>
                <asp:Label ID="labelInvoiceNumber" runat="server" Text=""></asp:Label>
            </strong>
        </td>
    </tr>
    <tr>
        <td></td>
        <td class="hw-border-top">Invoice Date</td>
        <td class="hw-border-top">
            <strong>
                <asp:Label ID="labelInvoiceDate" runat="server" Text=""></asp:Label>
            </strong>
        </td>
    </tr>
    <tr>
        <td>
            <small><strong>Customer/Address/Invoice Address</strong></small>
        </td>
        <td></td>
        <td class="hw-border-top">Maturity Date</td>
        <td class="hw-border-top">
            <strong>
                <asp:Label ID="labelMaturityDate" runat="server" Text=""></asp:Label>
            </strong>
        </td>
    </tr>
    <tr>
        <td>
            <strong>
                <asp:Label ID="labelInvoiceCustomerAddress" runat="server" Text=""></asp:Label>
            </strong>
        </td>
        <td></td>
        <td colspan="2">
            <small>
                Your Reference: <strong>
                    <asp:Label ID="labelInvoiceYourReferencePerson" runat="server" Text=""></asp:Label><!--First Name + Surname--></strong><br />
                Our Reference: <strong>
                    <asp:Label ID="labelInvoiceOurReferencePerson" runat="server" Text=""></asp:Label><!--Dan Hasson--></strong>
            </small>
        </td>
    </tr>
    <tr>
        <td>
            <asp:Panel ID="panelPurchaseOrder" runat="server">
                <strong>
                    <asp:Label ID="labelInvoicePurchaseOrderNumber" runat="server" Text=""></asp:Label>
                </strong>
            </asp:Panel>
        </td>
        <td></td>
        <td></td>
        <td></td>
    </tr>
</table>
<br />
<p>Payment terms: 30 days net. At the settlement after the due date will be charged interest of 2% per month.</p>
<table class="hw-invoice-items" cellpadding="5px">
    <thead>
    <tr class="hw-invoice-header">
        <td class="hw-border-left" colspan="4"><%= HtmlHelper.Anchor("Item", string.Format("customershow.aspx?Id={0}&SelectedTab=timebook", invoice.Customer.Id)) %></td>
        <td class="hw-border-left" style="width:10%">Qty</td>
        <td class="hw-border-left" style="width:10%">Unit</td>
        <td class="hw-border-left" style="width:10%">Price/Unit</td>
        <td class="hw-border-last" style="width:10%">Amount</td>
    </tr>
    </thead>
    <tbody>
    <% decimal subTotal = 0; %>
    <% Dictionary<decimal, decimal> vats = new Dictionary<decimal, decimal>(); %>
    <% int j = 0; %>
    <% foreach (var t in invoice.Timebooks) { %>
        <% if (t.Timebook.IsHeader) { %>
            <% if (j > 0) { %>
                <tr><td colspan="8"></td></tr>
                <tr><td colspan="8"></td></tr>
            <% } %>

            <tr>
                <td colspan="4"><%= t.Timebook.ToString() %></td>
                <td></td>
                <td></td>
                <td></td>
                <td></td>
                <%--<% subTotal += t.Timebook.Amount; %>
                <% if (vats.ContainsKey(t.Timebook.VAT)) { %>
                    <% vats[t.Timebook.VAT] += t.Timebook.VATAmount; %>
                <% } else { %>
                    <% vats[t.Timebook.VAT] = t.Timebook.VATAmount; %>
                <% } %>--%>
            </tr>
        <% } else { %>
            <tr>
                <td colspan="4"><%= t.Timebook.ToString() %></td>
                <td><%= t.Timebook.Quantity.ToString("# ##0.00") %></td>
                <td><%= t.Timebook.Item.Unit.Name %></td>
                <td class="text-right"><%= t.Timebook.Price.ToString("### ### ##0.00") %></td>
                <td class="text-right"><%= t.Timebook.Amount.ToString("### ### ##0.00") %></td>
                <% subTotal += t.Timebook.Amount; %>
                <% if (vats.ContainsKey(t.Timebook.VAT)) { %>
                    <% vats[t.Timebook.VAT] += t.Timebook.VATAmount; %>
                <% } else { %>
                    <% vats[t.Timebook.VAT] = t.Timebook.VATAmount; %>
                <% } %>
            </tr>
        <% } %>
        <% j++; %>
    <% } %>
    </tbody>

    <% var strVat = ""; var strVatLabel = ""; %>
    <% decimal totalVat = 0; %>
    <% var keys = vats.Keys.ToList(); %>
    <% keys.Sort(); %>
    <% foreach (var k in keys) { %>
        <% strVatLabel += "<td style='width:10%' class='hw-border-left'>VAT %</td>"; %>
        <% strVatLabel += "<td style='width:10%' class='hw-border-left'>VAT</td>"; %>

        <% strVat += "<td style='width:10%' class='hw-border-left'>" + k + "</td>"; %>
        <% strVat += "<td style='width:10%' class='hw-border-left'>" + vats[k].ToString("### ### ##0.00") + "</td>"; %>
        <% totalVat += vats[k]; %>
    <% } %>
    <tr><td>&nbsp;</td></tr>
    <tr class="hw-invoice-header"><td colspan="7"></td><td class="hw-border-last">Subtotal</td></tr>
    <tr><td colspan="7"></td><td class="hw-border-last"><%= subTotal.ToString("### ### ##0.00") %></td></tr>
    <tr class="hw-invoice-header">
        <td colspan="<%= (7 - vats.Count * 2) %>"></td>
        <%= strVatLabel %>
        <td class="hw-border-last">Total Amount</td>
    </tr>
    <tr class="hw-border-bottom">
        <td colspan="<%= (7 - vats.Count * 2) %>"></td>
        <%= strVat %>
        <td class="hw-border-last"><%= (subTotal + totalVat).ToString("### ### ##0.00") %></td>
    </tr>
</table>
<% if (invoice.Comments != "") { %>
<div class="form-group">
    <label for="labelInvoiceComments">Comments</label><br />
    <span id="labelInvoiceComments"><%= invoice.Comments %></span>
</div>
<% } %>
<small class="hw-footer">
    <table style="width:100%">
        <tr>
            <td colspan="4">
                <asp:Label ID="labelCompanyName" runat="server" Text=""></asp:Label>
            </td>
            <td><span>Bankgiro</span></td>
            <td>
                <asp:Label ID="labelCompanyBankAccountNumber" runat="server" Text=""></asp:Label>
            </td>
            <td></td>
        </tr>
        <tr>
            <td colspan="7">&nbsp;</td>
        </tr>
        <tr>
            <td><span>Phone</span></td>
            <td colspan="3">
                <asp:Label ID="labelCompanyPhone" runat="server" Text=""></asp:Label>
            </td>
            <td><span>VAT/Momsreg.nr</span></td>
            <td>
                <asp:Label ID="labelCompanyTIN" runat="server" Text=""></asp:Label>
            </td>
            <td></td>
        </tr>
        <tr>
            <td><span>Address</span></td>
            <td colspan="3">
                <asp:Label ID="labelCompanyAddress" runat="server" Text=""></asp:Label>
            </td>
            <td><span>F-skattebevis</span></td>
            <td></td>
            <td></td>
        </tr>
    </table>
</small>

<br />

<%= HtmlHelper.Anchor("Export this invoice to PDF", string.Format("invoiceexport.aspx?Id={0}", invoice.Id), "class='btn btn-success'") %>

<% if (!invoice.IsPaid) { %>
    <%= HtmlHelper.Anchor("Edit this invoice", string.Format("invoiceedit.aspx?Id={0}", invoice.Id), "class='btn btn-default'")%>
<% } %>

or <i><%= HtmlHelper.Anchor("go back to invoice list", "invoices.aspx") %></i>

</asp:Content>
