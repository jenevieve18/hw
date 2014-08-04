<%@ Page Title="" Language="C#" MasterPageFile="~/Grp.Master" AutoEventWireup="true" CodeBehind="Managers.aspx.cs" Inherits="HW.Grp3.Managers" %>
<%@ Import Namespace="HW.Core.Helpers" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<table class="table table-striped">
    <% foreach (var a in admins) { %>
    <tr>
        <td><%= HtmlHelper.Anchor(a.Name, "ManagerSetup.aspx") %></td>
        <td>
            <% foreach (var f in a.Functions) { %>
                <%= f.Function.Function %>
            <% } %>
        </td>
    </tr>
    <% } %>
</table>

</asp:Content>
