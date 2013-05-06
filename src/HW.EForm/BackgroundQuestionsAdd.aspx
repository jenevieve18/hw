<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="BackgroundQuestionsAdd.aspx.cs" Inherits="HW.EForm.BackgroundQuestionsAdd" %>
<%@ Import Namespace="HW.Core.Helpers" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<h3>Add a background question</h3>

<%= FormHelper.OpenForm("BackgroundQuestionsAdd.aspx") %>
<p>
	Internal<br />
	<%= FormHelper.Input("Internal") %>
</p>
<p>
	<%= BootstrapHelper.Button("Submit", "Add a background question", "btn btn-success", "icon-plus") %>
</p>
</form>

</asp:Content>
