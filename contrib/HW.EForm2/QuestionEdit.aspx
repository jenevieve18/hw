<%@ Page Title="" Language="C#" MasterPageFile="~/Layout.Master" AutoEventWireup="true" CodeBehind="QuestionEdit.aspx.cs" Inherits="HW.EForm2.QuestionEdit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h3>Question information</h3>
    <table>
        <% foreach (var o in question.Options) { %>
        <tr>
            <td><%= o.Option.Internal %></td>
        </tr>
        <% } %>
    </table>
</asp:Content>
