<%@ Page Title="" Language="C#" MasterPageFile="~/Layout.Master" AutoEventWireup="true" CodeBehind="Surveys.aspx.cs" Inherits="HW.EForm2.Surveys" %>
<%@ Import Namespace="HW.EForm.Core.Helpers" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h3>Surveys</h3>
    <table>
        <% foreach (var s in surveys) { %>
        <tr>
            <td><%= s.SurveyID %></td>
            <td><%= s.Internal %></td>
            <td>
                <%= HtmlHelper.Anchor("Edit", "surveyedit.aspx?SurveyID=" + s.SurveyID) %>
            </td>
        </tr>
        <% } %>
    </table>
</asp:Content>
