<%@ Page Title="" Language="C#" MasterPageFile="~/Invoicing.Master" AutoEventWireup="true" CodeBehind="InvoiceEdit.aspx.cs" Inherits="HW.Invoicing.InvoiceEdit" %>
<%@ Import Namespace="HW.Core.Helpers" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="css/bootstrap-datepicker.min.css" rel="stylesheet" type="text/css" />
    <script src="js/bootstrap-datepicker.min.js" type="text/javascript"></script>
    <script type="text/javascript">
         $(document).ready(function () {
            $('#<%= textBoxInvoiceDate.ClientID %>').datepicker({
                format: "yyyy-mm-dd",
                autoclose: true
            });
            $('#<%= textBoxInvoiceDate.ClientID %>').change(function() {
                var d = new Date($('#<%= textBoxInvoiceDate.ClientID %>').val());
                d.setDate(d.getDate() + 30);
                $('#<%= labelMaturityDate.ClientID %>').text(d.toISOString().slice(0, 10));
            });
         });
     </script>


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <h3>Edit an invoice</h3>
    
    <table width="100%" cellpadding="5px">
        <tr>
            <td rowspan="4" valign="bottom">
                <img src="img/ihg.png"><br />
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
                <asp:TextBox ID="textBoxInvoiceDate" runat="server" CssClass="form-control"></asp:TextBox>
                <!--<strong>
                    <%= DateTime.Now.ToString("yyyy-MM-dd") %>
                </strong>-->
            </td>
        </tr>
        <tr>
            <td>
                <small><strong>Customer/Postal Address/Invoice Address</strong></small>
                
            </td>
            <td></td>
            <td style="vertical-align: top" class="hw-border-top">Maturity Date</td>
            <td style="vertical-align: top" class="hw-border-top">
                <strong>
                    <asp:Label ID="labelMaturityDate" runat="server" Text=""></asp:Label></strong>
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
            <!--<td colspan="4">
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
            <td></td>-->
            
            <% decimal subTotal = 0; %>
            <% Dictionary<decimal, decimal> vats = new Dictionary<decimal, decimal>(); %>
            <% foreach (var t in invoice.Timebooks) { %>
            <tr>
                <td colspan="4"><%= t.Timebook.ToString() %></td>
                <td><%= t.Timebook.Quantity %></td>
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
        </tr>
        </tbody>

        <!--<tr><td>&nbsp;</td></tr>
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
        </tr>-->
        

        <% var strVat = ""; var strVatLabel = ""; %>
        <% decimal totalVat = 0; %>
        <% foreach (var k in vats.Keys) { %>
            <% strVatLabel += "<td style='width:10%' class='hw-border-left'>VAT %</td>"; %>
            <% strVatLabel += "<td style='width:10%' class='hw-border-left'>VAT</td>"; %>

            <% strVat += "<td style='width:10%' class='hw-border-left'>" + k + "%</td>"; %>
            <% strVat += "<td style='width:10%' class='hw-border-left'>" + vats[k].ToString("### ### ##0.00") + "</td>"; %>
            <% totalVat += vats[k]; %>
        <% } %>
        <tfoot>
        <tr><td>&nbsp;</td></tr>
        <tr class="hw-invoice-header"><td colspan="7"></td><td class="hw-border-last">Subtotal</td></tr>
        <tr><td colspan="7"></td><td class="hw-border-last"><%= subTotal.ToString("### ### ##0.00") %></td></tr>
        <tr class="hw-invoice-header">
            <td colspan="<%= (7 - vats.Count * 2) %>"></td>
            <!--<td style="width:10%" class="hw-border-left">VAT %</td>
            <td style="width:10%" class="hw-border-left">VAT</td>-->
            <%= strVatLabel %>
            <td class="hw-border-last">Total Amount</td>
        </tr>
        <tr class="hw-border-bottom">
            <td colspan="<%= (7 - vats.Count * 2) %>"></td>
            <%= strVat %>
            <!--<td style="width:10%" class="hw-border-left">25%</td>
            <td style="width:10%" class="hw-border-left">12 500,00</td>-->
            <td class="hw-border-last"><%= (subTotal + totalVat).ToString("### ### ##0.00") %></td>
        </tr>
        </tfoot>
    </table>

    <div class="form-group">
        <label for="<%= textBoxInvoiceComments.ClientID %>">Comments</label>
        <asp:TextBox ID="textBoxInvoiceComments" runat="server" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
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
    <asp:Button ID="buttonSave" OnClick="buttonSave_Click" runat="server" Text="Save invoice" CssClass="btn btn-success" />
    or <i><%= HtmlHelper.Anchor("cancel", "invoices.aspx") %></i>

</asp:Content>
