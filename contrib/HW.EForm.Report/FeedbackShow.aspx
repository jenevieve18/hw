<%@ Page Title="" Language="C#" MasterPageFile="~/Layout.Master" AutoEventWireup="true" CodeBehind="FeedbackShow.aspx.cs" Inherits="HW.EForm.Report.FeedbackShow" %>
<%@ Import Namespace="HW.EForm.Core.Helpers" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<script src="https://code.highcharts.com/highcharts.js"></script>
<script src="https://code.highcharts.com/modules/exporting.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h3><%= feedback.FeedbackText %></h3>
    <% int i = 0; %>
    <% foreach (var fq in feedback.Questions) { %>
        <script>
            $(function () {
                $('#container<%= i %>').highcharts(<%= new HighchartsColumnChart(fq.ToChart()) %>);
            });
        </script>
        <div class="panel panel-default">
            <div class="panel-heading"><%= fq.Question.Internal %></div>
            <div class="panel-body">
                <div id="container<%= i++ %>"></div>
                <%--<img id="container<%= i++ %>" />--%>
            </div>
        </div>
    <% } %>
</asp:Content>
