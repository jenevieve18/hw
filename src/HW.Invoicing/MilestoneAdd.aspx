<%@ Page Title="" Language="C#" MasterPageFile="~/Invoicing.Master" AutoEventWireup="true" CodeBehind="MilestoneAdd.aspx.cs" Inherits="HW.Invoicing.MilestoneAdd" %>
<%@ Import Namespace="HW.Core.Helpers" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">



<h3>Add a milestone</h3>
<div class="form-group">
	<label for="<%= textBoxName.ClientID %>">Name</label>
    <asp:TextBox ID="textBoxName" runat="server" CssClass="form-control"></asp:TextBox>
</div>
<div>
    <asp:Button ID="buttonSave" runat="server" Text="Save milestone" 
        onclick="buttonSave_Click" CssClass="btn btn-success" />
        or <i><%= HtmlHelper.Anchor("cancel", "milestones.aspx") %></i>
</div>

</asp:Content>
