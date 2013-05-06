<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ReportPartsEdit.aspx.cs" Inherits="HW.EForm.ReportPartsEdit" %>
<%@ Import Namespace="HW.Core.Helpers" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<h3>Update report part information</h3>

<%= FormHelper.OpenForm("ReportPartsEdit.aspx?ReportPartID=" + part.Id)%>
	<p>
		Internal<br />
		<%= FormHelper.Input("Internal", part.Internal) %>
	</p>
	<p>
		<%= BootstrapHelper.Button("Submit", "Update report part information", "btn btn-success", "icon-plus") %>
	</p>
</form>

</asp:Content>
