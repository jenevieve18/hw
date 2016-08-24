<%@ Page Title="" Language="C#" MasterPageFile="~/Layout.Master" AutoEventWireup="true" CodeBehind="SurveyEdit.aspx.cs" Inherits="HW.EForm2.SurveyEdit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h3>Survey information</h3>
    <table>
        <% foreach (var q in survey.Questions) { %>
        <tr>
            <td><%= q.Question.Internal %></td>
        </tr>
        <% } %>
    </table>
</asp:Content>
