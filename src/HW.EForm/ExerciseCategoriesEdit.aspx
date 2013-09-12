<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ExerciseCategoriesEdit.aspx.cs" Inherits="HW.EForm.ExerciseCategoriesEdit" %>
<%@ Import Namespace="HW.Core.Helpers" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<h3>Edit Exercise Category</h3>

<p>
    Category<br />
    <%= FormHelper.Input("CategoryName", category.CategoryName) %>
</p>
<p>
	<%= BootstrapHelper.Button("Submit", "Update exercise category information", "btn btn-success", "icon-plus") %>
</p>

</asp:Content>
