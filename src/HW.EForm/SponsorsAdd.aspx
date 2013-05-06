<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SponsorsAdd.aspx.cs" Inherits="HW.EForm.SponsorsAdd" %>
<%@ Import Namespace="HW.Core.Helpers" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<h3>Add a sponsor</h3>

<%= FormHelper.OpenForm("SponsorsAdd.aspx") %>
<p>
	Internal<br />
	<%= FormHelper.Input("Name") %>
</p>
<p>
	<%= BootstrapHelper.Button("Submit", "Add a sponsor", "btn btn-success", "icon-plus") %>
</p>
</form>

</asp:Content>
