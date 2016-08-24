<%@ Page Title="" Language="C#" MasterPageFile="~/Layout.Master" AutoEventWireup="true" CodeBehind="ProjectRoundShow.aspx.cs" Inherits="HW.EForm.Report.ProjectRoundShow" %>
<%@ Import Namespace="HW.EForm.Core.Helpers" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h3><%= projectRound.Internal %></h3>
    <dl class="dl-horizontal">
        <dt>Survey</dt>
        <dd><%= projectRound.Survey.ToString() %></dd>
        <%--<dt>Last Name</dt>
        <dd>Bar</dd>
        <dt>Job</dt>
        <dd>Something</dd>--%>
    </dl>
    <table class="table table-hover table-striped">
        <tr>
            <th>Unit</th>
            <th>ID</th>
            <th>Survey</th>
        </tr>
        <% foreach (var u in projectRound.Units) { %>
        <tr>
            <td><%= HtmlHelper.Anchor(u.Unit, "projectroundunitshow.aspx?ProjectRoundUnitID=" + u.ProjectRoundUnitID) %></td>
            <td><%= u.ID %></td>
            <td><%= u.Survey.ToString() %></td>
        </tr>
        <% } %>
    </table>
</asp:Content>
