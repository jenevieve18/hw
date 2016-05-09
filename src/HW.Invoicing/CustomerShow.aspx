<%@ Page Title="" Language="C#" MasterPageFile="~/Invoicing.Master" AutoEventWireup="true" CodeBehind="CustomerShow.aspx.cs" Inherits="HW.Invoicing.CustomerShow" %>
<%@ Import Namespace="HW.Core.Helpers" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="css/bootstrap-datepicker.min.css" rel="stylesheet" type="text/css" />
    <script src="js/bootstrap-datepicker.min.js" type="text/javascript"></script>
    <script src="js/jquery.number.min.js" type="text/javascript"></script>
    <script type="text/javascript">
        var invoiceItems = [];
        $(document).ready(function () {
            $('#<%= dropDownListTimebookItems.ClientID %>').change(function () {
                var selected = $(this).find('option:selected');
                var selectedPrice = selected.data('price');
                var selectedUnit = selected.data('unit');
                var selectedConsultant = selected.data('consultant');
                //alert(selectedConsultant);
                $('#<%= textBoxTimebookPrice.ClientID %>').val(selectedPrice);
                $('#<%= labelTimebookUnit.ClientID %>').text(selectedUnit);
                $('#<%= textBoxTimebookConsultant.ClientID %>').val(selectedConsultant);
            });
            $('#<%= dropDownListTimebookItems.ClientID %>').change();
            $('.date').datepicker({
                format: "yyyy-mm-dd",
                autoclose: true
            });
            $('#<%= textBoxInvoiceDate.ClientID %>').change(function() {
                var d = new Date($('#<%= textBoxInvoiceDate.ClientID %>').val());
                d.setDate(d.getDate() + 30);
                $('#<%= labelMaturityDate.ClientID %>').text(d.toISOString().slice(0, 10));
            });
            $('.timebook-item').change(function() {
                if ($(this).is(':checked')) {
                    var selected = $(this);
                    var id = selected.data('id');
                    var item = selected.data('item');
                    var unit = selected.data('unit');
                    var qty = selected.data('qty');
                    var price = selected.data('price');
                    var amount = selected.data('amount');
                    var consultant = selected.data('consultant');
                    var comments = selected.data('comments');
                    var vat = selected.data('vat');
                    var date = selected.data('date');
                    var datehidden = selected.data('datehidden');
                    var header = selected.data('header');
                    var invoiceItem = {
                        'id': id,
                        'item': item,
                        'unit': unit,
                        'qty': qty,
                        'price': price,
                        'amount': amount,
                        'consultant': consultant,
                        'comments': comments,
                        'vat': vat,
                        'date': date,
                        'datehidden': datehidden,
                        'header': header
                    };
                    invoiceItems.push(invoiceItem);
                } else {
                    var selected = $(this);
                    var id = selected.data('id');
                    findAndRemove(invoiceItems, 'id', id);
                }
            });
            $('#checkbox-timebook-all').click(function() {
                invoiceItems = [];
                if ($(this).is(':checked')) {
                    $('.timebook-item').prop('checked', true);
                    $('.timebook-item').change();
                } else {
                    $('.timebook-item').prop('checked', false);
                }
            });
            $('#modal-701809').click(function() {
                $.ajax({
                    type: "GET",
                    url: "CustomerShow.aspx/GetLatestInvoiceNumber",
                    data: "{}",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function(msg) {
                        $('#<%= labelInvoiceNumber.ClientID %>').text('<%= company.InvoicePrefix %>-' + ('000' + msg.d).slice(-3));
                    }
                });

                var items = $('.hw-invoice-items tbody');
                var subTotal = 0;
                var vats = new Array();
                items.html('');
                invoiceItems.forEach(function(e) {
                    var vatAmount = e.vat / 100.0 * e.amount;
                    var consultant = e.consultant == '' ? '' : ' (' + e.consultant + ')';
                    var date = (e.date != '' && !e.datehidden) ? e.date + ' ' : '';
                    var item = e.comments == '' ? e.item : date + e.comments + consultant;
                    if (e.header) {
                        items.append('' + 
                            '<tr>' + 
                            '   <td colspan="4">' + item + '<input type="hidden" id="invoice-timebooks" name="invoice-timebooks" value="' + e.id + '"></td>' + 
                            '   <td></td>' + 
                            '   <td></td>' + 
                            '   <td class="text-right"></td>' + 
                            '   <td class="text-right"></td>' + 
                            '   <td><input type="text" class="form-control" id="invoice-timebooks-sortorder" name="invoice-timebooks-sortorder" value="" /></td>' +
                            '</tr>' + 
                        '');
                    } else {
                        items.append('' + 
                            '<tr>' + 
                            '   <td colspan="4">' + item + '<input type="hidden" id="invoice-timebooks" name="invoice-timebooks" value="' + e.id + '"></td>' + 
                            '   <td>' + e.qty + '</td>' + 
                            '   <td>' + e.unit + '</td>' + 
                            '   <td class="text-right">' + $.number(e.price, 2, ',', ' ') + '</td>' + 
                            '   <td class="text-right">' + $.number(e.amount, 2, ',', ' ') + '</td>' + 
                            '   <td><input type="text" class="form-control" id="invoice-timebooks-sortorder" name="invoice-timebooks-sortorder" value="" /></td>' +
                            '</tr>' + 
                        '');
                    }
                    subTotal += parseFloat(e.amount);
                    if (vats.hasOwnProperty(e.vat)) {
                        vats[e.vat] += vatAmount;
                    } else {
                        vats[e.vat] = vatAmount;
                    }
                });
                var strVat = '', strVatLabel = '';
                var totalVat = 0;
                var vatKeys = new Array();
                for (var v in vats) {
                    vatKeys.push(v);
                }
                vatKeys.sort();
                for (var i = 0; i < vatKeys.length; i++) {
                    var v = vatKeys[i];
                    strVat += '   <td style="width:10%" class="hw-border-left">' + v + '</td>';
                    strVat += '   <td style="width:10%" class="hw-border-left">' + $.number(vats[v], 2, ',', ' ') + '</td>';
                    strVatLabel += '   <td style="width:10%" class="hw-border-left">VAT %</td>';
                    strVatLabel += '   <td style="width:10%" class="hw-border-left">VAT</td>';
                    totalVat += vats[v];
                }
                var totalAmount = subTotal + totalVat;
                items.append('' +
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

            $('.info').hide();
            $('#buttonEdit').click(function () {
                $('.info-text').hide();
                $('.info').show();
            });
            $('#buttonCancelEdit').click(function () {
                $('.info-text').show();
                $('.info').hide();
            });

            var status = <%= customer.Status %>;
            console.log(status);
            if (status == 0) {
                $('#<%= buttonReactivate.ClientID %>').hide();
                $('#<%= buttonUndelete.ClientID %>').hide();
            } else if (status == 1) {
                $('#<%= buttonDeactivate.ClientID %>').hide();
                $('#<%= buttonUndelete.ClientID %>').hide();
            } else if (status == 2) {
                $('#<%= buttonDeactivate.ClientID %>').hide();
                $('#<%= buttonReactivate.ClientID %>').hide();
                $('#<%= buttonDelete.ClientID %>').hide();
            }

            var subscriptionPanel = $('#<%= panelCustomerSubscription.ClientID %>');
            subscriptionPanel.hide();
            $('#<%= checkBoxSubscribe.ClientID %>').change(function() {
                if ($(this).is(':checked')) {
                    subscriptionPanel.show();
                } else {
                    subscriptionPanel.hide();
                }
            });
            $('#<%= checkBoxSubscribe.ClientID %>').change();
            $('#<%= checkBoxSubscriptionHasEndDate.ClientID %>').change(function() {
                if ($(this).is(':checked')) {
                    $('#subscription-end-date').show();
                } else {
                    $('#subscription-end-date').hide();
                }
            });
            $('#<%= checkBoxTimebookIsHeader.ClientID %>').change(function() {
                if ($(this).is(':checked')) {
                    $('#<%= Panel2.ClientID %>').hide();
                    $('#<%= Panel3.ClientID %>').hide();
                } else {
                    $('#<%= Panel2.ClientID %>').show();
                    $('#<%= Panel3.ClientID %>').show();
                }
            });
            $('#<%= checkBoxSubscriptionHasEndDate.ClientID %>').change();
            
            // Edit timebook consultant through AJAX
            $('.timebook-consultant-text').hide();
            $('.timebook-consultant-spinner').hide();
            $('.timebook-consultant').click(function () {
                var text = $(this).find('.timebook-consultant-text');
                $(this).find('.timebook-consultant-label').hide();
                text.show();
                text.focus();
            });
            $('.timebook-consultant-text').focusout(function () {
                var value = $(this).val();
                if (value == "") {
                    value = "(no consultant)";
                }
                var id = $(this).data('id');
                var label = $(this).closest('td').find('.timebook-consultant-label');
                label.text(value);

                var spinner = $(this).closest('td').find('.timebook-consultant-spinner');
                spinner.show();
                $.ajax({
                    type: 'POST',
                    url: 'CustomerShow.aspx/UpdateTimebookConsultant',
                    data: JSON.stringify({ consultant: value, id: id }),
                    contentType: "application/json; charset=utf-8",
                    dataType: 'json',
                    success: function (msg) {
                        spinner.hide();
                    }
                });
                $(this).hide();
                label.show();
            });

            // Edit timebook comments through AJAX
            $('.timebook-comments-text').hide();
            $('.timebook-comments-spinner').hide();
            $('.timebook-comments').click(function () {
                var text = $(this).find('.timebook-comments-text');
                $(this).find('.timebook-comments-label').hide();
                text.show();
                text.focus();
            });
            $('.timebook-comments-text').focusout(function () {
                var value = $(this).val();
                var id = $(this).data('id');
                var label = $(this).closest('td').find('.timebook-comments-label');
                label.text(value);

                var spinner = $(this).closest('td').find('.timebook-comments-spinner');
                spinner.show();
                $.ajax({
                    type: 'POST',
                    url: 'CustomerShow.aspx/UpdateTimebookComments',
                    data: JSON.stringify({ comments: value, id: id }),
                    contentType: "application/json; charset=utf-8",
                    dataType: 'json',
                    success: function (msg) {
                        spinner.hide();
                    }
                });
                $(this).hide();
                label.show();
            });
            $('#<%= dropDownListInvoiceYourReferencePerson.ClientID %>').change(function () {
                var selected = $(this).find('option:selected');
                var selectedReferenceNumber = selected.data('purchase-order-number');
                $('#<%= labelInvoicePurchaseOrderNumber.ClientID %>').text(selectedReferenceNumber);
            });
        });
    </script>
    <script type="text/javascript">
        // Validation javascript files.
        function validateNotes() {
            var errors = [];
            if ($('#<%= textBoxNotes.ClientID %>').val() == '') {
                errors.push("Notes shouldn't be empty");
            }
            return displayMessage(errors, '#notes-message');
        }

        function validateCustomerPrice() {
            var errors = [];
            var price = $('#<%= textBoxItemPrice.ClientID %>').val();
            addErrorIf(errors, price == '', "Price shouldn't be empty.");
            //addErrorIf(errors, isNaN(price), "Price should be a number.");
            addErrorIf(errors, !isNaN(price) && parseFloat(price) <= 0, "Price should be greater than zero.");
            return displayMessage(errors, '#customer-price-message');
        }

        function validateContactPerson() {
            var errors = [];
            addErrorIf(errors, $('#<%= textBoxContact.ClientID %>').val() == '', "Contact person name shouldn't be empty.");
            return displayMessage(errors, '#contact-person-message');
        }

        function validateTimebook() {
            var errors = [];
            //addErrorIf(errors, $('#<%= textBoxTimebookDepartment.ClientID %>').val() == '', "Department name shouldn't be empty.");
            //addErrorIf(errors, $('#<%= textBoxTimebookConsultant.ClientID %>').val() == '', "Consultant shouldn't be empty.");
            addErrorIf(errors, $('#<%= textBoxTimebookComments.ClientID %>').val() == '', "Comments shouldn't be empty.");

            var price = $('#<%= textBoxTimebookPrice.ClientID %>').val();
            addErrorIf(errors, price == '', "Price shouldn't be empty.");
            //addErrorIf(errors, isNaN(price), "Price should be a number.");
            addErrorIf(errors, !isNaN(price) && parseFloat(price) <= 0, "Price should be greater than zero.");
            
            var qty = $('#<%= textBoxTimebookQty.ClientID %>').val();
            addErrorIf(errors, qty == '', "Quantity shouldn't be empty.");
            //addErrorIf(errors, isNaN(qty), "Quantity should be a number.");
            //addErrorIf(errors, !isNaN(qty) && parseFloat(qty) <= 0, "Quantity should be greater than zero.");

            var vat = $('#<%= textBoxTimebookVAT.ClientID %>').val();
            addErrorIf(errors, vat == '', "VAT shouldn't be empty.");
            addErrorIf(errors, isNaN(vat), "VAT should be a number.");
            //addErrorIf(errors, !isNaN(vat) && parseFloat(vat) <= 0, "VAT should be greater than zero.");
            
            return displayMessage(errors, '#timebook-message');
        }
    </script>
    <style type="text/css">
        .timebook-list td, .timebook-list th {
            padding:5px !important;
        }
        .comments-width {
            width:300px;
        }
        .date-width 
        {
            width:120px;
        }
        .label a 
        {
            color:White;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<h3><asp:Label ID="labelCustomer" runat="server" Text=""></asp:Label></h3>

<% if (customer.HasSubscription) { %>
<p><%= HtmlHelper.Anchor("<< Back to customer listing", "customers.aspx") %></p>
<% } else { %>
<p><%= HtmlHelper.Anchor("<< Back to customer listing", "customers.aspx?SelectedTab=nonsubscribers")%></p>
<% } %>

<% if (message != null && message != "") { %>
    <%= message %>
<% } %>

<div class="tabbable" id="tabs-179602">
	<ul class="nav nav-tabs">
	    <li <%= selectedTab == "notes" ? "class='active'" : "" %>><a href="#notes" data-toggle="tab">Note</a></li>
	    <li <%= selectedTab == "timebook" ? "class='active'" : "" %>><a href="#timebook" data-toggle="tab">Timebook</a></li>
	    <li <%= selectedTab == "customer-prices" ? "class='active'" : "" %>><a href="#customer-prices" data-toggle="tab">Customer Prices</a></li>
		<li <%= selectedTab == "customer-info" ? "class='active'" : "" %>><a href="#customer-info" data-toggle="tab">Customer Info</a></li>
		<li <%= selectedTab == "contact-persons" ? "class='active'" : "" %>><a href="#contact-persons" data-toggle="tab">Contact Persons</a></li>
        <% if (company.HasSubscriber) { %>
            <li <%= selectedTab == "subscription" ? "class='active'" : "" %>><a href="#subscription" data-toggle="tab">Subscription</a></li>
        <% } %>
        <li <%= selectedTab == "agreements" ? "class='active'" : "" %>><a href="#agreements" data-toggle="tab">Agreements</a></li>
	</ul>
	<div class="tab-content">
        <div class="tab-pane <%= selectedTab == "notes" ? "active" : "" %>" id="notes">
			<br />
            <p><a id="modal-625558" href="#customer-notes-form" role="button" class="btn btn-info" data-toggle="modal">Create note</a></p>
            <div class="modal fade" id="customer-notes-form" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
				<div class="modal-dialog">
					<div class="modal-content">
						<div class="modal-header">
							<button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
							<h4 class="modal-title" id="H3">Create note</h4>
						</div>
						<div class="modal-body">
                            <span id="notes-message"></span>
                            <div class="form-group">
                                <label>Created By: <%= Session["UserRealName"] %></label>
                            </div>
                            <div class="form-group">
	                            <label for="<%= textBoxNotes.ClientID %>">Note</label>
                                <asp:TextBox ID="textBoxNotes" runat="server" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                            </div>
						</div>
						<div class="modal-footer">
							<button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                            <asp:Button ID="buttonSaveNotes" OnClientClick="return validateNotes()" runat="server" Text="Save note" CssClass="btn btn-primary" OnClick="buttonSaveNotes_Click" />
						</div>
					</div>
				</div>
			</div>
			<div class="alert alert-info">
				<strong>Customer note</strong> is a brief records of facts, topics, or thoughts, written down as an aid to memory.
			</div>
            <table class="table table-hover">
                <tr>
                    <th style="width:20%">Date</th>
                    <th style="width:10%">Creator</th>
                    <th>Note</th>
                    <th></th>
                </tr>
                <% foreach (var n in notes) { %>
                    <% if (n.Inactive) { %>
                        <tr>
                            <td><strike><%= n.CreatedAt.Value.ToString("yyyy-MM-dd H:mm:ss") %></strike></td>
                            <td><strike><%= n.CreatedBy.Name %></strike></td>
                            <td><strike><%= n.Notes %></strike></td>
                            <td>
                                <%= HtmlHelper.Anchor(" ", string.Format("customernotesedit.aspx?Id={0}&CustomerId={1}", n.Id, id), "title='Edit' class='glyphicon glyphicon-pencil'")%>
                                <%= HtmlHelper.Anchor(" ", string.Format("customernotesdelete.aspx?Id={0}&CustomerId={1}", n.Id, id), "title='Delete' class='glyphicon glyphicon-trash' onclick=\"return confirm('Are you sure you want to delete this customer note?')\"")%>
                            </td>
                        </tr>
                    <% } else { %>
                        <tr>
                            <td><%= n.CreatedAt.Value.ToString("yyyy-MM-dd H:mm:ss") %></td>
                            <td>
                                <% if (n.CreatedBy.Color != "") { %>
                                    <span class="label" style="background:<%= n.CreatedBy.Color %>">
                                        <%= n.CreatedBy.Name %>
                                    </span>
                                <% } else { %>
                                    <span><%= n.CreatedBy.Name %></span>
                                <% } %>
                            </td>
                            <td><%= n.Notes %></td>
                            <td>
                                <%= HtmlHelper.Anchor(" ", string.Format("customernotesedit.aspx?Id={0}&CustomerId={1}", n.Id, id), "title='Edit' class='glyphicon glyphicon-pencil'")%>
                                <%= HtmlHelper.Anchor(" ", string.Format("customernotesdeactivate.aspx?Id={0}&CustomerId={1}", n.Id, id), "title='Deactivate' class='glyphicon glyphicon-remove'")%>
                            </td>
                        </tr>
                    <% } %>
                <% } %>
            </table>
		</div>
		<div class="tab-pane <%= selectedTab == "timebook" ? "active" : "" %>" id="timebook">
			<br />
            <p>
                <a id="modal-717670" href="#timebook-form" role="button" class="btn btn-info" data-toggle="modal">Add a timebook</a>
                <a id="modal-701809" href="#modal-container-701809" role="button" class="btn btn-default" data-toggle="modal">Create Invoice</a>
            </p>
			<asp:Panel ID="panelCustomerTimebook" DefaultButton="buttonSaveTimebook" runat="server">
			<div class="modal fade" id="timebook-form" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
				<div class="modal-dialog">
					<div class="modal-content">
						<div class="modal-header">
							<button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
							<h4 class="modal-title" id="myModalLabel">Add a timebook</h4>
						</div>
						<div class="modal-body">
                            <span id="timebook-message"></span>
                            <div class="form-group">
	                            <label for="<%= textBoxTimebookDate.ClientID %>">Date</label>
                                <asp:TextBox ID="textBoxTimebookDate" runat="server" CssClass="form-control date"></asp:TextBox>
                            </div>
                            <div class="form-group">
                                <asp:CheckBox ID="checkBoxTimebookIsHeader" runat="server" CssClass="form-control" Text="&nbsp;This timebook is a header type" />
                            </div>

                            <asp:Panel ID="Panel2" runat="server">
                                <div class="form-group">
                                    <asp:CheckBox ID="checkBoxTimebookDateHidden" runat="server" CssClass="form-control" Text="&nbsp;Timebook date is hidden" />
                                </div>
                                <div class="form-group">
	                                <label for="<%= textBoxTimebookDepartment.ClientID %>">Department</label>
                                    <asp:TextBox ID="textBoxTimebookDepartment" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="form-group">
	                                <label for="<%= dropDownListTimebookContacts.ClientID %>">Contact Person</label>
                                    <asp:DropDownList ID="dropDownListTimebookContacts" runat="server" CssClass="form-control">
                                    </asp:DropDownList>
                                </div>
                                <div class="form-group">
	                                <label for="<%= dropDownListTimebookItems.ClientID %>">Item</label>
                                    <asp:DropDownList ID="dropDownListTimebookItems" runat="server" CssClass="form-control"></asp:DropDownList>
                                </div>
                                <div class="form-group">
	                                <label for="<%= labelTimebookUnit.ClientID %>">Unit</label>
                                    <asp:Label ID="labelTimebookUnit" runat="server" Text="" CssClass="form-control"></asp:Label>
                                </div>
                                <div class="form-group">
	                                <label for="<%= textBoxTimebookQty.ClientID %>">Qty</label>
                                    <asp:TextBox ID="textBoxTimebookQty" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="form-group">
	                                <label for="<%= textBoxTimebookPrice.ClientID %>">Price</label>
                                    <asp:TextBox ID="textBoxTimebookPrice" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="form-group">
	                                <label for="<%= textBoxTimebookVAT.ClientID %>">VAT</label>
                                    <asp:TextBox ID="textBoxTimebookVAT" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="form-group">
	                                <label for="<%= textBoxTimebookConsultant.ClientID %>">Consultant</label>
                                    <asp:TextBox ID="textBoxTimebookConsultant" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                            </asp:Panel>
                            <div class="form-group">
	                            <label for="<%= textBoxTimebookComments.ClientID %>">Comments</label>
                                <asp:TextBox ID="textBoxTimebookComments" runat="server" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                            </div>
                            <asp:Panel ID="Panel3" runat="server">
                                <div class="form-group">
	                                <label for="<%= textBoxTimebookInternalComments.ClientID %>">Internal Comments</label>
                                    <asp:TextBox ID="textBoxTimebookInternalComments" runat="server" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                                </div>
                            </asp:Panel>
						</div>
						<div class="modal-footer">
							<button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                            <asp:Button ID="buttonSaveTimebook" OnClientClick="return validateTimebook()" runat="server" Text="Save timebook" CssClass="btn btn-primary" OnClick="buttonSaveTimebook_Click" />
						</div>
					</div>
				</div>
			</div>
            </asp:Panel>
			<div class="modal fade" id="modal-container-701809" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
				
                <div class="modal-dialog" style="width:80%">
	                <div class="modal-content">
		                <div class="modal-header">
			                <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
			                <h4 class="modal-title" id="H1">Create an invoice</h4>
		                </div>
		                <div class="modal-body">
                            <table width="100%" cellpadding="5px">
                                <tr>
                                    <td rowspan="4" valign="bottom">
                                        <% if (company.HasInvoiceLogo) { %>
                                            <img src="uploads/<%= company.InvoiceLogo %>" style="width:<%= company.InvoiceLogoPercentage %>%" />
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
                                        <asp:TextBox ID="textBoxInvoiceDate" runat="server" CssClass="form-control date"></asp:TextBox>
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
                                                <asp:Label ID="labelInvoiceYourReferencePerson" runat="server" Text=""></asp:Label></strong>
                                                <asp:DropDownList ID="dropDownListInvoiceYourReferencePerson" runat="server">
                                                </asp:DropDownList><br />
                                            Our Reference: <strong>
                                                <asp:Label ID="labelInvoiceOurReferencePerson" runat="server" Text=""></asp:Label></strong>
                                        </small>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Panel ID="panelPurchaseOrderNumber" runat="server">
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
                                    <td class="hw-border-left" colspan="4">Item</td>
                                    <td class="hw-border-left" style="width:10%">Qty</td>
                                    <td class="hw-border-left" style="width:10%">Unit</td>
                                    <td class="hw-border-left" style="width:10%">Price/Unit</td>
                                    <td class="hw-border-left" style="width:10%">Amount</td>
                                    <td class="hw-border-last" style="width:10%">#</td>
                                </tr>
                                </thead>
                                <tbody>
                                </tbody>
                            </table>
                            <div class="form-group">
	                            <label for="<%= textBoxInvoiceComments.ClientID %>">Comments</label>
                                <asp:TextBox CssClass="form-control" ID="textBoxInvoiceComments" runat="server" TextMode="MultiLine"></asp:TextBox>
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
		                </div>
		                <div class="modal-footer">
			                <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                            <asp:Button ID="buttonSaveInvoice" runat="server" Text="Save invoice" CssClass="btn btn-primary" OnClick="buttonSaveInvoice_Click" />
		                </div>
	                </div>
                </div>

			</div>
			<div class="alert alert-info">
				<strong>Time book</strong> is a sheet for recording the time of arrival and departure of workers and for recording the amount of time spent on each job.
			</div>
            <table class="table table-hover timebook-list">
                <tr>
                    <th><input type="checkbox" id="checkbox-timebook-all" /></th>
                    <%--<th></th>--%>
                    <th style="width:85px !important">Date</th>
                    <th>Item / Unit / Department</th>
                    <th>Quantity</th>
                    <th style="text-align:right">Price</th>
                    <th style="text-align:right">Amount</th>
                    <th style="text-align:right">VAT</th>
                    <th></th>
                    <th></th>
                </tr>
                <% foreach (var t in timebooks) { %>
                    <% if (t.Inactive) { %>
                        <tr>
                            <td style="width:16px"></td>
                            <td>
                                <% if (t.Date != null && !t.DateHidden && !t.IsSubscription && !t.IsHeader) { %>
                                    <strike><%= t.Date.Value.ToString("yyyy-MM-dd") %></strike>
                                <% } %><br />
                                <small class="text-success">
                                    <strike><%= StrHelper.Str(t.Consultant != "", StrHelper.Str2(t.Consultant, 10), "(no consultant)") %></strike>
                                </small>
                            </td>
                            <% if (t.IsHeader || t.IsSubscription) { %>
                                <td class="timebook-comments"><strike><%= t.Comments %></strike></td>
                                <td></td>
                                <td></td>
                                <td></td>
                                <td></td>
                            <% } else { %>
                                <td>
                                    <strike><%= t.Item.Name %> /
                                    <%= t.Item.Unit.Name %> /
                                    <%= t.Department %></strike><br />
                                    <small class="text-muted">
                                        <span class="timebook-comments">
                                            <% if (t.HasInternalComments) { %>
                                                <img src="img/comment.png" title="<%= t.InternalComments %>"/>
                                            <% } %>
                                            <span class="timebook-comments-label" title="<%= StrHelper.Str(t.Comments.Length > 150, t.Comments, "") %>">
                                                <strike><%= StrHelper.Str2(t.Comments, 150) %></strike>
                                            </span>
                                        </span>
                                    </small>
                                </td>
                                <td style="width:80px !important;text-align:center"><strike><%= t.Quantity.ToString("### ### ##0.00") %></strike></td>
                                <td style="width:110px !important;text-align:right"><strike><%= t.Price.ToString("### ### ##0.00") %></strike></td>
                                <td style="width:110px !important;text-align:right"><strike><%= t.Amount.ToString("### ### ##0.00") %></strike></td>
                                <td style="text-align:right">
                                    <small class="text-success"><strike><%= t.VAT %>%</strike></small>
                                </td>
                            <% } %>
                            <td><%= t.GetStatus() %></td>
                            <td>
                                <%= HtmlHelper.Anchor(" ", string.Format("customertimebookedit.aspx?Id={0}&CustomerId={1}", t.Id, id), "title='Edit' class='glyphicon glyphicon-pencil'")%>
                                <%= HtmlHelper.Anchor(" ", string.Format("customertimebookdelete.aspx?Id={0}&CustomerId={1}", t.Id, id), "title='Delete' class='glyphicon glyphicon-trash' onclick=\"return confirm('Are you sure you want to delete this timebook?')\"")%>
                            </td>
                        </tr>
                    <% } else { %>
                        <tr>
                            <td style="width:16px">
                                <% if (t.Status == 0) { %>
                                    <input type="checkbox" class="timebook-item" id="timebook-<%= t.Id %>"
                                        data-id="<%= t.Id %>"
                                        data-item="<%= t.Item.Name %>"
                                        data-unit="<%= t.Item.Unit.Name %>"
                                        data-consultant="<%= t.Item.Consultant %>"
                                        data-qty="<%= t.Quantity %>"
                                        data-price="<%= t.Price %>"
                                        data-amount="<%= t.Amount %>"
                                        data-consultant="<%= t.Consultant %>"
                                        data-comments="<%= t.Comments %>"
                                        data-vat="<%= t.VAT %>"
                                        data-date="<%= t.Date != null ? t.Date.Value.ToString("yyyy-MM-dd") : "" %>"
                                        data-datehidden="<%= t.DateHidden ? "true" : "false" %>"
                                        data-header="<%= t.IsHeader ? "true" : "false" %>"
                                />
                                <% } %>
                            </td>
                            <td>
                                <% if (t.Date != null && !t.DateHidden && !t.IsSubscription && !t.IsHeader) { %>
                                    <%= t.Date.Value.ToString("yyyy-MM-dd") %>
                                <% } %>
                                <% if (!t.IsHeader) { %>
                                <br />
                                <small class="text-success timebook-consultant">
                                    <span class="timebook-consultant-label"><%= StrHelper.Str(t.Consultant != "", StrHelper.Str2(t.Consultant, 10), "(no consultant)") %></span>
                                    <textarea data-id="<%= t.Id %>" type="text" class="form-control timebook-consultant-text"><%= t.Consultant %></textarea>
                                    <img alt="" class="timebook-consultant-spinner" src="img/spiffygif_30x30.gif" />
                                </small>
                                <% } %>
                            </td>
                            <% if (t.IsHeader || t.IsSubscription) { %>
                                <td class="timebook-comments"><%= t.Comments %></td>
                                <td></td>
                                <td></td>
                                <td></td>
                                <td></td>
                            <% } else { %>
                                <td>
                                    <%= t.Item.Name %> /
                                    <%= t.Item.Unit.Name %> /
                                    <%= t.Department %><br />
                                    <small class="text-muted">
                                        <span class="timebook-comments">
                                            <% if (t.HasInternalComments) { %>
                                                <img src="img/comment.png" title="<%= t.InternalComments %>"/>
                                            <% } %>
                                            <span class="timebook-comments-label" title="<%= StrHelper.Str(t.Comments.Length > 150, t.Comments, "") %>">
                                                <%= StrHelper.Str2(t.Comments, 150) %>
                                            </span>
                                            <textarea data-id="<%= t.Id %>" type="text" class="form-control timebook-comments-text"><%= t.Comments %></textarea>
                                            <img alt="" class="timebook-comments-spinner" src="img/spiffygif_30x30.gif" />
                                        </span>
                                    </small>
                                </td>
                                <td style="width:80px !important;text-align:center"><%= t.Quantity.ToString("### ### ##0.00") %></td>
                                <td style="width:110px !important;text-align:right"><%= t.Price.ToString("### ### ##0.00") %></td>
                                <td style="width:110px !important;text-align:right"><%= t.Amount.ToString("### ### ##0.00") %></td>
                                <td style="text-align:right">
                                    <small class="text-success"><%= t.VAT %>%</small>
                                </td>
                            <% } %>
                            <td><%= t.GetStatus() %></td>
                            <td style="width:48px !important">
                                <% if (t.IsPaid) { %>
                                <% } else if (t.IsInvoiced) { %>
                                    <%= HtmlHelper.Anchor(" ", string.Format("customertimebookedit.aspx?Id={0}&CustomerId={1}", t.Id, id), "title='Edit' class='glyphicon glyphicon-pencil'")%>
                                <% } else { %>
                                    <%= HtmlHelper.Anchor(" ", string.Format("customertimebookedit.aspx?Id={0}&CustomerId={1}", t.Id, id), "title='Edit' class='glyphicon glyphicon-pencil'")%>
                                    <%= HtmlHelper.Anchor(" ", string.Format("customertimebookdeactivate.aspx?Id={0}&CustomerId={1}", t.Id, id), "title='Deactivate' class='glyphicon glyphicon-remove'")%>
                                <% } %>
                            </td>
                        </tr>
                    <% } %>
                <% } %>
            </table>
		</div>
		<div class="tab-pane <%= selectedTab == "customer-prices" ? "active" : "" %>" id="customer-prices">
			<br />
            <p><a id="modal-692185" href="#customer-prices-form" role="button" class="btn btn-info" data-toggle="modal">Add customer price</a></p>
			<asp:Panel ID="panelCustomerPrice" DefaultButton="buttonSaveItem" runat="server">
			<div class="modal fade" id="customer-prices-form" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
				<div class="modal-dialog">
					<div class="modal-content">
						<div class="modal-header">
							<button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
							<h4 class="modal-title" id="H2">
								Add customer price
							</h4>
						</div>
						<div class="modal-body">
                            <span id="customer-price-message"></span>
                            <div class="form-group">
	                            <label for="<%= dropDownListItems.ClientID %>">Item</label>
                                <asp:DropDownList ID="dropDownListItems" runat="server" CssClass="form-control">
                                </asp:DropDownList>
                            </div>
                            <div class="form-group">
	                            <label for="<%= textBoxItemPrice.ClientID %>">Price</label>
                                <asp:TextBox ID="textBoxItemPrice" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
						</div>
						<div class="modal-footer">
							<button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                            <asp:Button ID="buttonSaveItem" OnClientClick="return validateCustomerPrice()" runat="server" Text="Save price" CssClass="btn btn-primary" OnClick="buttonSaveItem_Click" />
						</div>
					</div>
				</div>
			</div>
            </asp:Panel>
			<div class="alert alert-info">
                In ordinary usage, <strong>price</strong> is the quantity of payment or compensation given by one party to another in return for goods or services.
			</div>
            <table class="table table-hover">
                <tr>
                    <th>Item</th>
                    <th>Ordinary Price</th>
                    <th>Price</th>
                    <th></th>
                </tr>
                <% foreach (var p in prices) { %>
                    <% if (p.Inactive) { %>
                        <tr>
                            <td><strike><%= p.Item.Name %></strike></td>
                            <td><strike><%= p.Item.Price.ToString("#### ##0.00") %></strike></td>
                            <td><strike><%= p.Price.ToString("### ##0.00") %></strike></td>
                            <td>
                                <%= HtmlHelper.Anchor(" ", string.Format("customerpriceedit.aspx?Id={0}&CustomerId={1}", p.Id, id), "title='Edit' class='glyphicon glyphicon-pencil'")%>
                                <%= HtmlHelper.Anchor(" ", string.Format("customerpricedelete.aspx?Id={0}&CUstomerId={1}", p.Id, id), "title='Delete' class='glyphicon glyphicon-trash' onclick=\"return confirm('Are you sure you want to delete this customer price?')\"")%>
                                <%= HtmlHelper.Anchor(" ", string.Format("customerpricemoveup.aspx?SortOrder={0}&CustomerId={1}", p.SortOrder, id), "title='Move Up' class='glyphicon glyphicon-arrow-up'")%>
                                <%= HtmlHelper.Anchor(" ", string.Format("customerpricemovedown.aspx?SortOrder={0}&CustomerId={1}", p.SortOrder, id), "title='Move Down' class='glyphicon glyphicon-arrow-down'")%>
                            </td>
                        </tr>
                    <% } else { %>
                        <tr>
                            <td><%= p.Item.Name %></td>
                            <td><%= p.Item.Price.ToString("### ##0.00") %></td>
                            <td><%= p.Price.ToString("### ##0.00") %></td>
                            <td>
                                <%= HtmlHelper.Anchor(" ", string.Format("customerpriceedit.aspx?Id={0}&CustomerId={1}", p.Id, id), "title='Edit' class='glyphicon glyphicon-pencil'")%>
                                <%= HtmlHelper.Anchor(" ", string.Format("customerpricedeactivate.aspx?Id={0}&CustomerId={1}", p.Id, id), "title='Deactivate' class='glyphicon glyphicon-remove'")%>
                                <%= HtmlHelper.Anchor(" ", string.Format("customerpricemoveup.aspx?SortOrder={0}&CustomerId={1}", p.SortOrder, id), "title='Move Up' class='glyphicon glyphicon-arrow-up'")%>
                                <%= HtmlHelper.Anchor(" ", string.Format("customerpricemovedown.aspx?SortOrder={0}&CustomerId={1}", p.SortOrder, id), "title='Move Down' class='glyphicon glyphicon-arrow-down'")%>
                            </td>
                        </tr>
                    <% } %>
                <% } %>
            </table>
		</div>
		<div class="tab-pane <%= selectedTab == "customer-info" ? "active" : "" %>" id="customer-info">
            <br />
			<div class="alert alert-info">
				<%--<strong>Customer information</strong> is lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.--%>
                <strong>Customer information</strong> is the details of the customer including the information regarding phone, email and invoicing address, etc.
			</div>
            <asp:Panel ID="panelCustomerInfo" DefaultButton="buttonSave" runat="server">
			<table class="table">
                <tr>
                    <td style="width: 30%;"><strong>Customer Name</strong></td>
                    <td>
                        <asp:Label ID="labelCustomerName" runat="server" Text="" CssClass="info-text"></asp:Label>
                        <asp:TextBox ID="textBoxCustomerName" runat="server" CssClass="info form-control"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="width: 30%;"><strong>Customer Number</strong></td>
                    <td>
                        <asp:Label ID="labelCustomerNumber" runat="server" Text="" CssClass="info-text"></asp:Label>
                        <asp:TextBox ID="textBoxCustomerNumber" runat="server" CssClass="info form-control"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td><strong>Postal Address</strong></td>
                    <td>
                        <asp:Label ID="labelPostalAddress" runat="server" Text="" CssClass="info-text"></asp:Label>
                        <asp:TextBox ID="textBoxPostalAddress" runat="server" CssClass="info form-control" TextMode="MultiLine"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td><strong>Invoicing Address</strong></td>
                    <td>
                        <asp:Label ID="labelInvoiceAddress" runat="server" Text="" CssClass="info-text"></asp:Label>
                        <asp:TextBox ID="textBoxInvoiceAddress" runat="server" CssClass="info form-control" TextMode="MultiLine"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td><strong>Invoicing Email</strong></td>
                    <td>
                        <asp:Label ID="labelInvoiceEmail" runat="server" Text="" CssClass="info-text"></asp:Label>
                        <asp:TextBox ID="textBoxInvoiceEmail" runat="server" CssClass="info form-control"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td><strong>Invoicing Email CC</strong></td>
                    <td>
                        <asp:Label ID="labelInvoiceEmailCC" runat="server" Text="" CssClass="info-text"></asp:Label>
                        <asp:TextBox ID="textBoxInvoiceEmailCC" runat="server" CssClass="info form-control"></asp:TextBox>
                    </td>
                </tr>
                <%--<tr>
                    <td><strong>Your Reference Person</strong></td>
                    <td>
                        <asp:Label ID="labelYourReferencePerson" runat="server" Text="" CssClass="info-text"></asp:Label>
                        <asp:DropDownList ID="dropDownListYourReferencePerson" runat="server" CssClass=" info form-control">
                        </asp:DropDownList>
                    </td>
                </tr>--%>
                <%--<tr>
                    <td><strong>Reference / Purchase Order Number</strong></td>
                    <td>
                        <asp:Label ID="labelPurchaseOrderNumber" runat="server" Text="" CssClass="info-text"></asp:Label>
                        <asp:TextBox ID="textBoxPurchaseOrderNumber" runat="server" CssClass=" info form-control"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td><strong>Your Reference Person</strong></td>
                    <td>
                        <asp:Label ID="labelYourReferencePerson" runat="server" Text="" CssClass="info-text"></asp:Label>
                        <asp:TextBox ID="textBoxYourReferencePerson" runat="server" CssClass="info form-control"></asp:TextBox>
                    </td>
                </tr>--%>
                <tr>
                    <td><strong>Our Reference Person</strong></td>
                    <td>
                        <asp:Label ID="labelOurReferencePerson" runat="server" Text="" CssClass="info-text"></asp:Label>
                        <asp:TextBox ID="textBoxOurReferencePerson" runat="server" CssClass="info form-control"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td><strong>Phone</strong></td>
                    <td>
                        <asp:Label ID="labelPhone" runat="server" Text="" CssClass="info-text"></asp:Label>
                        <asp:TextBox ID="textBoxPhone" runat="server" CssClass="info form-control"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td><strong>Email</strong></td>
                    <td>
                        <asp:Label ID="labelEmail" runat="server" Text="" CssClass="info-text"></asp:Label>
                        <asp:TextBox ID="textBoxEmail" runat="server" CssClass="info form-control"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td><strong>Language</strong></td>
                    <td>
                        <asp:Label ID="labelLanguage" runat="server" Text="" CssClass="info-text"></asp:Label>
                        <asp:DropDownList ID="dropDownListLanguage" runat="server" CssClass="info form-control">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td><strong>Currency</strong></td>
                    <td>
                        <asp:Label ID="labelCurrency" runat="server" Text="" CssClass="info-text"></asp:Label>
                        <asp:DropDownList ID="dropDownListCurrency" runat="server" CssClass="info form-control">
                        </asp:DropDownList>
                    </td>
                </tr>
            </table>
            </asp:Panel>
            <div>
                <a id="buttonEdit" href="javascript:;" class="info-text btn btn-info">Edit this customer</a>
                <asp:Button ID="buttonSave" runat="server" Text="Save this customer" CssClass="info btn btn-info" OnClick="buttonSave_Click" />
                <asp:Button ID="buttonDeactivate" runat="server" Text="Deactivate this customer" CssClass="btn btn-warning" OnClick="buttonDeactivate_Click" OnClientClick="return confirm('Are you sure you want to de-activate this customer?')" />
                <asp:Button ID="buttonReactivate" runat="server" Text="Reactivate this customer" CssClass="btn btn-warning" OnClick="buttonReactivate_Click" OnClientClick="return confirm('Are you sure you want to re-activate this customer?')" />
                <asp:Button ID="buttonDelete" runat="server" Text="Delete this customer" CssClass="btn btn-warning" OnClick="buttonDelete_Click" OnClientClick="return confirm('Are you sure you want to delete this customer?')" />
                <asp:Button ID="buttonUndelete" runat="server" Text="Undelete this customer" CssClass="btn btn-warning" OnClick="buttonUndelete_Click" OnClientClick="return confirm('Are you sure you want to undelete this customer?')" />
                <span class="info">or <i><a id="buttonCancelEdit" href="javascript:;">cancel</a></i></span>
            </div>
		</div>
        <div class="tab-pane <%= selectedTab == "contact-persons" ? "active" : "" %>" id="contact-persons">
			<br />
            <p><a id="modal-240447" href="#modal-container-240447" role="button" class="btn btn-info" data-toggle="modal">Add new contact person</a></p>
			<asp:Panel ID="panelContactPerson" DefaultButton="buttonSaveContact" runat="server">
			    <div class="modal fade" id="modal-container-240447" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
				    <div class="modal-dialog">
					    <div class="modal-content">
						    <div class="modal-header">
							    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
							    <h4 class="modal-title" id="H4">New customer contact</h4>
						    </div>
						    <div class="modal-body">
                                <span id="contact-person-message"></span>
                                <div class="form-group">
	                                <label for="">Contact person</label>
                                    <asp:TextBox ID="textBoxContact" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="form-group">
	                                <label for="">Reference / Purchase Order Number</label>
                                    <asp:TextBox ID="textBoxContactPurchaseOrderNumber" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="form-group">
	                                <label for="">Contact title</label>
                                    <asp:TextBox ID="textBoxContactTitle" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="form-group">
	                                <label for="">Phone</label>
                                    <asp:TextBox ID="textBoxContactPhone" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="form-group">
	                                <label for="">Mobile</label>
                                    <asp:TextBox ID="textBoxContactMobile" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="form-group">
	                                <label for="">Email</label>
                                    <asp:TextBox ID="textBoxContactEmail" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="form-group">
                                    <asp:RadioButtonList ID="radioButtonListContactType" runat="server">
                                    </asp:RadioButtonList>
                                </div>
						    </div>
						    <div class="modal-footer">
							    <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                                <asp:Button ID="buttonSaveContact" OnClientClick="return validateContactPerson()" runat="server" Text="Save contact" CssClass="btn btn-primary" OnClick="buttonSaveContact_Click" />
						    </div>
					    </div>
				    </div>
			    </div>
            </asp:Panel>
			<div class="alert alert-info">
				<strong>Customer contact information</strong> are people serving as a go-between, messenger, connection, or source of special information. Business contacts.
			</div>
            <table class="table table-hover">
                <tr>
                    <td></td>
                    <th>Contact Person</th>
                    <th>Invoice Reference</th>
                    <th>Title</th>
                    <th>Phone</th>
                    <th>Mobile</th>
                    <th>Email</th>
                    <th></th>
                </tr>
                <% foreach (var c in contacts) { %>
                    <% if (c.Inactive) { %>
                        <tr>
                            <td><%= c.GetContactType() %></td>
                            <td><strike><%= c.Name %></strike></td>
                            <td><strike><%= c.PurchaseOrderNumber %></strike></td>
                            <td><strike><%= c.Title %></strike></td>
                            <td><strike><%= c.Phone %></strike></td>
                            <td><strike><%= c.Mobile %></strike></td>
                            <td><strike><%= c.Email %></strike></td>
                            <td>
                                <%= HtmlHelper.Anchor(" ", string.Format("customercontactedit.aspx?Id={0}&CustomerId={1}", c.Id, id), "title='Edit' class='glyphicon glyphicon-pencil'")%>
                                <%= HtmlHelper.Anchor(" ", string.Format("customercontactdelete.aspx?Id={0}&CustomerId={1}", c.Id, id), "title='Delete' class='glyphicon glyphicon-trash' onclick=\"return confirm('Are you sure you want to delete this contact person?')\"")%>
                            </td>
                        </tr>
                    <% } else { %>
                        <tr>
                            <td><%= c.GetContactType() %></td>
                            <td><%= c.Name %></td>
                            <td><%= c.PurchaseOrderNumber %></td>
                            <td><%= c.Title %></td>
                            <td><%= c.Phone %></td>
                            <td><%= c.Mobile %></td>
                            <td><%= c.Email %></td>
                            <td>
                                <%= HtmlHelper.Anchor(" ", string.Format("customercontactedit.aspx?Id={0}&CustomerId={1}", c.Id, id), "title='Edit' class='glyphicon glyphicon-pencil'")%>
                                <%= HtmlHelper.Anchor(" ", string.Format("customercontactdeactivate.aspx?Id={0}&CustomerId={1}", c.Id, id), "title='Deactivate' class='glyphicon glyphicon-remove'")%>
                            </td>
                        </tr>
                    <% } %>
                <% } %>
            </table>
		</div>
        <% if (company.HasSubscriber) { %>
        <div class="tab-pane <%= selectedTab == "subscription" ? "active" : "" %>" id="subscription">
			<br />
			<div class="alert alert-info">
				<strong>Subscription</strong> is the action of making or agreeing to make an advance payment in order to receive or participate in something.
			</div>
            <div class="form-group">
                <asp:CheckBox ID="checkBoxSubscribe" runat="server" CssClass="form-control" Text="&nbsp;This customer has subscription" />
            </div>
            <asp:Panel runat="server" DefaultButton="buttonSaveSubscription" ID="panelCustomerSubscription">
            <table class="table">
                <tr>
                    <td><strong>Subscription Item</strong></td>
                    <td>
                        <asp:DropDownList ID="dropDownListSubscriptionItem" runat="server" CssClass="form-control">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td><strong>Start Date</strong></td>
                    <td>
                        <asp:TextBox CssClass="form-control date" ID="textBoxSubscriptionStartDate" runat="server"></asp:TextBox></td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:CheckBox ID="checkBoxSubscriptionHasEndDate" runat="server" CssClass="form-control" Text="&nbsp;This subscription has end date" />
                    </td>
                </tr>
                <tr id="subscription-end-date">
                    <td><strong>End Date</strong></td>
                    <td>
                        <asp:TextBox CssClass="date form-control" ID="textBoxSubscriptionEndDate" runat="server"></asp:TextBox></td>
                </tr>
            </table>
            </asp:Panel>
            <div>
                <asp:Button ID="buttonSaveSubscription" runat="server" Text="Save customer subscription" CssClass="btn btn-info" OnClick="buttonSaveSubscription_Click" />
            </div>
		</div>
        <% } %>
        <div class="tab-pane <%= selectedTab == "agreements" ? "active" : "" %>" id="agreements">
			<br />
            <p><a id="modal-931985" href="#modal-container-931985" role="button" class="btn btn-info" data-toggle="modal">Create an agreement</a></p>
            <asp:Panel ID="panel1" DefaultButton="buttonSaveContact" runat="server">
			    <div class="modal fade" id="modal-container-931985" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
				    <div class="modal-dialog">
					    <div class="modal-content">
						    <div class="modal-header">
							    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
							    <h4 class="modal-title" id="H5">New agreement</h4>
						    </div>
						    <div class="modal-body">
                                <div class="form-group">
	                                <label for="<%= textBoxAgreementLecturer.ClientID %>">Lecturer</label>
                                    <asp:TextBox ID="textBoxAgreementLecturer" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="form-group">
	                                <label for="<%= textBoxAgreementDate.ClientID %>">Date</label>
                                    <asp:TextBox ID="textBoxAgreementDate" runat="server" CssClass="form-control date"></asp:TextBox>
                                </div>
                                <div class="form-group">
	                                <label for="<%= textBoxAgreementRuntime.ClientID %>">Runtime</label>
                                    <asp:TextBox ID="textBoxAgreementRuntime" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="form-group">
	                                <label for="<%= textBoxAgreementLectureTitle.ClientID %>">Lecture Title</label>
                                    <asp:TextBox ID="textBoxAgreementLectureTitle" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="form-group">
	                                <label for="<%= textBoxAgreementLocation.ClientID %>">Location</label>
                                    <asp:TextBox ID="textBoxAgreementLocation" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="form-group">
	                                <label for="<%= textBoxAgreementContact.ClientID %>">Contact</label>
                                    <asp:TextBox ID="textBoxAgreementContact" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="form-group">
	                                <label for="<%= textBoxAgreementMobile.ClientID %>">Mobile</label>
                                    <asp:TextBox ID="textBoxAgreementMobile" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="form-group">
	                                <label for="<%= textBoxAgreementEmail.ClientID %>">Email</label>
                                    <asp:TextBox ID="textBoxAgreementEmail" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="form-group">
	                                <label for="<%= textBoxAgreementCompensation.ClientID %>">Compensation</label>
                                    <asp:TextBox ID="textBoxAgreementCompensation" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="form-group">
	                                <label for="<%= textBoxAgreementPaymentTerms.ClientID %>">Payment Terms</label>
                                    <asp:TextBox ID="textBoxAgreementPaymentTerms" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="form-group">
	                                <label for="<%= textBoxAgreementBillingAddress.ClientID %>">Billing Address</label>
                                    <asp:TextBox ID="textBoxAgreementBillingAddress" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="form-group">
	                                <label for="<%= textBoxAgreementOtherInformation.ClientID %>">Other Information</label>
                                    <asp:TextBox ID="textBoxAgreementOtherInformation" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
						    </div>
						    <div class="modal-footer">
							    <button type="button" class="btn btn-default" data-dismiss="modal">Close</button> 
                                <asp:Button ID="buttonSaveAgreement" CssClass="btn btn-success" runat="server" Text="Save agreement" OnClick="buttonSaveAgreement_Click" />
						    </div>
					    </div>
				    </div>
			    </div>
            </asp:Panel>
            <div class="alert alert-info">
				<strong>Agreeements</strong> are negotiated and typically legally binding arrangement between parties as to a course of action.
			</div>
            <table class="table table-hover">
                <tr>
                    <th>Date Created</th>
                    <th>Lecturer</th>
                    <th>Lecturer Title</th>
                    <th>Contact</th>
                    <th>Mobile</th>
                    <th>Email</th>
                    <th>Status</th>
                    <th></th>
                </tr>
                <% foreach (var a in agreements) { %>
                    <tr>
                        <td><%= a.Date.Value.ToString("yyyy-MM-dd") %></td>
                        <td><%= a.Lecturer %></td>
                        <td style="width:250px"><%= a.LectureTitle %></td>
                        <td><%= a.Contact %></td>
                        <td><%= a.Mobile %></td>
                        <td><%= a.Email %></td>
                        <td>
                            <%= a.IsClosed ? "Closed" : "Open" %>
                        </td>
                        <td>
                            <%= HtmlHelper.Anchor(" ", string.Format("customeragreementedit.aspx?Id={0}&CustomerId={1}", a.Id, id), "title='Edit' class='glyphicon glyphicon-pencil'")%>
                            <%= HtmlHelper.Anchor("Show", string.Format("customeragreementshow.aspx?Id={0}&CompanyId={1}&CustomerId={2}", a.Id, companyId, id), "title='Show' class=''")%>
                            <%= HtmlHelper.Anchor("Send link", string.Format("customeragreementsendlink.aspx?Id={0}&CompanyId={1}&CustomerId={2}", a.Id, companyId, id), "title='Send agreement link' class=''")%>
                            <%= HtmlHelper.Anchor(" ", string.Format("customeragreementdownload.aspx?Id={0}&CompanyId={1}&CustomerId={2}", a.Id, companyId, id), "title='Download PDF' class='glyphicon glyphicon-download-alt'")%>
                            <%= HtmlHelper.Anchor(" ", string.Format("customeragreementsend.aspx?Id={0}&CompanyId={1}&CustomerId={2}", a.Id, companyId, id), "title='Send agreement' class='glyphicon glyphicon-envelope'")%>
                            <%= HtmlHelper.Anchor(" ", string.Format("customeragreementdeactivate.aspx?Id={0}&CustomerId={1}", a.Id, id), "title='Deactivate' class='glyphicon glyphicon-remove'")%>
                        </td>
                    </tr>
                <% } %>
            </table>
        </div>
	</div>
</div>

</asp:Content>
