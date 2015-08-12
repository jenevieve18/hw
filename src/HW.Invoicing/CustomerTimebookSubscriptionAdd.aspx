<%@ Page Title="" Language="C#" MasterPageFile="~/Invoicing.Master" AutoEventWireup="true" CodeBehind="CustomerTimebookSubscriptionAdd.aspx.cs" Inherits="HW.Invoicing.CustomerTimebookSubscriptionAdd" %>
<%@ Import Namespace="HW.Core.Helpers" %>
<%@ Import Namespace="HW.Invoicing.Core.Models" %>
<%@ Import Namespace="System.Globalization" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="css/bootstrap-datepicker.min.css" rel="stylesheet" type="text/css" />
    <script src="js/bootstrap-datepicker.min.js" type="text/javascript"></script>
    <script src="js/jquery.number.min.js" type="text/javascript"></script>
    <script type="text/javascript">
        var customers = [];
        <% foreach (var c in customers) { %>
            var c = {
                'id': <%= c.Id %>,
                'startDate': '<%= c.SubscriptionStartDate.Value.ToString("yyyy-MM-dd") %>'
            };
            c.timebooks = [];
            <% foreach (var t in c.SubscriptionTimebooks) { %>
                var t = {
                    'startDate': '<%= t.SubscriptionStartDate.Value.ToString("yyyy-MM-dd") %>'
                };
                c.timebooks.push(t);
            <% } %>
            customers.push(c);
        <% } %>
        function inTimebook(customerId, startDate) {
            var found = false;
            $.each(customers, function(i, c) {
                if (c['id'] == customerId) {
                    $.each(c.timebooks, function(j, t) {
                        var d = new Date(t['startDate']);
                        d.setHours(0, 0, 0);
                        if (d.getTime() == startDate.getTime()) {
                            found = true;
                        }
                    });
                }
            });
            return found;
        }
    </script>
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
                $('#<%= textBoxText.ClientID %>').change();

                $('.subscription-quantities').val($.number(months, 2, '.', ''));
                $('.subscription-start-date').each(function () {
                    $(this).datepicker('update', startDate);
                });
                $('.subscription-end-date').each(function () {
                    $(this).datepicker('update', endDate);
                });
            });
            //$('.date').change();
            $('.subscription-date').change(function () {
                var textBoxStartDate = $(this).closest('tr').find('.subscription-start-date');
                var startDate = textBoxStartDate.datepicker('getDate');

                var customerSubscriptionStartDateTime = textBoxStartDate.data('subscriptionstartdate');
                var customerSubscriptionStartDate = new Date(customerSubscriptionStartDateTime);
                customerSubscriptionStartDate.setHours(0, 0, 0, 0);

                var customerId = textBoxStartDate.data('customerid');

                if (startDate < customerSubscriptionStartDate || inTimebook(customerId, startDate)) {
                    $(this).closest('tr').addClass('danger');
                    //textBoxStartDate.datepicker('update', startDate);
                } else {
                    $(this).closest('tr').removeClass('danger');
                    $(this).closest('tr').removeClass('warning');
                }
                var textBoxEndDate = $(this).closest('tr').find('.subscription-end-date');
                var endDate = textBoxEndDate.datepicker('getDate');
                var months = monthDiff(startDate, endDate);
                $(this).closest('tr').find('.subscription-quantities').val($.number(months, 2, '.', ''));
                var comments = $('#<%= textBoxText.ClientID %>').val();
                comments = comments + ' ' + textBoxStartDate.val().replace(/-/g, ".") + ' - ' + textBoxEndDate.val().replace(/-/g, ".");
                $(this).closest('tr').find('.subscription-comments').val(comments);
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
            //$('#<%= textBoxText.ClientID %>').change();

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

<div class='alert alert-warning'>
    <h4>Testing Purposes Only</h4>
    <p>This will delete all subscription timebooks. This will be removed on production.</p>
    <p><asp:Button ID="buttonClear" runat="server" Text="Click here!" CssClass="btn btn-warning" OnClick="buttonClear_Click" /></p>
</div>

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
        <th class='date-width'>Start Date</th>
        <th class='date-width'>End Date</th>
        <th class='quantity-width'>Qty</th>
        <th class='comments-width'>Comments</th>
    </tr>
    <% foreach (var c in customers) { %>
        <% int subscriptionId = c.GetLatestSubscriptionTimebookId(); %>
        <tr<%= c.GetSubscriptionTimebookAvailability(startDate) %>>
            <td>
                <%= HtmlHelper.Anchor(c.Name, "customershow.aspx?Id=" + c.Id + "&SelectedTab=timebook") %><br />
                <small>
                    (<%= c.SubscriptionStartDate.Value.ToString("yyyy.MM.dd") %><%= c.SubscriptionHasEndDate ? " - " + c.SubscriptionEndDate.Value.ToString("yyyy.MM.dd") : "" %>)
                </small>
                <input id="subscription-id"
                    name="subscription-id"
                    type="hidden" value="<%= c.GetLatestSubscriptionTimebookId() %>" />
                <%= c.GetLatestSubscriptionTimebookEndDate() %>
            </td>
            <td><%= c.SubscriptionItem.Name %></td>
            <td><%= c.SubscriptionItem.Unit.Name %></td>
            <td><%= c.SubscriptionItem.Price.ToString("### ### ##0,00") %></td>
            <% DateTime sDate = c.GetLatestSubscriptionTimebookStartDate(startDate); %>
            <td class='date-width'>
                <input id="subscription-start-date"
                    name="subscription-start-date"
                    type="text"
                    class="form-control subscription-start-date subscription-date"
                    data-subscriptionstartdate="<%= c.SubscriptionStartDate.Value.ToString("yyyy-MM-dd") %>"
                    data-customerid="<%= c.Id %>"
                    value="<%= sDate.ToString("yyyy-MM-dd") %>"
                    />
            </td>
            <% DateTime eDate = c.GetLatestSubscriptionTimebookEndDate(endDate); %>
            <td class='date-width'>
                <input id="subscription-end-date"
                    name="subscription-end-date"
                    type="text"
                    class="form-control subscription-end-date subscription-date"
                    data-customerid="<%= c.Id %>"
                    value="<%= eDate.ToString("yyyy-MM-dd") %>"
                    />
            </td>
            <% decimal quantity = c.GetLatestSubscriptionTimebookQuantity(sDate); %>
            <td class='quantity-width'>
                <input id="subscription-quantities"
                    name="subscription-quantities"
                    class="subscription-quantities form-control"
                    type="text"
                    value="<%= quantity.ToString("0.00", new CultureInfo("en-US")) %>"
                    />
            </td>
            <% string comments = c.GetLatestSubscriptionTimebookComments(sDate, eDate, generatedComments); %>
            <td class='comments-width'>
                <textarea id="subscription-comments"
                    name="subscription-comments"
                    class="subscription-comments form-control"><%= comments %></textarea>
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
