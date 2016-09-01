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
        <% int i = 0; %>
        <% foreach (var fq in feedback.Questions) { %>
            <script>
                $(function () {
                    $('#container<%= i %>').highcharts(<%= HighchartsBoxplot.GetHighchartsChart(fq.Question.Options[0].Option.OptionType, fq.ToChart(true)) %>);
                });
            </script>
            <div class="panel panel-default">
                <div class="panel-heading">
                    <span class="hidden">QuestionID: <%= fq.QuestionID %>, Options: <%= fq.Question.Options.Count %></span>
                    <%= fq.Question.GetLanguage(1).Question %>
                </div>
                <div class="panel-body">
                    <div id="container<%= i++ %>"></div>
                    <%--<img id="container<%= i++ %>" />--%>
                </div>
            </div>
        <% } %>
    <% } else { %>
        <h3><%= HtmlHelper.Anchor("<i class='fa fa-arrow-left' aria-hidden='true'></i>", "projectroundshow.aspx?ProjectRoundID=" + projectRoundID) %> No feedback result.</h3>
        Go back to list of <%= HtmlHelper.Anchor("projects", "projects.aspx") %>.
    <% } %>
</asp:Content>
