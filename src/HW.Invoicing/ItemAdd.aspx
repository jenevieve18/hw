<%@ Page Title="" Language="C#" MasterPageFile="~/Invoicing.Master" AutoEventWireup="true" CodeBehind="ItemAdd.aspx.cs" Inherits="HW.Invoicing.ItemAdd" %>
<%@ Import Namespace="HW.Core.Helpers" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<h3>Add an item</h3>
<p>
    Item name<br />
    <asp:TextBox ID="textBoxName" runat="server"></asp:TextBox>
</p>
<p>
    Description<br />
    <asp:TextBox ID="textBoxDescription" runat="server"></asp:TextBox>
</p>
<p>
    Price<br />
    <asp:TextBox ID="textBoxPrice" runat="server"></asp:TextBox>
</p>
<p>
    <asp:Button ID="buttonSave" CssClass="btn btn-success" runat="server" Text="Save item" 
        onclick="buttonSave_Click" />
        or <i><%= HtmlHelper.Anchor("cancel", "items.aspx") %></i>
</p>

</asp:Content>
