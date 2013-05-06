<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ReportsEdit.aspx.cs" Inherits="HW.EForm.ReportsEdit" %>
<%@ Import Namespace="HW.Core.Helpers" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<h3>Report Information</h3>

<p>
	Internal<br />
	<%= FormHelper.Input("Internal", report.Internal) %>
</p>
<p>
	<%= BootstrapHelper.Button("Submit", "Update report information", "btn btn-success", "icon-plus") %>
	<%= HtmlHelper.Anchor("Cancel", "Reports.aspx") %>
</p>

</asp:Content>
