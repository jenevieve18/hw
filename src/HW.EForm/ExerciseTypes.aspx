<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ExerciseTypes.aspx.cs" Inherits="HW.EForm.ExerciseTypes" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<h3>Exercise types</h3>

<table class="table table-hover">
    <% foreach (var t in types) { %>
    <tr>
        <td><%= t.TypeName %></td>
        <td><%= t.Language.Name %></td>
    </tr>
    <% } %>
</table>

</asp:Content>
