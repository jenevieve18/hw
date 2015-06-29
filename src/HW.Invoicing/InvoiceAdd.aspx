<%@ Page Title="" Language="C#" MasterPageFile="~/Invoicing.Master" AutoEventWireup="true" CodeBehind="InvoiceAdd.aspx.cs" Inherits="HW.Invoicing.InvoiceAdd" %>
<%@ Import Namespace="HW.Core.Helpers" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <h3>Add an invoice</h3>

    <table width="100%" cellpadding="5px">
        <tr>
            <td rowspan="4" valign="bottom">
                <img src="http://s16.postimg.org/vkxh59d5h/ihg.png"><br />
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
                    <asp:Label ID="labelInvoiceCustomerNumber" runat="server" Text=""></asp:Label>
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
                    <%= DateTime.Now.ToString("yyyy-MM-dd") %>
                </strong>
            </td>
        </tr>
        <tr>
            <td>
                <small><strong>Customer/Postal Address/Invoice Address</strong></small>
                <asp:DropDownList ID="dropDownListCustomer" runat="server" CssClass="form-control">
                </asp:DropDownList>
            </td>
            <td></td>
            <td style="vertical-align: top" class="hw-border-top">Maturity Date</td>
            <td style="vertical-align: top" class="hw-border-top">
                <strong>2012-06-30</strong>
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
                <strong>Purchase order number: 
                    <asp:Label ID="labelInvoicePurchaseOrderNumber" runat="server" Text="Label"></asp:Label><!--XX6279292--></strong>
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
            <td class="hw-border-left" colspan="4">Item</td>
            <td class="hw-border-left" style="width:10%">Qty</td>
            <td class="hw-border-left" style="width:10%">Unit</td>
            <td class="hw-border-left" style="width:10%">Price/Unit</td>
            <td class="hw-border-last" style="width:10%">Amount</td>
        </tr>
        </thead>
        <tbody>
        <tr>
            <td colspan="4">
                <asp:DropDownList ID="dropDownListItem" runat="server" CssClass="form-control">
                </asp:DropDownList>
            </td>
            <td>
                <asp:TextBox ID="TextBox1" runat="server" CssClass="form-control"></asp:TextBox>
            </td>
            <td></td>
            <td>
                <asp:TextBox ID="TextBox2" runat="server" CssClass="form-control"></asp:TextBox>
            </td>
            <td></td>
        </tr>

        <tr><td>&nbsp;</td></tr>
        <tr class="hw-invoice-header"><td colspan="7"></td><td class="hw-border-last">Subtotal</td></tr>
        <tr><td colspan="7"></td><td class="hw-border-last">50 000,00</td></tr>
        <tr class="hw-invoice-header">
            <td colspan="5"></td>
            <td style="width:10%" class="hw-border-left">VAT %</td>
            <td style="width:10%" class="hw-border-left">VAT</td>
            <td class="hw-border-last">Total Amount</td>
        </tr>
        <tr class="hw-border-bottom">
            <td colspan="5"></td>
            <td style="width:10%" class="hw-border-left">25%</td>
            <td style="width:10%" class="hw-border-left">12 500,00</td>
            <td class="hw-border-last">62 500,00</td>
        </tr>
        </tbody>
    </table>

    <div class="form-group">
        <label for="<%= textBoxComments.ClientID %>">Comments</label>
        <asp:TextBox ID="textBoxComments" runat="server" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
    </div>

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
                <td><span>Postal Address</span></td>
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
    <asp:Button ID="buttonSave" runat="server" Text="Save invoice" CssClass="btn btn-success" />
    or <i><%= HtmlHelper.Anchor("cancel", "invoices.aspx") %></i>

</asp:Content>
