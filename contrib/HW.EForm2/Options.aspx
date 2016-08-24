<%@ Page Title="" Language="C#" MasterPageFile="~/Layout.Master" AutoEventWireup="true" CodeBehind="Options.aspx.cs" Inherits="HW.EForm2.Options" %>
<%@ Import Namespace="HW.EForm.Core.Helpers" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h3>Options</h3>
    <table>
        <% foreach (var o in options) { %>
        <tr>
            <td><%= o.OptionID %></td>
            <td><%= o.Internal %></td>
            <td>
                <%= HtmlHelper.Anchor("Edit", "optionedit.aspx?OptionID=" + o.OptionID) %>
            </td>
        </tr>
        <% } %>
    </table>
</asp:Content>
