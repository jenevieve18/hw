<%@ Page Title="" Language="C#" MasterPageFile="~/Layout.Master" AutoEventWireup="true" CodeBehind="FeedbackEdit.aspx.cs" Inherits="HW.EForm2.FeedbackEdit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table>
        <tr>
            <td>Feedback</td>
        </tr>
    </table>

    <table>
        <% foreach (var fq in feedback.Questions) { %>
        <tr>
            <td><%= fq.Question.Internal %></td>
        </tr>
        <% } %>
    </table>
</asp:Content>
