<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ReportPartLanguagesEdit.aspx.cs" Inherits="HW.EForm.ReportPartLanguagesEdit" %>
<%@ Import Namespace="HW.Core.Helpers" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<h3>Report Part Language Information</h3>

<%= FormHelper.OpenForm("ReportPartLanguagesEdit.aspx?ReportPartLangID=" + part.Id)%>
<p>
	Language: <%= part.Language.Name %>
</p>
<p>
	Subject<br />
	<%= FormHelper.Input("Subject", part.Subject) %>
</p>
<p>
	Header<br />
	<%= FormHelper.TextArea("Header", part.Header) %>
</p>
<p>
	Footer<br />
	<%= FormHelper.TextArea("Footer", part.Footer) %>
</p>
<p>
	<%= BootstrapHelper.Button("Submit", "Update report part language", "btn btn-success", "icon-plus") %>
</p>
</form>

</asp:Content>
