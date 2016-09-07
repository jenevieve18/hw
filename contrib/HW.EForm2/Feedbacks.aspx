<%@ Page Title="" Language="C#" MasterPageFile="~/Layout.Master" AutoEventWireup="true" CodeBehind="Feedbacks.aspx.cs" Inherits="HW.EForm2.Feedbacks" %>
<%@ Import Namespace="HW.EForm.Core.Helpers" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <%= HtmlHelper.Anchor("Add feedback", "feedbackadd.aspx") %>
    <table>
        <% foreach (var f in feedbacks) { %>
        <tr>
            <td><%= f.FeedbackText %></td>
            <td><%= HtmlHelper.Anchor("Edit", "feedbackedit.aspx?FeedbackID=" + f.FeedbackID) %></td>
        </tr>
        <% } %>
    </table>
</asp:Content>
