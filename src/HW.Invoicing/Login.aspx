<%@ Page Title="" Language="C#" MasterPageFile="~/Home.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="HW.Invoicing.Login" %>
<%@ Import Namespace="HW.Core.Helpers" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">



<h3>Log In</h3>
<div class="form-group">
	<label for="<%= textBoxName.ClientID %>">User name</label>
    <asp:TextBox ID="textBoxName" runat="server" CssClass="form-control"></asp:TextBox>
</div>
<div class="form-group">
	<label for="<%= textBoxPassword.ClientID %>">Password</label>
    <asp:TextBox ID="textBoxPassword" runat="server" CssClass="form-control"></asp:TextBox>
</div>
<div>
    <asp:Button CssClass="btn btn-success" ID="buttonLogin" runat="server" Text="Save customer" 
        onclick="buttonLogin_Click" />
        or <i><%= HtmlHelper.Anchor("cancel", "customers.aspx") %></i>
</div>

</asp:Content>
