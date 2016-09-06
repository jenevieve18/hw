<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FeedbackQuestionImage.aspx.cs" Inherits="HW.EForm.Report.FeedbackQuestionImage" %>
<<%@ Import Namespace="HW.EForm.Core.Helpers" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.12.4/jquery.min.js"></script>
    <script src="https://code.highcharts.com/highcharts.js"></script>
    <script src="https://code.highcharts.com/highcharts-more.js"></script>
    <script src="https://code.highcharts.com/modules/exporting.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    
        <% if (feedback != null) { %>
            <h3><%= HtmlHelper.Anchor("<i class='fa fa-arrow-left' aria-hidden='true'></i>", "projectroundshow.aspx?ProjectRoundID=" + projectRoundID) %> <%= feedback.FeedbackText %></h3>
            <%--<p><%= HtmlHelper.Anchor("Export", string.Format("export.aspx?FeedbackID={0}&ProjectRoundID={1}&ProjectRoundUnitID={2}", feedbackID, projectRoundID, projectRoundUnitID), "class='btn btn-default'") %></p>--%>
            <% int i = 0; %>
            <% foreach (var fq in feedback.Questions) { %>
                <script>
                    $(function () {
                        //$('#container<%= i %>').highcharts();
                        var options = <%= HighchartsBoxplot.GetHighchartsChart(fq.Question.Options[0].Option.OptionType, fq.ToChart(true)) %>;
                        var exportUrl = 'http://export.highcharts.com/';
                        var object = {
                            options: JSON.stringify(options),
                            type: 'image/png',
                            async: true
                        };
                        $.ajax({
                            type: 'post',
                            url: exportUrl,
                            data: object,
                            success: function (data) {
                                $('#container<%= i %>').attr('src', exportUrl + data);
                            }
                        });
                    });
                </script>
                <div class="panel panel-default">
                    <div class="panel-heading">
                        <span class="hidden">QuestionID: <%= fq.QuestionID %>, Options: <%= fq.Question.Options.Count %></span>
                        <%= fq.Question.GetLanguage(1).Question %>
                    </div>
                    <div class="panel-body">
                        <%--<div id="container<%= i++ %>"></div>--%>
                        <img id="container<%= i++ %>" />
                    </div>
                </div>
            <% } %>
        <% } else { %>
            <h3><%= HtmlHelper.Anchor("<i class='fa fa-arrow-left' aria-hidden='true'></i>", "projectroundshow.aspx?ProjectRoundID=" + projectRoundID) %> No feedback result.</h3>
            Go back to list of <%= HtmlHelper.Anchor("projects", "projects.aspx") %>.
        <% } %>


    </form>
</body>
</html>
