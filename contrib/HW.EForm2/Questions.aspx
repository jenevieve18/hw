<%@ Page Title="" Language="C#" MasterPageFile="~/Layout.Master" AutoEventWireup="true" CodeBehind="Questions.aspx.cs" Inherits="HW.EForm2.Questions" %>
<%@ Import Namespace="HW.EForm.Core.Helpers" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h3>Questions</h3>
    <table>
        <% foreach (var q in questions) { %>
        <tr>
            <td><%= q.QuestionID %></td>
            <td><%= q.Internal %></td>
            <td>
                <%= HtmlHelper.Anchor("Edit", "questionedit.aspx?QuestionID=" + q.QuestionID) %>
            </td>
        </tr>
        <% } %>
    </table>
</asp:Content>
