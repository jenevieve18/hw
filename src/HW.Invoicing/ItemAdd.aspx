﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Invoicing.Master" AutoEventWireup="true" CodeBehind="ItemAdd.aspx.cs" Inherits="HW.Invoicing.ItemAdd" %>
<%@ Import Namespace="HW.Core.Helpers" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <h3>Add an item</h3>
    <% if (message != null && message != "") { %>
    <div class="alert alert-warning">
        <%= message %>
    </div>
    <% } %>
    <div class="form-group">
	    <label for="<%= textBoxName.ClientID %>">Item Name</label>
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
	    <label for="<%= dropDownListUnits.ClientID %>">Unit <%= HtmlHelper.Anchor("+", "unitadd.aspx") %></label>
        <asp:DropDownList ID="dropDownListUnits" runat="server" CssClass="form-control">
        </asp:DropDownList>
    </div>
    <div>
        <asp:Button ID="buttonSave" CssClass="btn btn-success" runat="server" Text="Save item" 
            onclick="buttonSave_Click" />
            or <i><%= HtmlHelper.Anchor("cancel", "items.aspx") %></i>
    </div>

</asp:Content>
