<%@ Page Title="" Language="C#" MasterPageFile="~/Invoicing.Master" AutoEventWireup="true" CodeBehind="CustomerTimebookSubscriptionAdd.aspx.cs" Inherits="HW.Invoicing.CustomerTimebookSubscriptionAdd" %>
<%@ Import Namespace="HW.Core.Helpers" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="css/bootstrap-datepicker.min.css" rel="stylesheet" type="text/css" />
    <script src="js/bootstrap-datepicker.min.js" type="text/javascript"></script>
    <script src="js/jquery.number.min.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $('.date').datepicker({
                format: "yyyy-mm-dd",
                autoclose: true
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
            $('#<%= textBoxQuantity.ClientID %>').change(function () {
                $('.subscription-quantity').val($(this).val());
            });
            $('#<%= textBoxComments.ClientID %>').change(function () {
                $('.subscription-comments').val($(this).val());
            });
        });
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<h3>Subscription Timebooks</h3>

<% if (message  != null) { %>
    <div class="alert alert-success">
	    <%= message %>
    </div>
<% } %>

<div class="alert alert-info">
	<strong>Subscription</strong> is the action of making or agreeing to make an advance payment in order to receive or participate in something.
</div>

<table>
    <tr>
        <td>
            <div class="form-group">
	            <label for="<%= textBoxStartDate.ClientID %>">Start Date</label>
                <asp:TextBox ID="textBoxStartDate" runat="server" CssClass="date form-control"></asp:TextBox>
            </div>
        </td>
        <td>
            <div class="form-group">
	            <label for="<%= textBoxEndDate.ClientID %>">End Date</label>
                <asp:TextBox ID="textBoxEndDate" runat="server" CssClass="date form-control"></asp:TextBox>
            </div>
        </td>
        <td>
            <div class="form-group">
	            <label for="<%= textBoxQuantity.ClientID %>">Quantity</label>
                <asp:TextBox ID="textBoxQuantity" runat="server" CssClass="form-control"></asp:TextBox>
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
        <th class="col-md-1">Qty</th>
        <th class="col-md-4">Comments</th>
    </tr>
    <% foreach (var c in customers) { %>
    <tr>
        <td><%= c.Name %></td>
        <td><%= c.SubscriptionItem.Name %></td>
        <td><%= c.SubscriptionItem.Unit.Name %></td>
        <td><%= c.SubscriptionItem.Price.ToString("### ### ##0.00") %></td>
        <td class="col-md-1">
            <input id="subscription-quantities" name="subscription-quantities" class="subscription-quantity form-control" type="text" value="1" />
        </td>
        <td class="col-md-4">
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
