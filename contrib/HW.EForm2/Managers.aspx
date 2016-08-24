<%@ Page Title="" Language="C#" MasterPageFile="~/Layout.Master" AutoEventWireup="true" CodeBehind="Managers.aspx.cs" Inherits="HW.EForm2.Managers" %>
<%@ Import Namespace="HW.EForm.Core.Helpers" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h3>Managers</h3>
    <table>
        <tr>
            <th></th>
            <th></th>
        </tr>
        <% foreach (var m in managers) { %>
        <tr>
            <td><%= m.Name %></td>
            <td><%= m.Email %></td>
            <td><%= HtmlHelper.Anchor("Edit", "manageredit.aspx?ManagerID=" + m.ManagerID) %></td>
        </tr>
        <% } %>
    </table>
</asp:Content>
