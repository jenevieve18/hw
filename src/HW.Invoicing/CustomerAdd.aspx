<%@ Page Title="" Language="C#" MasterPageFile="~/Invoicing.Master" AutoEventWireup="true" CodeBehind="CustomerAdd.aspx.cs" Inherits="HW.Invoicing.CustomerAdd" %>
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
            var subscriptionPanel = $('#<%= panelCustomerSubscription.ClientID %>');
            subscriptionPanel.hide();
            $('#<%= checkBoxSubscribe.ClientID %>').change(function () {
                if ($(this).is(':checked')) {
                    subscriptionPanel.show();
                } else {
                    subscriptionPanel.hide();
                }
            });
            $('#<%= checkBoxSubscribe.ClientID %>').change();
            $('#<%= checkBoxSubscriptionHasEndDate.ClientID %>').change(function () {
                if ($(this).is(':checked')) {
                    $('#subscription-end-date').show();
                } else {
                    $('#subscription-end-date').hide();
                }
            });
            $('#<%= checkBoxSubscriptionHasEndDate.ClientID %>').change();
        });
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<h3>Add a customer</h3>
<% if (message != null && message != "") { %>
<div class="alert alert-warning">
    <%= message %>
</div>
<% } %>
<div class="form-group">
	<label for="<%= textBoxName.ClientID %>">Customer Number</label>
    <asp:TextBox ID="textBoxNumber" runat="server" CssClass="form-control"></asp:TextBox>
</div>
<div class="form-group">
	<label for="<%= textBoxName.ClientID %>">Customer Name</label>
    <asp:TextBox ID="textBoxName" runat="server" CssClass="form-control"></asp:TextBox>
</div>
<div class="form-group">
	<label for="<%= textBoxPostalAddress.ClientID %>">Postal Address</label>
    <asp:TextBox ID="textBoxPostalAddress" TextMode="MultiLine" runat="server" CssClass="form-control"></asp:TextBox>
</div>
<div class="form-group">
	<label for="<%= textBoxInvoiceAddress.ClientID %>">Invoicing Address</label>
    <asp:TextBox ID="textBoxInvoiceAddress" TextMode="MultiLine" runat="server" CssClass="form-control"></asp:TextBox>
</div>
<%--<div class="form-group">
	<label for="<%= textBoxPurchaseOrderNumber.ClientID %>">Reference / Purchase Order Number</label>
    <asp:TextBox ID="textBoxPurchaseOrderNumber" runat="server" CssClass="form-control"></asp:TextBox>
</div>
<div class="form-group">
	<label for="<%= textBoxYourReferencePerson.ClientID %>">Your Reference Person</label>
    <asp:TextBox ID="textBoxYourReferencePerson" runat="server" CssClass="form-control"></asp:TextBox>
</div>--%>
<div class="form-group">
	<label for="<%= textBoxInvoiceEmail.ClientID %>">Invoicing Email</label>
    <asp:TextBox ID="textBoxInvoiceEmail" runat="server" CssClass="form-control"></asp:TextBox>
</div>
<div class="form-group">
	<label for="<%= textBoxInvoiceEmailCC.ClientID %>">Invoicing Email CC</label>
    <asp:TextBox ID="textBoxInvoiceEmailCC" runat="server" CssClass="form-control"></asp:TextBox>
</div>
<div class="form-group">
	<label for="<%= textBoxOurReferencePerson.ClientID %>">Our Reference Person</label>
    <asp:TextBox ID="textBoxOurReferencePerson" runat="server" CssClass="form-control"></asp:TextBox>
</div>
<div class="form-group">
	<label for="<%= textBoxPhone.ClientID %>">Phone</label>
    <asp:TextBox ID="textBoxPhone" runat="server" CssClass="form-control"></asp:TextBox>
</div>
<div class="form-group">
	<label for="<%= textBoxEmail.ClientID %>">Email</label>
    <asp:TextBox ID="textBoxEmail" runat="server" CssClass="form-control"></asp:TextBox>
</div>
<div class="form-group">
	<label for="<%= dropDownListLanguage.ClientID %>">Language</label>
    <asp:DropDownList ID="dropDownListLanguage" runat="server" CssClass="info form-control">
    </asp:DropDownList>
</div>
<div class="form-group">
	<label for="<%= dropDownListCurrency.ClientID %>">Currency</label>
    <asp:DropDownList ID="dropDownListCurrency" runat="server" CssClass="info form-control">
    </asp:DropDownList>
</div>
<% if (company.HasSubscriber) { %>
<div class="form-group">
    <asp:CheckBox ID="checkBoxSubscribe" runat="server" CssClass="form-control" Text="&nbsp;This customer has subscription" />
</div>
<asp:Panel runat="server" DefaultButton="buttonSave" ID="panelCustomerSubscription">
    <div class="form-group">
	    <label for="<%= dropDownListSubscriptionItem.ClientID %>">Subscription Item</label>
        <asp:DropDownList ID="dropDownListSubscriptionItem" runat="server" CssClass="info form-control">
                            </asp:DropDownList>
    </div>
    <div class="form-group">
	    <label for="<%= textBoxSubscriptionStartDate.ClientID %>">Start Date</label>
        <asp:TextBox CssClass="form-control date" ID="textBoxSubscriptionStartDate" runat="server"></asp:TextBox>
    </div>
    <div class="form-group">
        <asp:CheckBox ID="checkBoxSubscriptionHasEndDate" runat="server" CssClass="form-control" Text="&nbsp;This subscription has end date" />
    </div>
    <div id="subscription-end-date" class="form-group">
	    <label for="<%= textBoxSubscriptionEndDate.ClientID %>">End Date</label>
        <asp:TextBox CssClass="date form-control" ID="textBoxSubscriptionEndDate" runat="server"></asp:TextBox>
    </div>
</asp:Panel>
<% } %>
<div>
    <asp:Button CssClass="btn btn-success" ID="buttonSave" runat="server" Text="Save customer" 
        onclick="buttonSave_Click" />
        or <i><%= HtmlHelper.Anchor("cancel", "customers.aspx") %></i>
</div>

</asp:Content>
