﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Invoicing.Master" AutoEventWireup="true" CodeBehind="IssueAdd.aspx.cs" Inherits="HW.Invoicing.IssueAdd" %>
<%@ Import Namespace="HW.Core.Helpers" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<h3>Add an issue</h3>
<div class="form-group">
	<label for="<%= textBoxTitle.ClientID %>">Title</label>
    <asp:TextBox ID="textBoxTitle" runat="server" CssClass="form-control"></asp:TextBox>
</div>
<div class="form-group">
	<label for="<%= textBoxDescription.ClientID %>">Description</label>
    <asp:TextBox ID="textBoxDescription" runat="server" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
</div>
<div>
    <asp:Button CssClass="btn btn-success" ID="buttonSave" runat="server" Text="Save issue" 
        onclick="buttonSave_Click" />
        or <i><%= HtmlHelper.Anchor("cancel", "customers.aspx") %></i>
</div>

</asp:Content>
