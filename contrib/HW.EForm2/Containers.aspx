<%@ Page Title="" Language="C#" MasterPageFile="~/Layout.Master" AutoEventWireup="true" CodeBehind="Containers.aspx.cs" Inherits="HW.EForm2.Containers" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h3>Containers</h3>
    <table>
        <% foreach (var c in containers) { %>
        <tr>
            <td><%= c.QuestionContainerID %></td>
            <td><%= c.Container %></td>
        </tr>
        <% } %>
    </table>
</asp:Content>
