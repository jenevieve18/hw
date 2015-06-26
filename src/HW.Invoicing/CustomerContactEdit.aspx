<%@ Page Title="" Language="C#" MasterPageFile="~/Invoicing.Master" AutoEventWireup="true" CodeBehind="CustomerContactEdit.aspx.cs" Inherits="HW.Invoicing.CustomerContactEdit" %>
<%@ Import Namespace="HW.Core.Helpers" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<h3>Edit customer contact details</h3>
<% if (message != null && message != "") { %>
<div class="alert alert-warning">
    <%= message %>
</div>
<% } %>
<div class="form-group">
	<label for="<%= textBoxContact.ClientID %>">Contact person</label>
    <asp:TextBox ID="textBoxContact" runat="server" CssClass="form-control"></asp:TextBox>
</div>
<div class="form-group">
	<label for="<%= textBoxPhone.ClientID %>">Phone</label>
    <asp:TextBox ID="textBoxPhone" runat="server" CssClass="form-control"></asp:TextBox>
</div>
<div class="form-group">
	<label for="<%= textBoxMobile.ClientID %>">Mobile</label>
    <asp:TextBox ID="textBoxMobile" runat="server" CssClass="form-control"></asp:TextBox>
</div>
<div class="form-group">
	<label for="<%= textBoxEmail.ClientID %>">Email</label>
    <asp:TextBox ID="textBoxEmail" runat="server" CssClass="form-control"></asp:TextBox>
</div>
<div class="form-group">
    <asp:RadioButtonList ID="radioButtonListContactType" runat="server">
    </asp:RadioButtonList>
</div>
<asp:PlaceHolder ID="placeHolderReactivate" runat="server">
    <div class="form-group">
        <asp:CheckBox ID="checkBoxReactivate" runat="server" CssClass="form-control" Text="&nbsp;Re-activate this customer contact" />
    </div>
</asp:PlaceHolder>
<div>
    <asp:Button CssClass="btn btn-success" ID="buttonSave" runat="server" Text="Save customer contact" 
        onclick="buttonSave_Click" />
        or <i><%= HtmlHelper.Anchor("cancel", string.Format("customershow.aspx?Id={0}&SelectedTab=contact-persons", customerId))%></i>
</div>

</asp:Content>
