<%@ Page Title="" Language="C#" MasterPageFile="~/Invoicing.Master" AutoEventWireup="true" CodeBehind="UserEdit.aspx.cs" Inherits="HW.Invoicing.UserEdit" %>
<%@ Import Namespace="HW.Core.Helpers" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<h3>Edit a user</h3>
<p>
    User name<br />
    <asp:TextBox ID="textBoxName" runat="server"></asp:TextBox>
</p>
<p>
    Password<br />
    <asp:TextBox ID="textBoxPassword" runat="server" TextMode="Password"></asp:TextBox>
</p>
<p>
    <asp:Button ID="buttonSave" runat="server" Text="Save user" 
        onclick="buttonSave_Click" CssClass="btn btn-success" />
        or <i><%= HtmlHelper.Anchor("cancel", "users.aspx") %></i>
</p>

</asp:Content>
