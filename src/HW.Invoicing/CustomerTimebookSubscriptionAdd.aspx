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
        function onDateChange() {
            //$('.spinner').show();
            var startDate = $('#<%= textBoxStartDate.ClientID %>').datepicker('getDate');
            var endDate = $('#<%= textBoxEndDate.ClientID %>').datepicker('getDate');
            
            var months = monthDiff(startDate, endDate);
            $('#<%= textBoxQuantity.ClientID %>').val($.number(months, 2, '.', ''));
            
            $('#<%= textBoxText.ClientID %>').change();

            $('.subscription-start-date').each(function (e) {
                var v = $(this).data('subscriptionstartdate');
                var i = $(this).data('subscriptioninitial');
                if (v != "") { // Has start date
                    //console.log('Has start date...');
                    if (i) {
                        //console.log('Has initial timebook already saved...');
                        var d = new Date(v);
                        //console.log('Timebook start date: ' + d.toString() + '. Actual start date: ' + startDate.toString());
                        if (d.setHours(0, 0, 0) <= startDate.setHours(0, 0, 0)) {
                            $(this).datepicker('update', startDate);
                        }
                    } else {
                        //console.log('No initial timebook saved...');
                        var d = new Date($(this).val());
                        if (d.setHours(0, 0, 0) <= startDate.setHours(0, 0, 0)) {
                            $(this).datepicker('update', startDate);
                        }
                    }
                } else {
                    //console.log('Has no start date, thus update it with the start date from main start date textbox.');
                    $(this).datepicker('update', startDate);
                }
            });

            $('.subscription-end-date').each(function (e) {
                var v = $(this).data('subscriptionenddate');
                if (v != "") { // Has end date
                    console.log('Has end date...');
                    var d = new Date(v);
                    console.log("Subscription end date: " + d);
                    if (d.setHours(0, 0, 0) < endDate.setHours(0, 0, 0)) {
                        $(this).datepicker('update', d);
                    }
                } else {
                    console.log('No end date, thus update it with the end date from main end date textbox.');
                    console.log('Actual end date: ' + endDate.toString());
                    $(this).datepicker('update', endDate);
                }
            });

//            $.ajax({
//                type: "POST",
//                url: "CustomerTimebookSubscriptionAdd.aspx/FindActiveSubscribersByCompany",
//                data: JSON.stringify({ companyId: <%= Session["CompanyId"] %>, startDate: $('#<%= textBoxStartDate.ClientID %>').val(), endDate: $('#<%= textBoxEndDate.ClientID %>').val() }),
//                contentType: "application/json; charset=utf-8",
//                dataType: "json",
//                success: function (data) {
//                    $('#customer-list-body').empty();
//                    var customerList = '';
//                    var customers = data.d;
//                    $.each(customers, function (i, c) {
//                        var strEndDate = '';
//                        if (c.subscriptionHasEndDate) {
//                            strEndDate = "<small>End: " + c.subscriptionEndDate + "</small><br>";
//                        }
//                        customerList += "" +
//                            "<tr" + c.subscriptionTimebookAvailability + ">" +
//                                "<td>" +
//                                "   <a href='customershow.aspx?Id=" + c.id + "&SelectedTab=timebook'>" + c.name + "</a><br>" +
//                                "   <small>Start: " + c.subscriptionStartDate + "</small><br>" +
//                                "   " + strEndDate +
//                                "   <small>Latest Timebook: " + c.subscriptionTimebookEndDateLabel + "</small>" +
//                                "   <input id='subscription-id'" +
//                                "       name='subscription-id'" +
//                                "       type='hidden' value='" + c.latestSubscriptionTimebookId + "'" +
//                                "       />" +
//                                "</td>" +
//                                "<td>" + c.subscriptionItem + "</td>" +
//                                "<td class='unit-width'>" + c.subscriptionItemUnit + "</td>" +
//                                "<td class='price-width'>" + c.subscriptionItemPrice + "</td>" +
//                                "<td class='date-width'>" +
//                                "   <input id='subscription-start-date'" +
//                                "       name='subscription-start-date'" +
//                                "       type='text'" +
//                                "       class='form-control subscription-start-date subscription-date'" +
//                                "       data-subscriptionstartdate=''" +
//                                "       data-customerid='" + c.id + "'" +
//                                "       value='" + c.latestSubscriptionTimebookStartDate + "'" +
//                                "       />" +
//                                "</td>" +
//                                "<td class='date-width'>" +
//                                "   <input id='subscription-end-date'" +
//                                "       name='subscription-end-date'" +
//                                "       type='text'" +
//                                "       class='form-control subscription-end-date subscription-date'" +
//                                "       data-customerid='" + c.Id + "'" +
//                                "       value='" + c.latestSubscriptionTimebookEndDate + "'" +
//                                "       />" +
//                                "</td>" +
//                                "<td class='quantity-width'>" +
//                                "   <input id='subscription-quantities'" +
//                                "       name='subscription-quantities'" +
//                                "       class='subscription-quantities form-control'" +
//                                "       type='text'" +
//                                "       value='" + c.latestSubscriptionTimebookQuantity + "'" +
//                                "       />" +
//                                "</td>" +
//                                "<td class='comments-width'>" +
//                                "   <textarea id='subscription-comments'" +
//                                "       name='subscription-comments'" +
//                                "       class='subscription-comments form-control'>" + c.comments + "</textarea>" +
//                                "</td>" +
//                            "</tr>";
//                    });
//                    $('#customer-list-body').html(customerList);
//                    init();
//                }
//            });
        }
    </script>
    <script type="text/javascript">
        $(document).ready(function () {
            init();
            $('.date').change(onDateChange);
            //var textGeneratedComments = $('#<%= textBoxComments.ClientID %>');
            $('#<%= textBoxText.ClientID %>').change(function () {
                var text = $(this).val();
                //var startDate = $('#<%= textBoxStartDate.ClientID %>').datepicker('getDate');
                //var endDate = $('#<%= textBoxEndDate.ClientID %>').datepicker('getDate');
                //var generatedText = text + ' ' + $('#<%= textBoxStartDate.ClientID %>').val().replace(/-/g, ".") + ' - ' + $('#<%= textBoxEndDate.ClientID %>').val().replace(/-/g, ".");
                //textGeneratedComments.val(generatedText);
                //$('#<%= textBoxComments.ClientID %>').change();
                $('.subscription-start-date').each(function (e) {
                    /*var v = $(this).data('subscriptionstartdate');
                    var d = new Date(v);
                    if (startDate.setHours(0, 0, 0) < d.setHours(0, 0, 0)) {
                    $(this).datepicker('update', startDate);
                    }*/
                    changeText($(this), text);
                });
            });
            //$('#<%= textBoxComments.ClientID %>').change(function () {
            //    $('.subscription-comments').val($(this).val());
            //});
            $('#<%= textBoxQuantity.ClientID %>').change(function () {
                console.log('Quantity changed...');
                $('.subscription-quantities').val($(this).val());
                var quantity = $(this).val();
                var startDate = $('#<%= textBoxStartDate.ClientID %>').datepicker('getDate');
                var d = addMonth(startDate, quantity);
                console.log('Anticipated date: ' + d);
                $('#<%= textBoxEndDate.ClientID %>').datepicker('update', d);

                $('.subscription-quantities').each(function () {
                    changeQuantity($(this), quantity);
                    /*var startDate = $(this).closest('tr').find('.subscription-start-date').datepicker('getDate');
                    var d = addMonth(startDate, $(this).val());
                    $(this).closest('tr').find('.subscription-end-date').datepicker('update', d);*/
                });
            });
        });
        function changeText(row, text) {
        	var textBoxStartDate = row.closest('tr').find('.subscription-start-date');
            var startDate = textBoxStartDate.datepicker('getDate');

            var textBoxEndDate = row.closest('tr').find('.subscription-end-date');
            var endDate = textBoxEndDate.datepicker('getDate');
            
            var months = monthDiff(startDate, endDate);
            row.closest('tr').find('.subscription-quantities').val($.number(months, 2, '.', ''));
            
            var comments = text + ' ' + textBoxStartDate.val().replace(/-/g, ".") + ' - ' + textBoxEndDate.val().replace(/-/g, ".");
            row.closest('tr').find('.subscription-comments').val(comments);
        }
        function changeQuantity(row, quantity) {
        	var startDate = row.closest('tr').find('.subscription-start-date').datepicker('getDate');
            var d = addMonth(startDate, quantity);
            row.closest('tr').find('.subscription-end-date').datepicker('update', d);
        }
        function init() {
            $('.date, .subscription-start-date, .subscription-end-date').datepicker({
                format: "yyyy-mm-dd",
                autoclose: true
            });
            $('.spinner').hide();
            $('.subscription-date').change(function () {
                /*var textBoxStartDate = $(this).closest('tr').find('.subscription-start-date');
                var startDate = textBoxStartDate.datepicker('getDate');*/

                //var customerSubscriptionStartDateTime = textBoxStartDate.data('subscriptionstartdate');
                //var customerSubscriptionStartDate = new Date(customerSubscriptionStartDateTime);
                //customerSubscriptionStartDate.setHours(0, 0, 0, 0);

                //var customerId = textBoxStartDate.data('customerid');

                /*var textBoxEndDate = $(this).closest('tr').find('.subscription-end-date');
                var endDate = textBoxEndDate.datepicker('getDate');
                
                var months = monthDiff(startDate, endDate);
                $(this).closest('tr').find('.subscription-quantities').val($.number(months, 2, '.', ''));
                
                var comments = $('#<%= textBoxText.ClientID %>').val();
                comments = comments + ' ' + textBoxStartDate.val().replace(/-/g, ".") + ' - ' + textBoxEndDate.val().replace(/-/g, ".");
                $(this).closest('tr').find('.subscription-comments').val(comments);*/
                changeText($(this), $('#<%= textBoxText.ClientID %>').val());
            });
            $('.subscription-quantities').change(function () {
            	changeQuantity($(this), $(this).val());
                /*var startDate = $(this).closest('tr').find('.subscription-start-date').datepicker('getDate');
                var d = addMonth(startDate, $(this).val());
                $(this).closest('tr').find('.subscription-end-date').datepicker('update', d);*/
            });
        }
    </script>
    <style type="text/css">
        .label-hastimebook {
            background:#f2dede;
            color:#333;
        }
        .label-notimebook {
            background:white;
            color:#333;
        }
        .date-width {
            width:120px;
        }
        .quantity-width {
            width:80px;
        }
        .comments-width {
            width:300px;
        }
        .unit-width {
            width:60px;
        }
        .price-width {
            width:50px;
        }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <h3>Subscription Timebooks</h3>

    <asp:Panel ID="Panel1" DefaultButton="buttonSave" runat="server">

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
            <td><img alt="" class="spinner" src="img/spiffygif_30x30.gif" /></td>
        </tr>
    </table>
    <div class="form-group">
	    <label for="<%= textBoxText.ClientID %>">Text</label>
        <asp:TextBox ID="textBoxText" runat="server" CssClass="form-control"></asp:TextBox>
    </div>
    <!--<div class="form-group">
	    <label for="<%= textBoxComments.ClientID %>">Generated Comments</label>
        <asp:TextBox ID="textBoxComments" runat="server" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
    </div>-->

    <br />

    <div>
        <asp:Button CssClass="btn btn-success" ID="buttonSave" runat="server" Text="Save these subscription timebooks" 
            onclick="buttonSave_Click" />
            or go to <i><%= HtmlHelper.Anchor("customer list", "customers.aspx") %></i>
    </div>

    </asp:Panel>

    <br />
    <table class="table table-hover" id="cusotmer-list">
        <thead>
            <tr>
                <th>Customer</th>
                <th>Subscription Item</th>
                <th class='unit-width'>Unit</th>
                <th class='price-width'>Price</th>
                <%--<th class='date-width'>Start Date</th>
                <th class='date-width'>End Date</th>--%>
                <th class='date-width'>From</th>
                <th class='date-width'>To</th>
                <th class='quantity-width'>Qty</th>
                <th class='comments-width'>Comments</th>
            </tr>
        </thead>
        <tbody id="customer-list-body">
            <% foreach (var c in customers) { %>
            <tr>
                <td>
                    <%= HtmlHelper.Anchor(c.Name, "customershow.aspx?Id=" + c.Id + "&SelectedTab=timebook") %><br />
                    <small>Start: <%= c.GetSubscriptionStartDate() %></small><br />
                    <% if (c.SubscriptionHasEndDate) { %>
                        <small>End: <%= c.GetSubscriptionEndDate() %></small><br />
                    <% } %>
                    <small>Latest Timebook: <%= c.GetLatestSubscriptionTimebookEndDateLabel() %></small>
                    <input id="subscription-id"
                        name="subscription-id"
                        type="hidden" value="<%= c.GetLatestSubscriptionTimebookId() %>" />
                </td>
                <td><%= c.SubscriptionItem.Name %></td>
                <td class='unit-width'><%= c.SubscriptionItem.Unit.Name %></td>
                <td class='price-width'><%= c.SubscriptionItem.Price.ToString("### ### ##0,00") %></td>
                <% DateTime sDate = c.GetLatestSubscriptionTimebookStartDate(startDate); %>
                <td class='date-width'>
                    <input id="subscription-start-date"
                        name="subscription-start-date"
                        type="text"
                        class="form-control subscription-start-date subscription-date"
                        data-subscriptionstartdate="<%= c.SubscriptionStartDate.Value.ToString("yyyy-MM-dd") %>"
                        <%--data-subscriptioninitial='<%= c.IsInitial(sDate) ? "true" : "false" %>'--%>
                        data-subscriptioninitial='<%= c.IsInitial(startDate) ? "true" : "false" %>'
                        data-customerid="<%= c.Id %>"
                        value="<%= sDate.ToString("yyyy-MM-dd") %>"
                        />
                </td>
                <% DateTime eDate = c.GetLatestSubscriptionTimebookEndDate(sDate, endDate); %>
                <td class='date-width'>
                    <input id="subscription-end-date"
                        name="subscription-end-date"
                        type="text"
                        class="form-control subscription-end-date subscription-date"
                        data-subscriptionenddate="<%= c.GetSubscriptionEndDateString() %>"
                        data-customerid="<%= c.Id %>"
                        value="<%= eDate.ToString("yyyy-MM-dd") %>"
                        />
                </td>
                <% decimal quantity = c.GetLatestSubscriptionTimebookQuantity(sDate, eDate); %>
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
        </tbody>
    </table>

    <%--<h3>Subscription Timebooks</h3>

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
            <td><img alt="" class="spinner" src="img/spiffygif_30x30.gif" /></td>
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
    <table class="table table-hover" id="cusotmer-list">
        <thead>
            <tr>
                <th>Customer</th>
                <th>Subscription Item</th>
                <th class='unit-width'>Unit</th>
                <th class='price-width'>Price</th>
                <th class='date-width'>Start Date</th>
                <th class='date-width'>End Date</th>
                <th class='quantity-width'>Qty</th>
                <th class='comments-width'>Comments</th>
            </tr>
        </thead>
        <tbody id="customer-list-body">
        <% foreach (var c in customers) { %>
            <!--<% int subscriptionId = c.GetLatestSubscriptionTimebookId(); %>-->
            <tr<%= c.GetSubscriptionTimebookAvailability(startDate) %>>
                <td>
                    <%= HtmlHelper.Anchor(c.Name, "customershow.aspx?Id=" + c.Id + "&SelectedTab=timebook") %><br />
                    <small>Start: <%= c.GetSubscriptionStartDate() %></small><br />
                    <% if (c.SubscriptionHasEndDate) { %>
                        <small>End: <%= c.GetSubscriptionEndDate() %></small><br />
                    <% } %>
                    <small>Latest Timebook: <%= c.GetLatestSubscriptionTimebookEndDateLabel() %></small>
                    <input id="subscription-id"
                        name="subscription-id"
                        type="hidden" value="<%= c.GetLatestSubscriptionTimebookId() %>" />
                </td>
                <td><%= c.SubscriptionItem.Name %></td>
                <td class='unit-width'><%= c.SubscriptionItem.Unit.Name %></td>
                <td class='price-width'><%= c.SubscriptionItem.Price.ToString("### ### ##0,00") %></td>
                <% DateTime sDate = c.GetLatestSubscriptionTimebookStartDate(startDate); %>
                <td class='date-width'>
                    <input id="subscription-start-date"
                        name="subscription-start-date"
                        type="text"
                        class="form-control subscription-start-date subscription-date"
                        data-subscriptionstartdate="<%= c.SubscriptionStartDate.Value.ToString("yyyy-MM-dd") %>"
                        data-customerid="<%= c.Id %>"
                        data-subscriptioninitial='<%= c.IsInitial(sDate) ? "true" : "false" %>'
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
                <% decimal quantity = c.GetLatestSubscriptionTimebookQuantity(sDate, eDate); %>
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
        </tbody>
    </table>

    <br />

    <div>
        <asp:Button CssClass="btn btn-success" ID="buttonSave" runat="server" Text="Save these subscription timebooks" 
            onclick="buttonSave_Click" />
            or go to <i><%= HtmlHelper.Anchor("customer list", "customers.aspx") %></i>
    </div>

    <br /><br /><br />
    <strong>Legends:</strong>
    <ul>
        <li><span class="label label-hastimebook">Row colored red</span> - customer has created a timebook.</li>
        <li><span class="label label-notimebook">Row colored white</span> - customer has either invoiced the timebook or ready to create a timebook.</li>
    </ul>--%>

</asp:Content>
