<%@ Page Title="" Language="C#" MasterPageFile="~/Invoicing.Master" AutoEventWireup="true" CodeBehind="CreateSubscriptionTimebooks.aspx.cs" Inherits="HW.Invoicing.CreateSubscriptionTimebooks" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">


<link href="css/bootstrap-datepicker.min.css" rel="stylesheet" type="text/css" />
    <script src="js/bootstrap-datepicker.min.js" type="text/javascript"></script>
    <script src="js/jquery.number.min.js" type="text/javascript"></script>
    <script type="text/javascript">
        var month = new Array();
        month[0] = "January";
        month[1] = "February";
        month[2] = "March";
        month[3] = "April";
        month[4] = "May";
        month[5] = "June";
        month[6] = "July";
        month[7] = "August";
        month[8] = "September";
        month[9] = "October";
        month[10] = "November";
        month[11] = "December";
        $(document).ready(function () {
            /*$('#<%= textBoxStartDate.ClientID %>').datepicker({
            format: "yyyy-mm-dd",
            autoclose: true
            });*/
            $('.date').datepicker({
                format: "yyyy-mm-dd",
                autoclose: true
            });
            var textGeneratedComments = $('#<%= textBoxComments.ClientID %>');
            $('#<%= textBoxText.ClientID %>').change(function () {
                var text = $(this).val();
                var startDate = $('#<%= textBoxStartDate.ClientID %>').datepicker('getDate');
                var endDate = $('#<%= textBoxEndDate.ClientID %>').datepicker('getDate');
                var generatedText = text + ' ' + $('#<%= textBoxStartDate.ClientID %>').val() + ' - ' + $('#<%= textBoxEndDate.ClientID %>').val();
                textGeneratedComments.val(generatedText);
            });
            $('#<%= textBoxQuantity.ClientID %>').change(function () {
                $('.subscription-quantity').val($(this).val());
            });
        });
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<h3>Create Timebooks for Subscribed Customers</h3>

<div class="alert alert-info">
	<strong>Subscription</strong> is the action of making or agreeing to make an advance payment in order to receive or participate in something.
</div>

<div class="form-group">
	<label for="<%= textBoxStartDate.ClientID %>">Start Date</label>
    <asp:TextBox ID="textBoxStartDate" runat="server" CssClass="date form-control"></asp:TextBox>
</div>
<div class="form-group">
	<label for="<%= textBoxEndDate.ClientID %>">End Date</label>
    <asp:TextBox ID="textBoxEndDate" runat="server" CssClass="date form-control"></asp:TextBox>
</div>
<div class="form-group">
	<label for="<%= textBoxText.ClientID %>">Text</label>
    <asp:TextBox ID="textBoxText" runat="server" CssClass="form-control"></asp:TextBox>
</div>
<div class="form-group">
	<label for="<%= textBoxQuantity.ClientID %>">Quantity</label>
    <asp:TextBox ID="textBoxQuantity" runat="server" CssClass="form-control"></asp:TextBox>
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
        <th>Qty</th>
        <th>Comments</th>
    </tr>
    <% foreach (var c in customers) { %>
    <tr>
        <td><%= c.Name %></td>
        <td><%= c.SubscriptionItem.Name %></td>
        <td><%= c.SubscriptionItem.Unit.Name %></td>
        <td><%= c.SubscriptionItem.Price.ToString("### ### ##0.00") %></td>
        <td>
            <input class="subscription-quantity form-control" type="text" />
        </td>
        <td>
            <textarea class="subscription-comments form-control"></textarea>
        </td>
    </tr>
    <% } %>
</table>

</asp:Content>
