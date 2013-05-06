<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="LanguagesEdit.aspx.cs" Inherits="HW.EForm.LanguagesEdit" %>
<%@ Import Namespace="HW.Core.Helpers" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<h3>Edit Language Information</h3>

<%= FormHelper.OpenForm("LanguagesEdit.aspx?LangID=" + language.Id) %>
<p>
	Name<br />
	<%= FormHelper.Input("Name", language.Name) %>
</p>
<p>
	<%= BootstrapHelper.Button("Submit", "Update language information", "btn btn-success", "icon-plus") %>
</p>
</form>

</asp:Content>
