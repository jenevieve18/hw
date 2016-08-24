<%@ Page Title="" Language="C#" MasterPageFile="~/Layout.Master" AutoEventWireup="true" CodeBehind="ReportEdit.aspx.cs" Inherits="HW.EForm2.ReportEdit" %>
<%@ Import Namespace="HW.EForm.Core.Helpers" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h3>Report information</h3>
    <table>
        <tr>
            <td>Internal</td>
            <td>
                <asp:TextBox ID="textBoxInternal" runat="server"></asp:TextBox></td>
            <td></td>
        </tr>
        <tr>
            <td></td>
            <td>
                <ul>
                <% foreach (var p in report.Parts) { %>
                <li>
                    <%= p.Internal %>
                    <%= HtmlHelper.Anchor("Delete", "reportpartdelete.aspx?ReportPartID=" + p.ReportPartID) %>
                </li>
                <% } %>
                </ul>
            </td>
            <td></td>
        </tr>
    </table>
</asp:Content>
