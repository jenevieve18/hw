<%@ Page Title="" Language="C#" MasterPageFile="~/Layout.Master" AutoEventWireup="true" CodeBehind="OptionComponents.aspx.cs" Inherits="HW.EForm2.OptionComponents" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h3>Option components</h3>
    <table>
        <% foreach (var c in components) { %>
        <tr>
            <td><%= c.Internal %></td>
        </tr>
        <% } %>
    </table>
</asp:Content>
