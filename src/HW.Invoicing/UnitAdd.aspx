<%@ Page Title="" Language="C#" MasterPageFile="~/Invoicing.Master" AutoEventWireup="true" CodeBehind="UnitAdd.aspx.cs" Inherits="HW.Invoicing.UnitAdd" %>
<%@ Import Namespace="HW.Core.Helpers" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


<h3>Add a unit</h3>
<div class="form-group">
	<label for="<%= textBoxName.ClientID %>">Unit name</label>
    <asp:TextBox ID="textBoxName" runat="server" CssClass="form-control"></asp:TextBox>
</div>
<div>
    <asp:Button CssClass="btn btn-success" ID="buttonSave" runat="server" Text="Save customer" 
        onclick="buttonSave_Click" />
        or <i><%= HtmlHelper.Anchor("cancel", "customers.aspx") %></i>
</div>

</asp:Content>
