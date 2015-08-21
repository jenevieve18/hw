<%@ Page Title="" Language="C#" MasterPageFile="~/Invoicing.Master" AutoEventWireup="true" CodeBehind="InvoiceEdit.aspx.cs" Inherits="HW.Invoicing.InvoiceEdit" %>
<%@ Import Namespace="HW.Core.Helpers" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="css/bootstrap-datepicker.min.css" rel="stylesheet" type="text/css" />
    <script src="js/bootstrap-datepicker.min.js" type="text/javascript"></script>
    <script src="js/jquery.number.min.js" type="text/javascript"></script>
    <script type="text/javascript">
        var invoiceItems = [];
        $(document).ready(function () {
            $('#<%= textBoxInvoiceDate.ClientID %>').datepicker({
                format: "yyyy-mm-dd",
                autoclose: true
            });
            $('#<%= textBoxInvoiceDate.ClientID %>').change(function () {
                var d = new Date($('#<%= textBoxInvoiceDate.ClientID %>').val());
                d.setDate(d.getDate() + 30);
                $('#<%= labelMaturityDate.ClientID %>').text(d.toISOString().slice(0, 10));
            });
            $('#checkbox-timebook-all').click(function () {
                if ($(this).is(':checked')) {
                    $('.timebook-item').prop('checked', true);
                } else {
                    $('.timebook-item').prop('checked', false);
                }
                $('.timebook-item').change();
            });
            $('.timebook-item').change(function () {
                var tr = $(this).closest('tr');
                var td = $(this).closest('td');
                var id = $(this).data('id');
                if ($(this).is(':checked')) {
                    tr.addClass('grayed');
                    td.append('<input type="hidden" id="invoice-timebooks" name="invoice-timebooks" value="' + id + '">');

                    var selected = $(this);
                    var invoiceItem = getInvoiceItem(selected);
                    invoiceItems.push(invoiceItem);
                } else {
                    tr.removeClass('grayed');
                    td.find('input[type="hidden"]').remove();

                    var selected = $(this);
                    var id = selected.data('id');
                    //findAndRemove(invoiceItems, 'id', id);
                    invoiceItems = $.grep(invoiceItems, function (data, index) {
                        return data.id != id;
                    });
                }

                var footer = $('tfoot');
                var subTotal = 0;
                var vats = Array();
                footer.html('');
                invoiceItems.forEach(function (e) {
                    var vatAmount = e.vat / 100.0 * e.amount;
                    subTotal += e.amount;
                    if (vats.hasOwnProperty(e.vat)) {
                        vats[e.vat] += vatAmount;
                    } else {
                        vats[e.vat] = vatAmount;
                    }
                });
                var strVat = '', strVatLabel = '';
                var totalVat = 0;
                for (var v in vats) {
                    strVat += '   <td style="width:10%" class="hw-border-left">' + v + '</td>';
                    strVat += '   <td style="width:10%" class="hw-border-left">' + $.number(vats[v], 2, ',', ' ') + '</td>';
                    strVatLabel += '   <td style="width:10%" class="hw-border-left">VAT %</td>';
                    strVatLabel += '   <td style="width:10%" class="hw-border-left">VAT</td>';
                    totalVat += vats[v];
                }
                totalAmount = subTotal + totalVat;
                footer.append('' +
                    '<tr><td>&nbsp;</td></tr>' +
                    '<tr class="hw-invoice-header"><td colspan="7"></td><td class="hw-border-last">Subtotal</td></tr>' +
                    '<tr><td colspan="7"></td><td class="hw-border-last">' + $.number(subTotal, 2, ',', ' ') + '</td></tr>' +
                    '<tr class="hw-invoice-header">' +
                    '   <td colspan="' + (7 - (Object.size(vats) * 2)) + '"></td>' +
                    strVatLabel +
                    '   <td class="hw-border-last">Total Amount</td>' +
                    '</tr>' +
                    '<tr class="hw-border-bottom">' +
                    '   <td colspan="' + (7 - (Object.size(vats) * 2)) + '"></td>' +
                    strVat +
                    '   <td class="hw-border-last">' + $.number(totalAmount, 2, ',', ' ') + '</td>' +
                    ''
                );
            });
        });

        Object.size = function (obj) {
            var size = 0, key;
            for (key in obj) {
                if (obj.hasOwnProperty(key)) size++;
            }
            return size;
        };

        function findAndRemove(array, property, value) {
            $.each(array, function (index, result) {
                if (result[property] == value) {
                    //Remove from array
                    array.splice(index, 1);
                }
            });
        }

        function getInvoiceItem(selected) {
            var id = selected.data('id');
            var item = selected.data('item');
            var unit = selected.data('unit');
            var qty = selected.data('qty');
            var price = selected.data('price');
            var amount = selected.data('amount');
            var consultant = selected.data('consultant');
            var comments = selected.data('comments');
            var vat = selected.data('vat');
            var invoiceItem = {
                'id': id,
                'item': item,
                'unit': unit,
                'qty': qty,
                'price': price,
                'amount': amount,
                'consultant': consultant,
                'comments': comments,
                'vat': vat
            };
            return invoiceItem;
        }
     </script>
     <style type="text/css">
         .grayed 
         {
             background:#efefef;
         }
     </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <h3>Edit an invoice</h3>
    <% if (message != null && message != "") { %>
    <div class="alert alert-warning">
        <%= message %>
    </div>
    <% } %>
    
    <table width="100%" cellpadding="5px">
        <tr>
            <td rowspan="4" valign="bottom">
                <% if (company.HasInvoiceLogo) { %>
                    <img src="uploads/<%= company.InvoiceLogo %>" /><br />
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
                <asp:TextBox ID="textBoxInvoiceDate" runat="server" CssClass="form-control"></asp:TextBox>
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
                    Your Reference:
                        <strong>
                            <asp:Label ID="labelInvoiceYourReferencePerson" runat="server" Text=""></asp:Label>
                        </strong><br />
                    Our Reference:
                        <strong>
                            <asp:Label ID="labelInvoiceOurReferencePerson" runat="server" Text=""></asp:Label>
                        </strong>
                </small>
            </td>
        </tr>
        <tr>
            <td>
                <strong>
                    Purchase order number: 
                    <asp:Label ID="labelInvoicePurchaseOrderNumber" runat="server" Text="Label"></asp:Label>
                </strong>
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
                <td class="hw-border-left" style="width:16px">
                    <input type="checkbox" id="checkbox-timebook-all" />
                </td>
                <td class="hw-border-left" colspan="3"><%= HtmlHelper.Anchor("Item", string.Format("customershow.aspx?Id={0}&SelectedTab=timebook", invoice.Customer.Id)) %></td>
                <td class="hw-border-left" style="width:10%">Qty</td>
                <td class="hw-border-left" style="width:10%">Unit</td>
                <td class="hw-border-left" style="width:10%">Price/Unit</td>
                <td class="hw-border-last" style="width:10%">Amount</td>
            </tr>
        </thead>
        <tbody>
            
            <% decimal subTotal = 0; %>
            <% Dictionary<decimal, decimal> vats = new Dictionary<decimal, decimal>(); %>

            <% foreach (var t in invoice.Timebooks) { %>
                <tr class="grayed">
                    <td style="width:16px">
                        <input type="checkbox" class="timebook-item" checked
                            data-id="<%= t.Timebook.Id %>"
                            data-item="<%= t.Timebook.Item.Name %>"
                            data-unit="<%= t.Timebook.Item.Unit.Name %>"
                            data-qty="<%= t.Timebook.Quantity %>"
                            data-price="<%= t.Timebook.Price %>"
                            data-amount="<%= t.Timebook.Amount %>"
                            data-consultant="<%= t.Timebook.Consultant %>"
                            data-comments="<%= t.Timebook.Comments %>"
                            data-vat="<%= t.Timebook.VAT %>"
                        />
                        <script type="text/javascript">
                            invoiceItems.push({
                                'id': <%= t.Timebook.Id %>,
                                'item': "<%= t.Timebook.Item.Name %>",
                                'unit': "<%= t.Timebook.Item.Unit.Name %>",
                                'qty': <%= t.Timebook.Quantity %>,
                                'price': <%= t.Timebook.Price %>,
                                'amount': <%= t.Timebook.Amount %>,
                                'consultant': "<%= t.Timebook.Consultant %>",
                                'comments': "<%= t.Timebook.Comments %>",
                                'vat': <%= t.Timebook.VAT %>
                            });
                        </script>
                        <input type="hidden" id="invoice-timebooks" name="invoice-timebooks" value="<%= t.Timebook.Id %>" />
                    </td>
                    <td colspan="3"><%= t.Timebook.ToString() %></td>
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

            <% foreach (var t in timebooks) { %>
                <tr>
                    <td style="width:16px">
                        <input type="checkbox" class="timebook-item"
                            data-id="<%= t.Id %>"
                            data-item="<%= t.Item.Name %>"
                            data-unit="<%= t.Item.Unit.Name %>"
                            data-qty="<%= t.Quantity %>"
                            data-price="<%= t.Price %>"
                            data-amount="<%= t.Amount %>"
                            data-consultant="<%= t.Consultant %>"
                            data-comments="<%= t.Comments %>"
                            data-vat="<%= t.VAT %>"
                        />
                    </td>
                    <td colspan="3"><%= t.ToString() %></td>
                    <td><%= t.Quantity %></td>
                    <td><%= t.Item.Unit.Name %></td>
                    <td class="text-right"><%= t.Price.ToString("### ### ##0.00") %></td>
                    <td class="text-right"><%= t.Amount.ToString("### ### ##0.00") %></td>
                </tr>
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

        <tfoot>
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
    <asp:Button ID="buttonSave" OnClick="buttonSave_Click" runat="server" Text="Save this invoice" CssClass="btn btn-success" />
    or <i><%= HtmlHelper.Anchor("cancel", "invoices.aspx") %></i>

</asp:Content>
