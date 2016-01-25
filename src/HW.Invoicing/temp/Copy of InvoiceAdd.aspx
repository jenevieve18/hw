<%@ Page Title="" Language="C#" MasterPageFile="~/Invoicing.Master" AutoEventWireup="true" CodeBehind="InvoiceAdd.aspx.cs" Inherits="HW.Invoicing.InvoiceAdd" %>
<%@ Import Namespace="HW.Core.Helpers" %>
<%@ Import Namespace="HW.Invoicing.Core.Helpers" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

<link href="//cdn.rawgit.com/Eonasdan/bootstrap-datetimepicker/d004434a5ff76e7b97c8b07c01f34ca69e635d97/build/css/bootstrap-datetimepicker.css" rel="stylesheet">

<script type="text/javascript" language=javascript>
    var items = <%= ItemHelper.ToJson(items) %>;
    $(document).ready(function () {
        $('#invoice-date').datetimepicker();
        $('#invoice-due-date').datetimepicker();
        $('#add-invoice-item').click(function () {
            $('#invoice-items').append('<tr>' + $('#invoice-item-template').html() + '</tr>');
            $('select[name="item"]').change(itemChange);
            $('input[name="quantity"]').change(priceChange);
        });
        $('select[name="item"]').change(itemChange);
        $('input[name="quantity"]').change(priceChange);
        $('#items').change();
    });
    function priceChange() {
        var quantity = parseF($(this).val());
        var price = parseF($(this).closest('tr').find('input[name="price"]').val());
        var amount = quantity * price;
        $(this).closest('tr').find('.amount').text('SEK' + Math.round(amount).toFixed(2));
    }
    function parseF(v) {
        if (isNaN(parseFloat(v))) {
            return 0;
        }
        return parseFloat(v);
    }
    function itemChange() {
        var id = $(this).children(':selected').attr('id');
        var item = findItem(id);
        if (item != null) {
            $(this).closest('tr').find('input[name="description"]').val(item.description);
            $(this).closest('tr').find('input[name="price"]').val(item.price);
        }
    }
    function findItem(id) {
        for (var i in items) {
            if (items[i].id == id) {
                return items[i];
            }
        }
        return null;
    }
</script>

<script src="//cdnjs.cloudflare.com/ajax/libs/moment.js/2.9.0/moment-with-locales.js"></script>
<script src="//cdn.rawgit.com/Eonasdan/bootstrap-datetimepicker/d004434a5ff76e7b97c8b07c01f34ca69e635d97/src/js/bootstrap-datetimepicker.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<h3>Add an invoice</h3>

<div class="col-sm-5 column">
    <div class="form-group">
	    <label for="<%= dropDownListCustomer.ClientID %>">Customer</label>
        <asp:DropDownList ID="dropDownListCustomer" runat="server" CssClass="form-control">
        </asp:DropDownList>
    </div>
</div>
<div class="col-sm-4 column"></div>
<div class="col-sm-3 column">
    <div class="form-group">
	    <label for="<%= textBoxDate.ClientID %>">Date</label>
        <div class='input-group date' id='invoice-date'>
            <asp:TextBox ID="textBoxDate" runat="server" CssClass="form-control"></asp:TextBox>
            <span class="input-group-addon">
                <span class="glyphicon glyphicon-calendar"></span>
            </span>
        </div>
    </div>
    <div class="form-group">
	    <label for="<%= textBoxDueDate.ClientID %>">Due Date</label>
        <div class='input-group date' id='invoice-due-date'>
            <asp:TextBox ID="textBoxDueDate" runat="server" CssClass="form-control"></asp:TextBox>
            <span class="input-group-addon">
                <span class="glyphicon glyphicon-calendar"></span>
            </span>
        </div>
    </div>
</div>
<br />

<table class="table table-hover" id="invoice-items">
    <tr>
        <th style="width:20%">Item</th>
        <th>Description</th>
        <th style="width:8%">Qty</th>
        <th style="width:10%">Price</th>
        <th style="width:10%">Amount</th>
        <th></th>
    </tr>
    <tr id="invoice-item-template">
        <td>
            <select id="items" class="form-control input input-small" name="item">
                <% foreach (var i in items) { %>
                <option id="<%= i.Id %>"><%= i.Name %></option>
                <% } %>
            </select>
        </td>
        <td class="description">
            <input name="description" type="text" class="form-control input input-small" />
        </td>
        <td>
            <input name="quantity" type="text" class="form-control input input-small" />
        </td>
        <td>
            <input name="price" type="text" class="form-control input input-small" />
        </td>
        <td class="amount"></td>
        <td>
            <a href="javascript:;" class="glyphicon glyphicon-remove">&nbsp;</a>
        </td>
    </tr>
</table>

<p class="text-right">
	Total Amount:
</p>
<h3 class="text-right">
	3000.00
</h3>

<p><%= HtmlHelper.Anchor("Add another item", "javascript:;", "id='add-invoice-item' class='glyphicon glyphicon-plus'")%></p>
<div>
    <asp:Button ID="buttonSave" runat="server" Text="Save invoice" 
        onclick="buttonSave_Click" CssClass="btn btn-success" />
        or <i><%= HtmlHelper.Anchor("cancel", "invoices.aspx") %></i>
</div>

</asp:Content>
