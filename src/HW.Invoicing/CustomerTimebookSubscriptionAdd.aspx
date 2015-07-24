<%@ Page Title="" Language="C#" MasterPageFile="~/Invoicing.Master" AutoEventWireup="true" CodeBehind="CustomerTimebookSubscriptionAdd.aspx.cs" Inherits="HW.Invoicing.CustomerTimebookSubscriptionAdd" %>
<%@ Import Namespace="HW.Core.Helpers" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="css/bootstrap-datepicker.min.css" rel="stylesheet" type="text/css" />
    <script src="js/bootstrap-datepicker.min.js" type="text/javascript"></script>
    <script src="js/jquery.number.min.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $('.date, .subscription-start-date, .subscription-end-date').datepicker({
                format: "yyyy-mm-dd",
                autoclose: true
            });
            $('.date').change(function () {
                var startDate = $('#<%= textBoxStartDate.ClientID %>').datepicker('getDate');
                var endDate = $('#<%= textBoxEndDate.ClientID %>').datepicker('getDate');
                var months = monthDiff(startDate, endDate);
                $('#<%= textBoxQuantity.ClientID %>').val($.number(months, 2, '.', ''));
                $('.subscription-quantities').val($.number(months, 2, '.', ''));
                $('#<%= textBoxText.ClientID %>').change();

                $('.subscription-start-date').each(function () {
                    $(this).datepicker('update', $('#<%= textBoxStartDate.ClientID %>').datepicker('getDate'));
                });
                $('.subscription-end-date').each(function () {
                    $(this).datepicker('update', $('#<%= textBoxEndDate.ClientID %>').datepicker('getDate'));
                });
            });
            $('.date').change();
            $('.subscription-date').change(function () {
                //var hiddenSubscriptionStartDate = $(this).closest('tr').find('.customer-subscription-start-date');
                //console.log(hiddenSubscriptionStartDate.val());
                var textBoxStartDate = $(this).closest('tr').find('.subscription-start-date');
                var startDate = textBoxStartDate.datepicker('getDate');

                var customerSubscriptionStartDate = textBoxStartDate.data('subscriptionstartdate');
                var d = new Date(customerSubscriptionStartDate);
                d.setHours(0, 0, 0, 0);
                console.log(startDate);
                console.log(d);
                if (startDate < d) {
                    //console.log("whoa!");
                    $(this).closest('tr').addClass('danger');
                    //alert("Subscription start date you selected is lesser than the customer's subscription start date.");
                } else {
                    $(this).closest('tr').removeClass('danger');
                    var textBoxEndDate = $(this).closest('tr').find('.subscription-end-date');
                    var endDate = textBoxEndDate.datepicker('getDate');
                    var months = monthDiff(startDate, endDate);
                    $(this).closest('tr').find('.subscription-quantities').val($.number(months, 2, '.', ''));
                    var comments = $('#<%= textBoxText.ClientID %>').val();
                    comments = comments + ' ' + textBoxStartDate.val().replace(/-/g, ".") + ' - ' + textBoxEndDate.val().replace(/-/g, ".");
                    $(this).closest('tr').find('.subscription-comments').val(comments);
                }
            });

            var textGeneratedComments = $('#<%= textBoxComments.ClientID %>');
            $('#<%= textBoxText.ClientID %>').change(function () {
                var text = $(this).val();
                var startDate = $('#<%= textBoxStartDate.ClientID %>').datepicker('getDate');
                var endDate = $('#<%= textBoxEndDate.ClientID %>').datepicker('getDate');
                var generatedText = text + ' ' + $('#<%= textBoxStartDate.ClientID %>').val().replace(/-/g, ".") + ' - ' + $('#<%= textBoxEndDate.ClientID %>').val().replace(/-/g, ".");
                textGeneratedComments.val(generatedText);
                $('#<%= textBoxComments.ClientID %>').change();
            });
            $('#<%= textBoxComments.ClientID %>').change(function () {
                $('.subscription-comments').val($(this).val());
            });
            $('#<%= textBoxQuantity.ClientID %>').change(function () {
                $('.subscription-quantities').val($(this).val());
                var startDate = $('#<%= textBoxStartDate.ClientID %>').datepicker('getDate');
                var d = addMonth(startDate, $(this).val());
                $('#<%= textBoxEndDate.ClientID %>').datepicker('update', d);
            });
            $('#<%= textBoxText.ClientID %>').change();

            $('.subscription-quantities').change(function () {
                var startDate = $(this).closest('tr').find('.subscription-start-date').datepicker('getDate');
                var d = addMonth(startDate, $(this).val());
                $(this).closest('tr').find('.subscription-end-date').datepicker('update', d);
            });
        });
    </script>
    <style type="text/css">
        .date-width 
        {
            width:100px;
        }
        .quantity-width 
        {
            width:60px;
        }
        .comments-width 
        {
            width:300px;
        }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<h3>Subscription Timebooks</h3>

<% if (message  != null) { %>
    <%= message %>
<% } %>

<div class="alert alert-info">
	<strong>Subscription</strong> is the action of making or agreeing to make an advance payment in order to receive or participate in something.
</div>

<table>
    <tr>
        <td>
            <div class="form-group">
	            <label for="<%= textBoxStartDate.ClientID %>">Start Date</label>
                <asp:TextBox ID="textBoxStartDate" runat="server" CssClass="date form-control date-width"></asp:TextBox>
            </div>
        </td>
        <td>
            <div class="form-group">
	            <label for="<%= textBoxEndDate.ClientID %>">End Date</label>
                <asp:TextBox ID="textBoxEndDate" runat="server" CssClass="date form-control date-width"></asp:TextBox>
            </div>
        </td>
        <td>
            <div class="form-group">
	            <label for="<%= textBoxQuantity.ClientID %>">Quantity</label>
                <asp:TextBox ID="textBoxQuantity" runat="server" CssClass="form-control quantity-width"></asp:TextBox>
            </div>
        </td>
    </tr>
</table>
<div class="form-group">
	<label for="<%= textBoxText.ClientID %>">Text</label>
    <asp:TextBox ID="textBoxText" runat="server" CssClass="form-control"></asp:TextBox>
</div>
<div class="form-group">
	<label for="<%= textBoxComments.ClientID %>">Generated Comments</label>
    <asp:TextBox ID="textBoxComments" runat="server" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
</div>

<br />
<table class="table table-hover">
    <tr>
        <th>Customer</th>
        <th>Subscription Item</th>
        <th>Unit</th>
        <th>Price</th>
        <!--<th class="col-md-2">Start Date</th>
        <th class="col-md-2">End Date</th>
        <th class="col-md-1">Qty</th>
        <th class="col-md-4">Comments</th>-->
        <th class='date-width'>Start Date</th>
        <th class='date-width'>End Date</th>
        <th class='quantity-width'>Qty</th>
        <th class='comments-width'>Comments</th>
    </tr>
    <% foreach (var c in customers) { %>
    <tr>
        <td>
            <%= c.Name %><br />
            ( <%= c.SubscriptionStartDate.Value.ToString("yyyy-MM-dd") %> )
            <input type="hidden" class="customer-subscription-start-date" value="<%= c.SubscriptionStartDate.Value.ToString("yyyy-MM-dd") %>" />
        </td>
        <td><%= c.SubscriptionItem.Name %></td>
        <td><%= c.SubscriptionItem.Unit.Name %></td>
        <td><%= c.SubscriptionItem.Price.ToString("### ### ##0.00") %></td>
        <!--<td class="col-md-2">-->
        <td class='date-width'>
            <input id="subscription-start-date" name="subscription-start-date" type="text" class="form-control subscription-start-date subscription-date"
                data-subscriptionstartdate="<%= c.SubscriptionStartDate.Value.ToString("yyyy-MM-dd") %>"/>
        </td>
        <!--<td class="col-md-2">-->
        <td class='date-width'>
            <input id="subscription-end-date" name="subscription-end-date" type="text" class="form-control subscription-end-date subscription-date" />
        </td>
        <!--<td class="col-md-1">-->
        <td class='quantity-width'>
            <input id="subscription-quantities" name="subscription-quantities" class="subscription-quantities form-control" type="text" value="1" />
        </td>
        <!--<td class="col-md-4">-->
        <td class='comments-width'>
            <textarea id="subscription-comments" name="subscription-comments" class="subscription-comments form-control"></textarea>
        </td>
    </tr>
    <% } %>
</table>

<br />

<div>
    <asp:Button CssClass="btn btn-success" ID="buttonSave" runat="server" Text="Save these subscription timebooks" 
        onclick="buttonSave_Click" />
        or go to <i><%= HtmlHelper.Anchor("customer list", "customers.aspx") %></i>
</div>

</asp:Content>
