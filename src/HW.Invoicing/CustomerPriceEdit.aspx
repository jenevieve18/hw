<%@ Page Title="" Language="C#" MasterPageFile="~/Invoicing.Master" AutoEventWireup="true" CodeBehind="CustomerPriceEdit.aspx.cs" Inherits="HW.Invoicing.CustomerPriceEdit" %>
<%@ Import Namespace="HW.Core.Helpers" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<h3>Edit customer price</h3>
<div class="form-group">
	<label for="<%= dropDownListItems.ClientID %>">Item</label>
    <asp:DropDownList ID="dropDownListItems" runat="server" CssClass="form-control">
    </asp:DropDownList>
</div>
<div class="form-group">
	<label for="<%= textBoxItemPrice.ClientID %>">Price</label>
    <asp:TextBox ID="textBoxItemPrice" runat="server" CssClass="form-control"></asp:TextBox>
</div>
<div>
    <asp:Button CssClass="btn btn-success" ID="buttonSave" runat="server" Text="Save customer price" 
        onclick="buttonSave_Click" />
        or <i><%= HtmlHelper.Anchor("cancel", string.Format("customershow.aspx?Id={0}&SelectedTab=customer-prices", customerId))%></i>
</div>

</asp:Content>
