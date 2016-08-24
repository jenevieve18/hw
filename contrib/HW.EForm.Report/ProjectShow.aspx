<%@ Page Title="" Language="C#" MasterPageFile="~/Layout.Master" AutoEventWireup="true" CodeBehind="ProjectShow.aspx.cs" Inherits="HW.EForm.Report.ProjectShow" %>
<%@ Import Namespace="HW.EForm.Core.Helpers" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h3><%= project.Internal %></h3>
    <table>
        <tr>
            <td><strong>Name</strong></td>
            <td><%= project.Name %></td>
        </tr>
        <tr>
            <td><strong>Logo</strong></td>
            <td></td>
        </tr>
        <tr>
            <td valign="top"><strong>Survey(s)</strong></td>
            <td>
                <ul>
                <% foreach (var s in project.Surveys) { %>
                <li><%= s.Survey.Internal %></li>
                <% } %>
                </ul>
            </td>
        </tr>
    </table>
    <h4>Project Rounds</h4>
    <table class="table table-hover table-striped">
        <tr>
            <th>Project Round</th>
            <th>Survey</th>
            <th></th>
        </tr>
        <% foreach (var r in project.Rounds) { %>
        <tr>
            <td><%= HtmlHelper.Anchor(r.Internal, "projectroundshow.aspx?ProjectRoundID=" + r.ProjectRoundID) %></td>
            <td><%= r.Survey.ToString() %></td>
            <td>
                <% foreach (var l in r.Languages) { %>
                <img src="assets/img/langID_<%= l.LangID - 1 %>.gif" />
                <% } %>
            </td>
        </tr>
        <% } %>
    </table>
</asp:Content>
