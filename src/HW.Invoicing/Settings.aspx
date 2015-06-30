<%@ Page Title="" Language="C#" MasterPageFile="~/Invoicing.Master" AutoEventWireup="true" CodeBehind="Settings.aspx.cs" Inherits="HW.Invoicing.Settings" %>
<%@ Import Namespace="HW.Core.Helpers" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

<link href="css/bootstrap-datepicker.min.css" rel="stylesheet" type="text/css" />
    <script src="js/bootstrap-datepicker.min.js" type="text/javascript"></script>

<script type="text/javascript">
    $(function () {
        $('#<%= textBoxFinancialMonthStart.ClientID %>').datepicker({
            format: "MM dd",
            autoclose: true
        });
        $('#<%= textBoxFinancialMonthEnd.ClientID %>').datepicker({
            format: "MM dd",
            autoclose: true
        });
    });
    
</script>


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<h3>Settings</h3>
<div class="form-group">
	<label for="<%= textBoxName.ClientID %>">Company Name</label>
    <asp:TextBox ID="textBoxName" runat="server" CssClass="form-control"></asp:TextBox>
</div>
<div class="form-group">
	<label for="<%= textBoxAddress.ClientID %>">Address</label>
    <asp:TextBox ID="textBoxAddress" runat="server" CssClass="form-control"></asp:TextBox>
</div>
<div class="form-group">
	<label for="<%= textBoxPhone.ClientID %>">Phone</label>
    <asp:TextBox ID="textBoxPhone" runat="server" CssClass="form-control"></asp:TextBox>
</div>
<div class="form-group">
	<label for="<%= textBoxBankAccountNumber.ClientID %>">Bank Account</label>
    <asp:TextBox ID="textBoxBankAccountNumber" runat="server" CssClass="form-control"></asp:TextBox>
</div>
<div class="form-group">
	<label for="<%= textBoxTIN.ClientID %>">TIN</label>
    <asp:TextBox ID="textBoxTIN" runat="server" CssClass="form-control"></asp:TextBox>
</div>
<div class="form-group">
	<label for="<%= textBoxFinancialMonthStart.ClientID %>">Start Financial Month</label>
    <asp:TextBox ID="textBoxFinancialMonthStart" runat="server" CssClass="form-control"></asp:TextBox>
</div>
<div class="form-group">
	<label for="<%= textBoxFinancialMonthEnd.ClientID %>">End Financial Month</label>
    <asp:TextBox ID="textBoxFinancialMonthEnd" runat="server" CssClass="form-control"></asp:TextBox>
</div>
<div>
    <asp:Button ID="buttonSave" CssClass="btn btn-success" runat="server" Text="Save settings" 
        onclick="buttonSave_Click" />
        or <i><%= HtmlHelper.Anchor("cancel", "dashboard.aspx") %></i>
</div>

</asp:Content>
