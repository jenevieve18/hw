<%@ Page Title="" Language="C#" MasterPageFile="~/Layout.Master" AutoEventWireup="true" CodeBehind="Projects.aspx.cs" Inherits="HW.EForm2.Projects" %>
<%@ Import Namespace="HW.EForm.Core.Helpers" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h3>Projects</h3>
    <table>
        <tr>
            <th>Name</th>
        </tr>
        <% foreach (var p in projects) { %>
        <tr>
            <td><%= p.Internal %></td>
            <td>
                <%= HtmlHelper.Anchor("Edit", "projectedit.aspx?ProjectID=" + p.ProjectID) %>
            </td>
        </tr>
        <% } %>
    </table>
</asp:Content>
