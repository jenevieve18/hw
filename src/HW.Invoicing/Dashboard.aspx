<%@ Page Title="" Language="C#" MasterPageFile="~/Invoicing.Master" AutoEventWireup="true" CodeBehind="Dashboard.aspx.cs" Inherits="HW.Invoicing.Dashboard" %>
<%@ Import Namespace="HW.Core.Helpers" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

<script type="text/javascript" src="https://www.google.com/jsapi?autoload={
    'modules':[{
        'name':'visualization',
        'version':'1',
        'packages':['corechart']
    }]
    }">
</script>

<script type="text/javascript">
    google.setOnLoadCallback(drawChart);

    function drawChart() {
        var data = google.visualization.arrayToDataTable([
            ['Year', 'Sales', 'Expenses'],
            ['2004', 1000, 400],
            ['2005', 1170, 460],
            ['2006', 660, 1120],
            ['2007', 1030, 540]
        ]);

        var options = {
            title: 'Company Performance',
            curveType: 'function',
            legend: { position: 'bottom' }
        };

        var lineChart = new google.visualization.LineChart(document.getElementById('curve_chart'));
        lineChart.draw(data, options);

        var data = google.visualization.arrayToDataTable([
            ['Task', 'Hours per Day'],
            ['Work', 11],
            ['Eat', 2],
            ['Commute', 2],
            ['Watch TV', 2],
            ['Sleep', 7]
        ]);

        var options = {
            title: 'My Daily Activities'
        };

        var pieChart = new google.visualization.PieChart(document.getElementById('piechart'));
        pieChart.draw(data, options);
    }
</script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<h3>Dashboard</h3>

<div class="row">
    <div class="col-sm-6">
        <h4>Settings</h4>
        <div class="well">
            <p>Bellow are the most common tasks</p>
            <ul>
                <li>Add a <%= HtmlHelper.Anchor("customer", "customeradd.aspx") %> or an <%= HtmlHelper.Anchor("item", "itemadd.aspx") %>.</li>
                <li>Show <%= HtmlHelper.Anchor("customers", "customers.aspx")%> and add timebook.</li>
                <li>List <%= HtmlHelper.Anchor("invoices", "invoices.aspx") %> and check receivables.</li>
            </ul>
        </div>
    </div>
    <div class="col-sm-6">
        <h4>Income and Expenses</h4>
        <div class="well">
            <div id="curve_chart"></div>
        </div>

        <h4>Expenses</h4>
        <div class="well">
            <div id="piechart"></div>
        </div>
    </div>
</div>

</asp:Content>
