<%@ Page Title="" Language="C#" MasterPageFile="~/Invoicing.Master" AutoEventWireup="true" CodeBehind="CompanyAdd.aspx.cs" Inherits="HW.Invoicing.CompanyAdd" %>
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
        });
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<h3>Add a company</h3>

<div class="form-group">
	<label for="<%= textBoxName.ClientID %>">Company Name</label>
    <asp:TextBox ID="textBoxName" runat="server" CssClass="form-control"></asp:TextBox>
</div>
<div class="form-group">
	<label for="<%= textBoxAddress.ClientID %>">Address</label>
    <asp:TextBox ID="textBoxAddress" runat="server" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
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
    <asp:TextBox ID="textBoxFinancialMonthStart" runat="server" CssClass="date form-control"></asp:TextBox>
</div>
<div class="form-group">
	<label for="<%= textBoxFinancialMonthEnd.ClientID %>">End Financial Month</label>
    <asp:TextBox ID="textBoxFinancialMonthEnd" runat="server" CssClass="date form-control"></asp:TextBox>
</div>
<div class="form-group">
    <label for="<%= fileUploadInvoiceLogo.ClientID %>">Invoice Logo</label>
    <asp:FileUpload ID="fileUploadInvoiceLogo" runat="server" />
</div>
<div class="form-group">
    <asp:CheckBox ID="checkBoxHasSubscriber" runat="server" CssClass="form-control" Text="&nbsp;This company has subscribers." />
</div>
<div>
    <asp:Button ID="buttonSave" CssClass="btn btn-success" runat="server" Text="Save company info" 
        onclick="buttonSave_Click" />
        or <i><%= HtmlHelper.Anchor("cancel", "companies.aspx") %></i>
</div>

</asp:Content>
