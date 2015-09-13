<%@ Page Title="" Language="C#" MasterPageFile="~/Invoicing.Master" AutoEventWireup="true" CodeBehind="InvoiceRevertAll.aspx.cs" Inherits="HW.Invoicing.InvoiceRevertAll" %>
<%@ Import Namespace="HW.Core.Helpers" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<h3>Rever all invoices</h3>
<% if (message != null && message != "") { %>
<div class="alert alert-warning">
    <%= message %>
</div>
<% } %>
<div class="form-group">
	<label for="<%= textBoxInvoiceNumber.ClientID %>">Starting invoice number</label>
    <asp:TextBox ID="textBoxInvoiceNumber" runat="server" CssClass="form-control"></asp:TextBox>
</div>
<div>
    <asp:Button CssClass="btn btn-danger" ID="buttonRevertAll" runat="server" Text="Rever all invoices" 
        onclick="buttonReverAll_Click" />
        or <i><%= HtmlHelper.Anchor("cancel", "invoices.aspx")%></i>
</div>

</asp:Content>
