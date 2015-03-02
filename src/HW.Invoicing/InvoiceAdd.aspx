<%@ Page Title="" Language="C#" MasterPageFile="~/Invoicing.Master" AutoEventWireup="true" CodeBehind="InvoiceAdd.aspx.cs" Inherits="HW.Invoicing.InvoiceAdd" %>
<%@ Import Namespace="HW.Core.Helpers" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<h3>Add an invoice</h3>
<p>
    Date<br />
    <asp:TextBox ID="textBoxDate" runat="server"></asp:TextBox>
</p>
<p>
    <asp:Button ID="buttonSave" runat="server" Text="Save invoice" 
        onclick="buttonSave_Click" CssClass="btn btn-success" />
        or <i><%= HtmlHelper.Anchor("cancel", "invoices.aspx") %></i>
</p>

</asp:Content>
