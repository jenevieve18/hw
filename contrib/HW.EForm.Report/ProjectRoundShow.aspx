<%@ Page Title="" Language="C#" MasterPageFile="~/Layout.Master" AutoEventWireup="true" CodeBehind="ProjectRoundShow.aspx.cs" Inherits="HW.EForm.Report.ProjectRoundShow" %>
<%@ Import Namespace="HW.EForm.Core.Helpers" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h3><%= HtmlHelper.Anchor("<i class='fa fa-arrow-left' aria-hidden='true'></i>", "projectshow.aspx?ProjectID=" + projectRound.ProjectID) %> <%= projectRound.Internal %></h3>
    <p><strong>Survey: </strong><%= projectRound.Survey.ToString() %> <img src="assets/img/<%= projectRound.LangID %>.gif" /></p>
    <h4>Units</h4>
    <table class="table table-hover table-striped">
        <tr>
            <th>Unit</th>
            <th></th>
            <th>ID</th>
            <th>Users</th>
            <th>Sent</th>
            <th>Survey</th>
            <th>Lang</th>
        </tr>
        <% foreach (var pru in projectRound.Units) { %>
        <tr>
            <%--<td><%= HtmlHelper.Anchor(u.Unit, "projectroundunitshow.aspx?ProjectRoundUnitID=" + u.ProjectRoundUnitID) %></td>--%>
            <td><%= HtmlHelper.Anchor(pru.Unit, string.Format("feedbackshow.aspx?FeedbackID={0}&ProjectRoundID={1}&ProjectRoundUnitID={2}", pru.ProjectRound.FeedbackID, pru.ProjectRound.ProjectRoundID, pru.ProjectRoundUnitID)) %></td>
            <td><%= HtmlHelper.Anchor(pru.Unit, string.Format("feedbackshow2.aspx?FeedbackID={0}&ProjectRoundID={1}&ProjectRoundUnitID={2}", pru.ProjectRound.FeedbackID, pru.ProjectRound.ProjectRoundID, pru.ProjectRoundUnitID)) %></td>
            <td><%= pru.ID %></td>
            <td><%= pru.Managers.Count %></td>
            <td></td>
            <td><%= pru.Survey.ToString() %></td>
            <td><img src="assets/img/<%= pru.LangID %>.gif" /></td>
        </tr>
        <% } %>
    </table>
</asp:Content>
