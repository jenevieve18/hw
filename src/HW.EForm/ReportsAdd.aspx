<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ReportsAdd.aspx.cs" Inherits="HW.EForm.ReportsAdd" %>
<%@ Import Namespace="HW.Core.Helpers" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<h3>Add report information</h3>

<%= FormHelper.OpenForm("ReportsAdd.aspx") %>
<p>
	Internal<br />
	<%= FormHelper.Input("Internal") %>
</p>
<p>
	<%= BootstrapHelper.Button("Submit", "Add report information", "btn btn-success", "icon-plus") %>
</p>
</form>

</asp:Content>
