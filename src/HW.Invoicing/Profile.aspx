<%@ Page Title="" Language="C#" MasterPageFile="~/Invoicing.Master" AutoEventWireup="true" CodeBehind="Profile.aspx.cs" Inherits="HW.Invoicing.Profile" %>
<%@ Import Namespace="HW.Core.Helpers" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<h3>Edit your profile</h3>
<div class="form-group">
	<label for="<%= textBoxName.ClientID %>">User name</label>
    <asp:TextBox ID="textBoxName" runat="server" CssClass="form-control"></asp:TextBox>
</div>
<div class="form-group">
	<label for="<%= textBoxPassword.ClientID %>">Password</label>
    <asp:TextBox ID="textBoxPassword" runat="server" CssClass="form-control" TextMode="SingleLine"></asp:TextBox>
</div>
<div>
    <asp:Button ID="buttonSave" CssClass="btn btn-success" runat="server" Text="Save profile" 
        onclick="buttonSave_Click" />
        or <i><%= HtmlHelper.Anchor("cancel", "dashboard.aspx") %></i>
</div>

</asp:Content>
