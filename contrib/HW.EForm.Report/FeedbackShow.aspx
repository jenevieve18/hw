<%@ Page Title="" Language="C#" MasterPageFile="~/Layout.Master" AutoEventWireup="true" CodeBehind="FeedbackShow.aspx.cs" Inherits="HW.EForm.Report.FeedbackShow" %>
<%@ Import Namespace="HW.EForm.Core.Helpers" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="https://code.highcharts.com/highcharts.js"></script>
    <script src="https://code.highcharts.com/highcharts-more.js"></script>
    <script src="https://code.highcharts.com/modules/exporting.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <% if (feedback != null) { %>
        <h3><%= HtmlHelper.Anchor("<i class='fa fa-arrow-left' aria-hidden='true'></i>", "projectroundshow.aspx?ProjectRoundID=" + projectRoundID) %> <%= feedback.FeedbackText %></h3>
        <p><%= HtmlHelper.Anchor("Export", string.Format("export.aspx?FeedbackID={0}&ProjectRoundID={1}&ProjectRoundUnitID={2}", feedbackID, projectRoundID, projectRoundUnitID), "class='btn btn-default'") %></p>
        <%--<% int i = 0; %>--%>
        <%--<% foreach (var fq in feedback.Questions) { %>--%>
        <% foreach (var c in feedback.Charts) { %>
            <script>
                $(function () {
                    $('#container<%= c.ID %>').highcharts(<%= HighchartsBoxplot.GetHighchartsChart(c.Type, c).GetOptions() %>);
                });
            </script>
            <div class="panel panel-default">
                <div class="panel-heading">
                    <%= c.Title %>
                </div>
                <div class="panel-body">
                    <div id="container<%= c.ID %>"></div>
                </div>
            </div>
        <% } %>
    <% } else { %>
        <h3><%= HtmlHelper.Anchor("<i class='fa fa-arrow-left' aria-hidden='true'></i>", "projectroundshow.aspx?ProjectRoundID=" + projectRoundID) %> No feedback result.</h3>
        Go back to list of <%= HtmlHelper.Anchor("projects", "projects.aspx") %>.
    <% } %>
</asp:Content>
