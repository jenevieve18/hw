<%@ Page Title="" Language="C#" MasterPageFile="~/Invoicing.Master" AutoEventWireup="true" CodeBehind="CustomerAdd.aspx.cs" Inherits="HW.Invoicing.CustomerAdd" %>
<%@ Import Namespace="HW.Core.Helpers" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
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
    <asp:TextBox ID="textBoxPostalAddress" runat="server" CssClass="form-control"></asp:TextBox>
</div>
<div class="form-group">
	<label for="<%= textBoxInvoiceAddress.ClientID %>">Invoicing Address</label>
    <asp:TextBox ID="textBoxInvoiceAddress" runat="server" CssClass="form-control"></asp:TextBox>
</div>
<div class="form-group">
	<label for="<%= textBoxPurchaseOrderNumber.ClientID %>">Reference / Purchase Order Number</label>
    <asp:TextBox ID="textBoxPurchaseOrderNumber" runat="server" CssClass="form-control"></asp:TextBox>
</div>
<div class="form-group">
	<label for="<%= textBoxYourReferencePerson.ClientID %>">Your Reference Person</label>
    <asp:TextBox ID="textBoxYourReferencePerson" runat="server" CssClass="form-control"></asp:TextBox>
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
<div>
    <asp:Button CssClass="btn btn-success" ID="buttonSave" runat="server" Text="Save customer" 
        onclick="buttonSave_Click" />
        or <i><%= HtmlHelper.Anchor("cancel", "customers.aspx") %></i>
</div>

</asp:Content>
