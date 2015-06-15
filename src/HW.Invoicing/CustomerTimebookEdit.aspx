﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Invoicing.Master" AutoEventWireup="true" CodeBehind="CustomerTimebookEdit.aspx.cs" Inherits="HW.Invoicing.CustomerTimebookEdit" %>
<%@ Import Namespace="HW.Core.Helpers" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <link href="css/bootstrap-datepicker.min.css" rel="stylesheet" type="text/css" />
    <script src="js/bootstrap-datepicker.min.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $('#<%= dropDownListTimebookItems.ClientID %>').change(function () {
                var selected = $(this).find('option:selected');
                var selectedPrice = selected.data('price');
                var selectedUnit = selected.data('unit');
                $('#<%= textBoxTimebookPrice.ClientID %>').val(selectedPrice);
                $('#<%= labelTimebookUnit.ClientID %>').text(selectedUnit);
            });
            $('#<%= dropDownListTimebookItems.ClientID %>').change();

            $('#<%= textBoxTimebookDate.ClientID %>').datepicker({
                format: "yyyy-mm-dd",
                autoclose: true
            });
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<h3>Edit customer timebook</h3>
<div class="form-group">
	<label for="<%= textBoxTimebookDate.ClientID %>">Date</label>
    <asp:TextBox ID="textBoxTimebookDate" runat="server" CssClass="form-control"></asp:TextBox>
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
	<label for="<%= textBoxTimebookConsultant.ClientID %>">Consultant</label>
    <asp:TextBox ID="textBoxTimebookConsultant" runat="server" CssClass="form-control"></asp:TextBox>
</div>
<div class="form-group">
	<label for="<%= textBoxTimebookComments.ClientID %>">Comments</label>
    <asp:TextBox ID="textBoxTimebookComments" runat="server" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
</div>
<div>
    <asp:Button CssClass="btn btn-success" ID="buttonSave" runat="server" Text="Save customer timebook" 
        onclick="buttonSave_Click" />
        or <i><%= HtmlHelper.Anchor("cancel", string.Format("customershow.aspx?Id={0}&SelectedTab=timebook", customerId)) %></i>
</div>

</asp:Content>