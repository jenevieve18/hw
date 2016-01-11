<%@ Page Title="" Language="C#" MasterPageFile="~/Invoicing.Master" AutoEventWireup="true" CodeBehind="ItemEdit.aspx.cs" Inherits="HW.Invoicing.ItemEdit" %>
<%@ Import Namespace="HW.Core.Helpers" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<h3>Edit an item</h3>
<% if (message != null && message != "") { %>
<div class="alert alert-warning">
    <%= message %>
</div>
<% } %>

<div class="form-group">
	<label for="<%= textBoxName.ClientID %>">Item name</label>
    <asp:TextBox ID="textBoxName" runat="server" CssClass="form-control"></asp:TextBox>
</div>
<div class="form-group">
	<label for="<%= textBoxConsultant.ClientID %>">Consultant</label>
    <asp:TextBox ID="textBoxConsultant" runat="server" CssClass="form-control"></asp:TextBox>
</div>
<div class="form-group">
	<label for="<%= textBoxDescription.ClientID %>">Description</label>
    <asp:TextBox ID="textBoxDescription" runat="server" CssClass="form-control"></asp:TextBox>
</div>
<div class="form-group">
	<label for="<%= textBoxPrice.ClientID %>">Price</label>
    <asp:TextBox ID="textBoxPrice" runat="server" CssClass="form-control"></asp:TextBox>
</div>
<div class="form-group">
	<label for="<%= dropDownListUnits.ClientID %>">Unit</label>
    <asp:DropDownList ID="dropDownListUnits" runat="server" CssClass="form-control">
    </asp:DropDownList>
</div>
<asp:PlaceHolder ID="placeHolderReactivate" runat="server">
    <div class="form-group">
        <asp:CheckBox ID="checkBoxReactivate" runat="server" CssClass="form-control" Text="&nbsp;Reactivate this item" />
    </div>
</asp:PlaceHolder>
<div>
    <asp:Button ID="buttonSave" CssClass="btn btn-success" runat="server" Text="Save item" 
        onclick="buttonSave_Click" />
        or <i><%= HtmlHelper.Anchor("cancel", "items.aspx") %></i>
</div>

</asp:Content>
