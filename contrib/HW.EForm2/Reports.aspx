<%@ Page Title="" Language="C#" MasterPageFile="~/Layout.Master" AutoEventWireup="true" CodeBehind="Reports.aspx.cs" Inherits="HW.EForm2.Reports" %>
<%@ Import Namespace="HW.EForm.Core.Helpers" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h3>Reports</h3>
    <table>
        <% foreach (var r in reports) { %>
        <tr>
            <td><%= r.Internal %></td>
            <td><%= HtmlHelper.Anchor("Edit", "reportedit.aspx?ReportID=" + r.ReportID) %></td>
        </tr>
        <% } %>
    </table>
</asp:Content>
