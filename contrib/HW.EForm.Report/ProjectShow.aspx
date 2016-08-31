<%@ Page Title="" Language="C#" MasterPageFile="~/Layout.Master" AutoEventWireup="true" CodeBehind="ProjectShow.aspx.cs" Inherits="HW.EForm.Report.ProjectShow" %>
<%@ Import Namespace="HW.EForm.Core.Helpers" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h3><%= HtmlHelper.Anchor("<i class='fa fa-arrow-left' aria-hidden='true'></i>", "projects.aspx") %> <%= project.Internal %></h3>
    <p><strong>Name: </strong><%= project.Name %></p>
    <h4>Project Rounds</h4>
    <table class="table table-hover table-striped">
        <tr>
            <th>Project Round</th>
            <th>Survey</th>
            <th></th>
        </tr>
        <% foreach (var pr in project.Rounds) { %>
        <tr>
            <td><%= HtmlHelper.Anchor(pr.Internal, "projectroundshow.aspx?ProjectRoundID=" + pr.ProjectRoundID) %></td>
            <td><%= pr.Survey.ToString() %></td>
            <td>
                <% foreach (var l in pr.Languages) { %>
                <img src="assets/img/<%= l.LangID %>.gif" />
                <% } %>
            </td>
        </tr>
        <% } %>
    </table>
</asp:Content>
