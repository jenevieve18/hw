<%@ Page Title="" Language="C#" MasterPageFile="~/Invoicing.Master" AutoEventWireup="true" CodeBehind="CustomerAdd.aspx.cs" Inherits="HW.Invoicing.CustomerAdd" %>
<%@ Import Namespace="HW.Core.Helpers" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<h3>Add a customer</h3>
<div class="form-group">
	<label for="<%= textBoxName.ClientID %>">Customer Number</label>
    <asp:TextBox ID="textBoxNumber" runat="server" CssClass="form-control"></asp:TextBox>
</div>
<div class="form-group">
	<label for="<%= textBoxName.ClientID %>">Customer Name</label>
    <asp:TextBox ID="textBoxName" runat="server" CssClass="form-control"></asp:TextBox>
</div>
<div class="form-group">
	<label for="<%= textBoxAddress.ClientID %>">Postal Address</label>
    <asp:TextBox ID="textBoxAddress" runat="server" CssClass="form-control"></asp:TextBox>
</div>
<div class="form-group">
	<label for="<%= textBoxPhone.ClientID %>">Invoicing Address</label>
    <asp:TextBox ID="textBoxPhone" runat="server" CssClass="form-control"></asp:TextBox>
</div>
<div class="form-group">
	<label for="<%= textBoxEmail.ClientID %>">Reference / Purchase Order Number</label>
    <asp:TextBox ID="textBoxEmail" runat="server" CssClass="form-control"></asp:TextBox>
</div>
<div class="form-group">
	<label for="<%= textBoxEmail.ClientID %>">Your Reference Person</label>
    <asp:TextBox ID="textBox1" runat="server" CssClass="form-control"></asp:TextBox>
</div>
<div class="form-group">
	<label for="<%= textBoxEmail.ClientID %>">Our Reference Person</label>
    <asp:TextBox ID="textBox2" runat="server" CssClass="form-control"></asp:TextBox>
</div>
<div>
    <asp:Button CssClass="btn btn-success" ID="buttonSave" runat="server" Text="Save customer" 
        onclick="buttonSave_Click" />
        or <i><%= HtmlHelper.Anchor("cancel", "customers.aspx") %></i>
</div>

</asp:Content>
