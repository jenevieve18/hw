<%@ Page Title="" Language="C#" MasterPageFile="~/Layout.Master" AutoEventWireup="true" CodeBehind="ProjectRoundShow.aspx.cs" Inherits="HW.EForm.Report.ProjectRoundShow" %>
<%@ Import Namespace="HW.EForm.Core.Helpers" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h3><%= projectRound.Internal %></h3>
    <p><strong>Survey: </strong><%= projectRound.Survey.ToString() %> <img src="assets/img/<%= projectRound.LangID %>.gif" /></p>
    <h4>Units</h4>
    <table class="table table-hover table-striped">
        <tr>
            <th>Unit</th>
            <th>ID</th>
            <th>Users</th>
            <th>Sent</th>
            <th>Survey</th>
            <th>Lang</th>
        </tr>
        <% foreach (var u in projectRound.Units) { %>
        <tr>
            <td><%= HtmlHelper.Anchor(u.Unit, "projectroundunitshow.aspx?ProjectRoundUnitID=" + u.ProjectRoundUnitID) %></td>
            <td><%= u.ID %></td>
            <td></td>
            <td></td>
            <td><%= u.Survey.ToString() %></td>
            <td><img src="assets/img/<%= u.LangID %>.gif" /></td>
        </tr>
        <% } %>
    </table>
</asp:Content>
