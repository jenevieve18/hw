<%@ Page Title="" Language="C#" MasterPageFile="~/Layout.Master" AutoEventWireup="true" CodeBehind="ProjectRoundUnitShow.aspx.cs" Inherits="HW.EForm.Report.ProjectRoundUnitShow" %>
<%@ Import Namespace="HW.EForm.Core.Helpers" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<script src="https://code.highcharts.com/highcharts.js"></script>
<script src="https://code.highcharts.com/modules/exporting.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h3><%= projectRoundUnit.Unit %></h3>
    <p><strong>Unit exernal ID: </strong><%= projectRoundUnit.ID %></p>
    <p>
        <strong>Survey: </strong><%= projectRoundUnit.Survey.ToString() %>
        <img src="assets/img/<%= projectRoundUnit.LangID %>.gif" />
    </p>
    <p><%= HtmlHelper.Anchor("Export", "export.aspx", "class='btn btn-default'") %></p>
    <% if (projectRoundUnit.ProjectRound.Feedback != null) { %>
        <% int i = 0; %>
        <% foreach (var q in projectRoundUnit.ProjectRound.Feedback.Questions) { %>
        
        <%--<script>
            $(function () {
                // Regular chart options
                // Just never rendered with "new Highcharts.Chart" or "$('#container').highcharts()"
                var options = {
                    title: {
                        text: 'Monthly Average Temperature',
                        x: -20 //center
                    },
                    subtitle: {
                        text: 'Source: WorldClimate.com',
                        x: -20
                    },
                    xAxis: {
                        categories: ['Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun',
                                     'Jul', 'Aug', 'Sep', 'Oct', 'Nov', 'Dec']
                    },
                    yAxis: {
                        title: {
                            text: 'Temperature (°C)'
                        },
                        plotLines: [{
                            value: 0,
                            width: 1,
                            color: '#808080'
                        }]
                    },
                    tooltip: {
                        valueSuffix: '°C'
                    },
                    legend: {
                        layout: 'vertical',
                        align: 'right',
                        verticalAlign: 'middle',
                        borderWidth: 0
                    },
                    series: [{
                        name: 'Tokyo',
                        data: [7.0, 6.9, 9.5, 14.5, 18.2, 21.5, 25.2, 26.5, 23.3, 18.3, 13.9, 9.6]
                    }, {
                        name: 'New York',
                        data: [-0.2, 0.8, 5.7, 11.3, 17.0, 22.0, 24.8, 24.1, 20.1, 14.1, 8.6, 2.5]
                    }, {
                        name: 'Berlin',
                        data: [-0.9, 0.6, 3.5, 8.4, 13.5, 17.0, 18.6, 17.9, 14.3, 9.0, 3.9, 1.0]
                    }, {
                        name: 'London',
                        data: [3.9, 4.2, 5.7, 8.5, 11.9, 15.2, 17.0, 16.6, 14.2, 10.3, 6.6, 4.8]
                    }]
                };

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
                        console.log('test');
                    }
                });
            });
        </script>--%>
        <div class="panel panel-default">
            <div class="panel-heading"><%= q.Question.Internal %></div>
            <div class="panel-body">
                <div id="container<%= i++ %>"></div>
                <%--<img id="container<%= i++ %>" />--%>
            </div>
        </div>
        <% } %>
    <% } %>
</asp:Content>
